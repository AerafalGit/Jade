// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a cache for ECS (Entity Component System) queries.
/// Stores cached query results and provides methods for invalidation and retrieval.
/// </summary>
internal sealed class QueryCache
{
    private readonly ConcurrentDictionary<QueryHash, QueryCached> _cache;
    private readonly World _world;
    private long _version;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryCache"/> class.
    /// </summary>
    /// <param name="world">The ECS world associated with this query cache.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryCache(World world)
    {
        _cache = [];
        _world = world;
    }

    /// <summary>
    /// Invalidates the query cache by incrementing its version.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InvalidateCache()
    {
        Interlocked.Increment(ref _version);
    }

    /// <summary>
    /// Retrieves the archetypes that match the specified component masks from the cache.
    /// Computes and caches the result if not already cached.
    /// </summary>
    /// <param name="allMask">The component mask specifying components that must be present.</param>
    /// <param name="anyMask">The component mask specifying components that may be present.</param>
    /// <param name="noneMask">The component mask specifying components that must not be present.</param>
    /// <returns>An enumerable of archetypes matching the specified masks.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Archetype> GetMatchingArchetypes(in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask)
    {
        var key = new QueryHash(allMask, anyMask, noneMask);
        var currentVersion = Interlocked.Read(ref _version);

        if (_cache.TryGetValue(key, out var cached) && cached.IsValid(currentVersion))
            return cached.GetMatchingArchetypes();

        return GetOrComputeArchetypes(key, allMask, anyMask, noneMask, currentVersion);
    }

    /// <summary>
    /// Computes the archetypes matching the specified component masks and updates the cache.
    /// </summary>
    /// <param name="hash">The hash representing the query.</param>
    /// <param name="allMask">The component mask specifying components that must be present.</param>
    /// <param name="anyMask">The component mask specifying components that may be present.</param>
    /// <param name="noneMask">The component mask specifying components that must not be present.</param>
    /// <param name="currentVersion">The current version of the query cache.</param>
    /// <returns>An enumerable of archetypes matching the specified masks.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private IEnumerable<Archetype> GetOrComputeArchetypes(in QueryHash hash, in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask, long currentVersion)
    {
        var archetypes = _world.GetArchetypesWith(allMask, anyMask, noneMask);

        var cached = _cache.AddOrUpdate(
            hash,
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

    /// <summary>
    /// Clears the query cache and invalidates it by incrementing its version.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _cache.Clear();
        Interlocked.Increment(ref _version);
    }
}
