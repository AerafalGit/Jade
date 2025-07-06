// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;
using Jade.Ecs.Entities;

namespace Jade.Ecs.Archetypes;

/// <summary>
/// Represents a chunk of memory allocated for storing entities and their components in an ECS (Entity Component System).
/// Provides functionality for managing entities and components within the chunk.
/// </summary>
public sealed unsafe class ArchetypeChunk : IDisposable
{
    /// <summary>
    /// The memory alignment used for allocations within the chunk.
    /// </summary>
    private const int MemoryAlignment = 64;

    private readonly Entity* _entities;
    private readonly void** _components;
    private readonly ComponentType[] _componentTypes;

    private bool _isDisposed;

    /// <summary>
    /// Gets the maximum number of entities the chunk can hold.
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    /// Gets or sets the current number of entities stored in the chunk.
    /// </summary>
    public int Count { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchetypeChunk"/> class with the specified capacity and component types.
    /// Allocates memory for entities and components.
    /// </summary>
    /// <param name="capacity">The maximum number of entities the chunk can hold.</param>
    /// <param name="componentTypes">The component types associated with the chunk.</param>
    public ArchetypeChunk(int capacity, ReadOnlySpan<ComponentType> componentTypes)
    {
        _entities = (Entity*)NativeMemory.AlignedAlloc((nuint)(capacity * sizeof(Entity)), MemoryAlignment);
        _components = (void**)NativeMemory.AlignedAlloc((nuint)(componentTypes.Length * sizeof(void*)), MemoryAlignment);

        for (var i = 0; i < componentTypes.Length; i++)
            _components[i] = NativeMemory.AlignedAlloc((nuint)(capacity * componentTypes[i].Size), MemoryAlignment);

        _componentTypes = componentTypes.ToArray();

        Capacity = capacity;
        Count = 0;
    }

    /// <summary>
    /// Finalizer that ensures the chunk is disposed when it is garbage collected.
    /// </summary>
    ~ArchetypeChunk()
    {
        Dispose();
    }

    /// <summary>
    /// Retrieves a span of entities stored in the chunk.
    /// </summary>
    /// <returns>A span of entities currently stored in the chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<Entity> GetEntities()
    {
        return new Span<Entity>(_entities, Count);
    }

    /// <summary>
    /// Retrieves a span of components of the specified type stored in the chunk.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <param name="componentIndexInArchetype">The index of the component type within the archetype.</param>
    /// <returns>A span of components of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetComponents<T>(int componentIndexInArchetype)
        where T : struct, IComponent
    {
        return new Span<T>(_components[componentIndexInArchetype], Count);
    }

    /// <summary>
    /// Retrieves a pointer to the memory block for a specific component type.
    /// </summary>
    /// <param name="componentIndexInArchetype">The index of the component type within the archetype.</param>
    /// <returns>A pointer to the memory block for the specified component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void* GetComponentPtr(int componentIndexInArchetype)
    {
        return _components[componentIndexInArchetype];
    }

    /// <summary>
    /// Copies an entity and its components from a source chunk to this chunk at the specified indices.
    /// </summary>
    /// <param name="sourceChunk">The source chunk to copy from.</param>
    /// <param name="sourceIndex">The index of the entity in the source chunk.</param>
    /// <param name="destinationIndex">The index of the entity in this chunk.</param>
    public void CopyFrom(ArchetypeChunk sourceChunk, int sourceIndex, int destinationIndex)
    {
        _entities[destinationIndex] = sourceChunk._entities[sourceIndex];

        for (var i = 0; i < _componentTypes.Length; i++)
        {
            var size = _componentTypes[i].Size;
            var sourcePtr = (byte*)sourceChunk._components[i] + sourceIndex * size;
            var destPtr = (byte*)_components[i] + destinationIndex * size;
            Unsafe.CopyBlock(destPtr, sourcePtr, (uint)size);
        }
    }

    /// <summary>
    /// Releases the memory allocated for entities and components in the chunk.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        for (var i = 0; i < _componentTypes.Length; i++)
            NativeMemory.AlignedFree(_components[i]);

        NativeMemory.AlignedFree(_components);
        NativeMemory.AlignedFree(_entities);

        GC.SuppressFinalize(this);
    }
}
