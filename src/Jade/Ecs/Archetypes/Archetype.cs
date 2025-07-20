// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Microsoft.Extensions.ObjectPool;

namespace Jade.Ecs.Archetypes;

/// <summary>
/// Represents an archetype in the ECS (Entity Component System) architecture.
/// Manages chunks of entities and their associated components.
/// </summary>
public sealed class Archetype : IDisposable
{
    /// <summary>
    /// Gets the object pool for managing <see cref="ArchetypeChunk"/> instances.
    /// </summary>
    private ObjectPool<ArchetypeChunk> ArchetypeChunkPool { get; }

    /// <summary>
    /// Gets the component mask associated with the archetype.
    /// </summary>
    public ComponentMask Mask { get; }

    /// <summary>
    /// Gets the list of chunks managed by the archetype.
    /// </summary>
    public List<ArchetypeChunk> Chunks { get; }

    /// <summary>
    /// Gets the total number of entities in the archetype.
    /// </summary>
    public int EntityCount { get; private set; }

    /// <summary>
    /// Gets the IDs of the components associated with the archetype.
    /// </summary>
    public int[] ComponentIds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Archetype"/> class.
    /// </summary>
    /// <param name="mask">The component mask defining the archetype.</param>
    public Archetype(ComponentMask mask)
    {
        Mask = mask;
        ComponentIds = Mask.GetComponents();
        ArchetypeChunkPool = new DefaultObjectPool<ArchetypeChunk>(new ArchetypeChunkPolicy(ComponentIds));
        Chunks = [ArchetypeChunkPool.Get()];
    }

    /// <summary>
    /// Finalizer to release unmanaged resources.
    /// </summary>
    ~Archetype()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>
    /// Adds an entity to the archetype.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>
    /// A tuple containing the index of the chunk and the index of the entity within the chunk.
    /// </returns>
    public (int chunkIndex, int indexInChunk) Add(Entity entity)
    {
        var chunk = GetChunkForAdd();
        var indexInChunk = chunk.Add(entity);
        EntityCount++;
        return (Chunks.Count - 1, indexInChunk);
    }

    /// <summary>
    /// Removes an entity from the archetype.
    /// </summary>
    /// <param name="chunkIndex">The index of the chunk containing the entity.</param>
    /// <param name="indexInChunk">The index of the entity within the chunk.</param>
    /// <returns>The entity that was moved to the removed entity's position.</returns>
    public Entity Remove(int chunkIndex, int indexInChunk)
    {
        var chunk = Chunks[chunkIndex];
        var movedEntity = chunk.Remove(indexInChunk);
        EntityCount--;

        if (chunk.Count is 0 && Chunks.Count > 1)
        {
            Chunks.Remove(chunk);
            ArchetypeChunkPool.Return(chunk);
        }

        return movedEntity;
    }

    /// <summary>
    /// Gets the chunk to which a new entity can be added.
    /// </summary>
    /// <returns>The chunk for adding a new entity.</returns>
    private ArchetypeChunk GetChunkForAdd()
    {
        var lastChunk = Chunks[^1];
        if (lastChunk.IsFull)
        {
            lastChunk = ArchetypeChunkPool.Get();
            Chunks.Add(lastChunk);
        }
        return lastChunk;
    }

    /// <summary>
    /// Releases unmanaged resources used by the archetype.
    /// </summary>
    private void ReleaseUnmanagedResources()
    {
        foreach (var chunk in Chunks)
            chunk.Dispose();
    }

    /// <summary>
    /// Disposes the archetype, releasing unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
