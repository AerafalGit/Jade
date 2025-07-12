// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Archives;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    private readonly List<Func<Entity, bool>> _filters;
    private readonly World _world;

    private ComponentMask _all;
    private ComponentMask _any;
    private ComponentMask _none;

    public Query(World world)
    {
        _world = world;
        _all = new ComponentMask();
        _any = new ComponentMask();
        _none = new ComponentMask();
        _filters = [];
    }

    public readonly Entity Single()
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        if (matchingArchetypes.Count != 1)
            throw new InvalidOperationException("Query must match exactly one archetype.");

        var archetype = matchingArchetypes[0];

        if (archetype.Chunks.Count != 1 || archetype.Chunks[0].Count != 1)
            throw new InvalidOperationException("Query must match exactly one entity.");

        var entity = archetype.Chunks[0].Entities[0];

        return !_filters.All(f => f(entity))
            ? Entity.Null
            : entity;
    }

    public readonly ref T Single<T>()
        where T : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId = Component<T>.Metadata.Id;
        var strategy = storage.GetStrategy(componentId);

        if (matchingArchetypes.Count is not 1)
            throw new InvalidOperationException("Query must match exactly one archetype.");

        var archetype = matchingArchetypes[0];

        if (archetype.Chunks.Count is not 1 || archetype.Chunks[0].Count is not 1)
            throw new InvalidOperationException("Query must match exactly one entity.");

        var chunk = archetype.Chunks[0];

        var entities = chunk.Entities;
        var componentArray = strategy is ArchiveType.Archetype ? chunk.GetArray<T>() : null!;
        var sparseSet = strategy is ArchiveType.SparseSet ? storage.GetSparseSet(componentId) : null!;

        var entity = entities[0];

        if (!_filters.All(f => f(entity)))
            return ref Unsafe.NullRef<T>();

        return ref strategy is ArchiveType.Archetype ? ref componentArray.Get(0) : ref sparseSet!.Get<T>(entity);
    }

    private readonly ReadOnlySpan<Entity> GetMatchingEntities(ArchetypeChunk chunk)
    {
        if (_filters.Count is 0)
            return chunk.Entities;

        if (chunk.Count is 0)
            return [];

        if (chunk.Count <= ComponentMask.MaxComponents)
        {
            Span<Entity> buffer = stackalloc Entity[chunk.Count];

            var count = 0;

            foreach (var entity in chunk.Entities)
            {
                if (_filters.All(f => f(entity)))
                    buffer[count++] = entity;
            }

            return buffer[..count].ToArray();
        }

        var entities = new List<Entity>(chunk.Count);

        foreach (var entity in chunk.Entities)
        {
            if (_filters.All(f => f(entity)))
                entities.Add(entity);
        }

        return CollectionsMarshal.AsSpan(entities);
    }
}
