// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

internal sealed class QueryCache
{
    private readonly Dictionary<QueryKey, QueryCached> _cache;
    private readonly ArchetypeGraph _graph;

    private ulong _version;

    public QueryCache(ArchetypeGraph graph)
    {
        _cache = [];
        _graph = graph;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InvalidateCache()
    {
        Interlocked.Increment(ref _version);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<Archetype> GetMatchingArchetypes(ComponentMask allMask, ComponentMask anyMask, ComponentMask noneMask)
    {
        var key = new QueryKey(allMask, anyMask, noneMask);
        var currentVersion = _version;

        if (_cache.TryGetValue(key, out var cached) && cached.IsValid(currentVersion))
            return cached.MatchingArchetypes;

        var archetypes = _graph.GetArchetypesWith(allMask, anyMask, noneMask);

        if (cached is not null)
            cached.Update(archetypes, currentVersion);
        else
            _cache[key] = cached = new QueryCached(archetypes, currentVersion);

        return cached.MatchingArchetypes;
    }

    public void Clear()
    {
        _cache.Clear();
    }
}
