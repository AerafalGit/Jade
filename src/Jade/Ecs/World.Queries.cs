// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs;

public sealed partial class World
{
    public Query Query()
    {
        return new Query(this);
    }

    public Entity FirstOrDefault()
    {
        return Query().FirstOrDefault();
    }

    public Entity FirstOrDefault(Func<Entity, bool> predicate)
    {
        return Query().FirstOrDefault(predicate);
    }

    public int Count()
    {
        return Query().Count();
    }

    public int Count(Func<Entity, bool> predicate)
    {
        return Query().Count(predicate);
    }

    public bool Any()
    {
        return Query().Any();
    }

    public bool Any(Func<Entity, bool> predicate)
    {
        return Query().Any(predicate);
    }

    public IEnumerable<Entity> GetEntities()
    {
        return Query().ToList();
    }

    public IEnumerable<Entity> GetEntities(Func<Entity, bool> predicate)
    {
        return Query().Where(predicate);
    }

    internal IEnumerable<Archetype> GetMatchingArchetypes(in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask)
    {
        return QueryCache.GetMatchingArchetypes(in allMask, in anyMask, in noneMask);
    }

    internal IEnumerable<Archetype> GetArchetypesWith(ComponentMask allMask, ComponentMask anyMask, ComponentMask noneMask)
    {
        foreach (var (mask, archetype) in _archetypes)
        {
            if (!allMask.IsEmpty && !mask.HasAll(allMask))
                continue;

            if (!anyMask.IsEmpty && !mask.HasAny(anyMask))
                continue;

            if (!noneMask.IsEmpty && mask.HasAny(noneMask))
                continue;

            yield return archetype;
        }
    }
}
