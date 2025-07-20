// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Archetypes;

/// <summary>
/// Represents a chunk in the ECS (Entity Component System) architecture.
/// Stores entities and their associated components in unmanaged memory.
/// </summary>
public sealed unsafe class ArchetypeChunk : IDisposable
{
    /// <summary>
    /// The memory alignment for unmanaged allocations.
    /// </summary>
    private const nuint Alignment = 64;

    /// <summary>
    /// The maximum capacity of entities in the chunk.
    /// </summary>
    private const int Capacity = 16 * 1024;

    private readonly Entity* _entities;
    private readonly Dictionary<int, ComponentArray> _componentArrays;

    /// <summary>
    /// Gets the current number of entities in the chunk.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the chunk is full.
    /// </summary>
    public bool IsFull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Count >= Capacity;
    }

    /// <summary>
    /// Gets a value indicating whether the chunk is empty.
    /// </summary>
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Count is 0;
    }

    /// <summary>
    /// Gets a read-only span of entities currently stored in the chunk.
    /// </summary>
    public ReadOnlySpan<Entity> Entities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(_entities, Count);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchetypeChunk"/> class.
    /// Allocates unmanaged memory for entities and initializes component arrays.
    /// </summary>
    /// <param name="componentIds">The IDs of the components to be stored in the chunk.</param>
    public ArchetypeChunk(int[] componentIds)
    {
        _entities = (Entity*)NativeMemory.AlignedAlloc(Capacity * (nuint)sizeof(Entity), Alignment);
        NativeMemory.Clear(_entities, Capacity * (nuint)sizeof(Entity));

        _componentArrays = [];

        foreach (var id in componentIds)
            _componentArrays[id] = ComponentRegistry.GetMetadata(id).ComponentArrayFactory(Capacity);
    }

    /// <summary>
    /// Finalizer to release unmanaged resources.
    /// </summary>
    ~ArchetypeChunk()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>
    /// Resets the chunk, clearing all entities and component arrays.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        Count = 0;

        NativeMemory.Clear(_entities, Capacity * (nuint)sizeof(Entity));

        foreach (var array in _componentArrays.Values)
            array.Reset();
    }

    /// <summary>
    /// Adds an entity to the chunk.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The index of the added entity.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add(Entity entity)
    {
        var index = Count++;
        _entities[index] = entity;
        return index;
    }

    /// <summary>
    /// Removes an entity from the chunk at the specified index.
    /// Moves the last entity to the removed entity's position to maintain compactness.
    /// </summary>
    /// <param name="index">The index of the entity to remove.</param>
    /// <returns>The entity that was moved to the removed entity's position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the index is negative or exceeds the current count.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Remove(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

        var lastIndex = --Count;

        if (index == lastIndex)
            return Entity.Null;

        var movedEntity = _entities[lastIndex];
        _entities[index] = movedEntity;

        foreach (var array in _componentArrays.Values)
            array.Move(lastIndex, index);

        return movedEntity;
    }

    /// <summary>
    /// Gets a span of components of type <typeparamref name="T"/> for all entities in the chunk.
    /// </summary>
    /// <typeparam name="T">The type of the components.</typeparam>
    /// <returns>A span of components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>()
    {
        return GetArray<T>().GetSpan(Count);
    }

    /// <summary>
    /// Gets a span of components of type <typeparamref name="T"/> for the specified number of entities.
    /// </summary>
    /// <typeparam name="T">The type of the components.</typeparam>
    /// <param name="count">The number of entities to include in the span.</param>
    /// <returns>A span of components.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is negative.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return GetArray<T>().GetSpan(Math.Min(count, Count));
    }

    /// <summary>
    /// Gets the component array for components of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the components.</typeparam>
    /// <returns>The component array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray<T> GetArray<T>()
    {
        return (ComponentArray<T>)_componentArrays[Component<T>.Id];
    }

    /// <summary>
    /// Gets the component array for the specified component ID.
    /// </summary>
    /// <param name="componentId">The ID of the component.</param>
    /// <returns>The component array.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the component ID is negative or exceeds the maximum allowed value.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray GetArray(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(componentId, ComponentMask.MaxComponents - 1);
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);

        return _componentArrays[componentId];
    }

    /// <summary>
    /// Releases unmanaged resources used by the chunk.
    /// </summary>
    private void ReleaseUnmanagedResources()
    {
        NativeMemory.AlignedFree(_entities);

        foreach (var array in _componentArrays.Values)
            array.Dispose();
    }

    /// <summary>
    /// Disposes the chunk, releasing unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
