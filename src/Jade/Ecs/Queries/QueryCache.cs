// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

internal sealed class QueryCache
{
    private readonly ConcurrentDictionary<QueryHash, QueryCached> _cache;
    private readonly World _world;

    private long _version;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryCache(World world)
    {
        _cache = [];
        _world = world;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InvalidateCache()
    {
        Interlocked.Increment(ref _version);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Archetype> GetMatchingArchetypes(in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask)
    {
        var key = new QueryHash(allMask, anyMask, noneMask);
        var currentVersion = Interlocked.Read(ref _version);

        if (_cache.TryGetValue(key, out var cached) && cached.IsValid(currentVersion))
            return cached.GetMatchingArchetypes();

        return GetOrComputeArchetypes(key, allMask, anyMask, noneMask, currentVersion);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private IEnumerable<Archetype> GetOrComputeArchetypes(in QueryHash key, in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask, long currentVersion)
    {
        var archetypes = _world.GetArchetypesWith(allMask, anyMask, noneMask).ToArray();

        var cached = _cache.AddOrUpdate(
            key,
            _ => new QueryCached(archetypes, currentVersion),
            (_, existing) =>
            {
                if (existing.IsValid(currentVersion))
                    return existing;

                existing.Update(archetypes, currentVersion);
                return existing;
            });

        return cached.GetMatchingArchetypes();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _cache.Clear();
        Interlocked.Increment(ref _version);
    }

    public void CleanupStaleEntries()
    {
        var currentVersion = Interlocked.Read(ref _version);
        var staleKeys = new List<QueryHash>();

        foreach (var kvp in _cache)
        {
            if (!kvp.Value.IsValid(currentVersion))
                staleKeys.Add(kvp.Key);
        }

        foreach (var key in staleKeys)
            _cache.TryRemove(key, out _);
    }
}
