// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Components;
using Jade.Ecs.Entities;

namespace Jade.Ecs.Archetypes;

/// <summary>
/// Represents an archetype in the ECS (Entity Component System), which defines a specific layout of components.
/// Manages chunks of entities and their associated components.
/// </summary>
public sealed class Archetype : IDisposable
{
    /// <summary>
    /// The size of each chunk in bytes.
    /// </summary>
    private const int ChunkSizeInBytes = 16 * 1024;

    private readonly List<ArchetypeChunk> _chunks;
    private readonly int _chunkCapacity;

    /// <summary>
    /// The mask representing the components included in this archetype.
    /// </summary>
    public ComponentMask Mask { get; }

    /// <summary>
    /// The array of component types associated with this archetype.
    /// </summary>
    public ComponentType[] ComponentTypes { get; }

    /// <summary>
    /// The total number of entities stored in this archetype.
    /// </summary>
    public int EntityCount { get; private set; }

    /// <summary>
    /// Gets the number of chunks associated with this archetype.
    /// </summary>
    public int ChunkCount =>
        _chunks.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="Archetype"/> class with the specified component mask and types.
    /// </summary>
    /// <param name="mask">The mask representing the components included in this archetype.</param>
    /// <param name="componentTypes">The component types associated with this archetype.</param>
    public Archetype(ComponentMask mask, ReadOnlySpan<ComponentType> componentTypes)
    {
        var entitySizeInChunk = Unsafe.SizeOf<Entity>() + Sum(ref componentTypes);

        _chunks = [];
        _chunkCapacity = entitySizeInChunk > 0 ? ChunkSizeInBytes / entitySizeInChunk : 0;

        if (_chunkCapacity is 0 && entitySizeInChunk > 0)
            _chunkCapacity = 1;

        Mask = mask;
        ComponentTypes = componentTypes.ToArray();
    }

    /// <summary>
    /// Finalizer that ensures the archetype is disposed when it is garbage collected.
    /// </summary>
    ~Archetype()
    {
        Dispose();
    }

    /// <summary>
    /// Adds an entity to the archetype and returns its location.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The location of the added entity within the archetype.</returns>
    public EntityLocation AddEntity(Entity entity)
    {
        var chunk = GetOrCreateChunkWithSpace();
        var indexInChunk = chunk.Count;

        chunk.Count++;
        chunk.GetEntities()[indexInChunk] = entity;
        EntityCount++;

        return new EntityLocation(this, _chunks.Count - 1, indexInChunk);
    }

    /// <summary>
    /// Removes an entity from the archetype and returns information about the moved entity, if applicable.
    /// </summary>
    /// <param name="location">The location of the entity to remove.</param>
    /// <returns>
    /// A tuple containing the moved entity and its new location, or <c>null</c> if no entity was moved.
    /// </returns>
    public (Entity movedEntity, EntityLocation newLocation)? RemoveEntity(in EntityLocation location)
    {
        var chunkToRemoveFrom = _chunks[location.ChunkIndex];
        var indexToRemove = location.IndexInChunk;

        EntityCount--;

        var lastChunk = _chunks[^1];
        var lastIndex = lastChunk.Count - 1;

        (Entity movedEntity, EntityLocation newLocation)? movedEntityInfo = null;

        if (location.ChunkIndex != _chunks.Count - 1 || indexToRemove != lastIndex)
        {
            chunkToRemoveFrom.CopyFrom(lastChunk, lastIndex, indexToRemove);

            var entityThatMoved = chunkToRemoveFrom.GetEntities()[indexToRemove];

            movedEntityInfo = (entityThatMoved, new EntityLocation(this, location.ChunkIndex, indexToRemove));
        }

        lastChunk.Count--;

        if (lastChunk.Count is 0 && _chunks.Count > 1)
        {
            lastChunk.Dispose();
            _chunks.RemoveAt(_chunks.Count - 1);
        }

        return movedEntityInfo;
    }

    /// <summary>
    /// Retrieves the list of chunks associated with this archetype.
    /// </summary>
    /// <returns>The list of chunks.</returns>
    public List<ArchetypeChunk> GetChunks()
    {
        return _chunks;
    }

    /// <summary>
    /// Retrieves a specific chunk by its index.
    /// </summary>
    /// <param name="chunkIndex">The index of the chunk to retrieve.</param>
    /// <returns>The chunk at the specified index.</returns>
    public ArchetypeChunk GetChunk(int chunkIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(chunkIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(chunkIndex, _chunks.Count);

        return _chunks[chunkIndex];
    }

    /// <summary>
    /// Retrieves the index of a component type by its generic type parameter.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The index of the component type, or -1 if not found.</returns>
    public int GetComponentIndex<T>()
        where T : struct, IComponent
    {
        return GetComponentIndex(ComponentRegistry.GetMetadata<T>().Id);
    }

    /// <summary>
    /// Retrieves the index of a component type by its type.
    /// </summary>
    /// <param name="type">The type of the component.</param>
    /// <returns>The index of the component type, or -1 if not found.</returns>
    public int GetComponentIndex(Type type)
    {
        return GetComponentIndex(ComponentRegistry.GetMetadata(type).Id);
    }

    /// <summary>
    /// Retrieves the index of a component type by its unique identifier.
    /// </summary>
    /// <param name="componentId">The unique identifier of the component type.</param>
    /// <returns>The index of the component type, or -1 if not found.</returns>
    public int GetComponentIndex(int componentId)
    {
        for (var i = 0; i < ComponentTypes.Length; i++)
        {
            if (ComponentTypes[i].Id == componentId)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Retrieves or creates a chunk with available space for entities.
    /// </summary>
    /// <returns>The chunk with available space.</returns>
    private ArchetypeChunk GetOrCreateChunkWithSpace()
    {
        if (_chunks.Count is 0 || _chunks[^1].Count >= _chunkCapacity)
            _chunks.Add(new ArchetypeChunk(_chunkCapacity, ComponentTypes));

        return _chunks[^1];
    }

    /// <summary>
    /// Disposes the archetype by releasing all associated chunks.
    /// </summary>
    public void Dispose()
    {
        foreach (var chunk in _chunks)
            chunk.Dispose();

        _chunks.Clear();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Computes the sum of the sizes of all component types.
    /// </summary>
    /// <param name="componentTypes">The span of component types.</param>
    /// <returns>The total size of all component types.</returns>
    private static int Sum(ref ReadOnlySpan<ComponentType> componentTypes)
    {
        var sum = 0;

        foreach (var type in componentTypes)
            sum += type.Size;

        return sum;
    }
}
