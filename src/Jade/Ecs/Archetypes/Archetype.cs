// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions;
using Jade.Ecs.Components;

namespace Jade.Ecs.Archetypes;

internal sealed class Archetype : IDisposable
{
    public ComponentMask Mask { get; }

    public List<ArchetypeChunk> Chunks { get; }

    public int EntityCount { get; private set; }

    public ComponentId[] ComponentIds { get; }

    public Dictionary<ComponentId, Archetype> AddTransitions { get; }

    public Dictionary<ComponentId, Archetype> RemoveTransitions { get; }

    public Archetype(ComponentMask mask)
    {
        Mask = mask;
        ComponentIds = Mask.GetComponents();
        Chunks = [new ArchetypeChunk(ComponentIds)];
        AddTransitions = [];
        RemoveTransitions = [];
    }

    ~Archetype()
    {
        ReleaseUnmanagedResources();
    }

    public (int chunkIndex, int indexInChunk) Add(Entity entity)
    {
        var chunk = GetChunkForAdd();
        var indexInChunk = chunk.Add(entity);
        EntityCount++;
        return (Chunks.Count - 1, indexInChunk);
    }

    public Entity Remove(int chunkIndex, int indexInChunk)
    {
        var chunk = Chunks[chunkIndex];
        var movedEntity = chunk.Remove(indexInChunk);
        EntityCount--;
        return movedEntity;
    }

    private ArchetypeChunk GetChunkForAdd()
    {
        var lastChunk = Chunks[^1];
        if (lastChunk.IsFull)
        {
            lastChunk = new ArchetypeChunk(ComponentIds);
            Chunks.Add(lastChunk);
        }
        return lastChunk;
    }

    private void ReleaseUnmanagedResources()
    {
        foreach (var chunk in Chunks)
            chunk.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
