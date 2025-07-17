// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    public readonly Entity FirstOrDefault()
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype)
            {
                if (chunk.Count > 0)
                    return chunk.Entities[0];
            }
        }

        return Entity.Null;
    }

    public readonly Entity FirstOrDefault(Func<Entity, bool> predicate)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype)
            {
                foreach (var entity in chunk.Entities)
                {
                    if (predicate(entity))
                        return entity;
                }
            }
        }

        return Entity.Null;
    }

    public readonly int Count()
    {
        return _world.GetMatchingArchetypes(in _all, in _any, in _none).Sum(static x => x.EntityCount);
    }

    public readonly int Count(Func<Entity, bool> predicate)
    {
        var count = 0;

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype)
            {
                foreach (var entity in chunk.Entities)
                {
                    if (predicate(entity))
                        count++;
                }
            }
        }

        return count;
    }

    public readonly bool Any()
    {
        return Count() > 0;
    }

    public readonly bool Any(Func<Entity, bool> predicate)
    {
        return Count(predicate) > 0;
    }

    public readonly List<Entity> ToList()
    {
        var entities = new List<Entity>();

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype)
                entities.AddRange(chunk.Entities);
        }

        return entities;
    }

    public readonly List<Entity> Where(Func<Entity, bool> predicate)
    {
        var entities = new List<Entity>();

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype)
            {
                foreach (var entity in chunk.Entities)
                {
                    if (predicate(entity))
                        entities.Add(entity);
                }
            }
        }

        return entities;
    }
}
