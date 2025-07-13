// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs.Archetypes;

internal sealed class ArchetypeGraph : IDisposable
{
    private readonly Dictionary<ComponentMask, Archetype> _archetypes;

    public Archetype Root { get; }

    public QueryCache QueryCache { get; }

    public ArchetypeGraph()
    {
        _archetypes = [];

        QueryCache = new QueryCache(this);
        Root = GetOrCreateArchetype(new ComponentMask());
    }

    ~ArchetypeGraph()
    {
        ReleaseUnmanagedResources();
    }

    public Archetype GetOrCreateArchetype(in ComponentMask mask)
    {
        if (_archetypes.TryGetValue(mask, out var archetype))
            return archetype;

        var newArchetype = new Archetype(mask);
        BuildEdges(newArchetype);
        _archetypes[mask] = newArchetype;

        QueryCache.InvalidateCache();

        return newArchetype;
    }

    public IEnumerable<Archetype> GetArchetypesWith(ComponentMask mask)
    {
        return _archetypes.Values.Where(archetype => archetype.Mask.HasAll(mask));
    }

    public IEnumerable<Archetype> GetArchetypesWith(ComponentMask allMask, ComponentMask anyMask, ComponentMask noneMask)
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

    private void BuildEdges(in Archetype newArchetype)
    {
        foreach (var existingArchetype in _archetypes.Values)
        {
            if (existingArchetype == newArchetype)
                continue;

            TryCreateEdge(newArchetype, existingArchetype);
            TryCreateEdge(existingArchetype, newArchetype);
        }
    }

    private static void TryCreateEdge(in Archetype from, in Archetype to)
    {
        var fromMask = from.Mask;
        var toMask = to.Mask;

        if (!toMask.HasAll(in fromMask))
            return;

        var diffMask = toMask & ~fromMask;

        var popCount = diffMask.PopCount();

        if (popCount is 1)
        {
            var componentId = diffMask.FirstSetBit();

            if (!componentId.IsRelation)
            {
                from.AddTransitions[componentId] = to;
                to.RemoveTransitions[componentId] = from;
            }
        }
    }

    private void ReleaseUnmanagedResources()
    {
        Root.Dispose();

        foreach (var archetype in _archetypes.Values)
            archetype.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
