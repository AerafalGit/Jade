// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Components;
using Microsoft.Extensions.ObjectPool;

namespace Jade.Ecs.Archetypes;

public sealed class Archetype : IDisposable
{
    public ObjectPool<ArchetypeChunk> ArchetypeChunkPool { get; }

    public ComponentMask Mask { get; }

    public List<ArchetypeChunk> Chunks { get; }

    public int EntityCount { get; private set; }

    public ComponentId[] ComponentIds { get; }

    public Archetype(ComponentMask mask)
    {
        Mask = mask;
        ComponentIds = Mask.GetComponents();
        ArchetypeChunkPool = new DefaultObjectPool<ArchetypeChunk>(new ArchetypeChunkPolicy(ComponentIds));
        Chunks = [ArchetypeChunkPool.Get()];
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

        if (chunk.Count is 0 && Chunks.Count > 1)
        {
            Chunks.Remove(chunk);
            ArchetypeChunkPool.Return(chunk);
        }

        return movedEntity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<ArchetypeChunk> GetEnumerator()
    {
        return Chunks.GetEnumerator();
    }

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
