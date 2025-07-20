// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world.
/// Manages entities, archetypes, and query caching.
/// </summary>
public sealed class World : IDisposable
{
    private static readonly Lazy<World> s_instance = new(() => new World(), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the singleton instance of the <see cref="World"/> class.
    /// </summary>
    public static World Instance =>
        s_instance.Value;

    private readonly Dictionary<ComponentMask, Archetype> _archetypes;
    private readonly Dictionary<uint, EntityLocation> _locations;
    private readonly List<uint> _versions;
    private readonly Queue<uint> _recycledIds;
    private readonly Lock _archetypeLock;
    private readonly Lock _entityLock;

    private volatile bool _cacheNeedsInvalidation;
    private uint _nextId;

    /// <summary>
    /// The query cache for managing cached queries.
    /// </summary>
    private QueryCache QueryCache { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="World"/> class.
    /// </summary>
    public World()
    {
        _archetypes = [];
        _locations = [];
        _versions = [0];
        _recycledIds = [];
        _archetypeLock = new Lock();
        _entityLock = new Lock();

        QueryCache = new QueryCache(this);
    }

    /// <summary>
    /// Finalizer to release unmanaged resources.
    /// </summary>
    ~World()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>
    /// Retrieves the archetypes that match the specified component masks using the query cache.
    /// </summary>
    /// <param name="allMask">The component mask specifying components that must be present.</param>
    /// <param name="anyMask">The component mask specifying components that may be present.</param>
    /// <param name="noneMask">The component mask specifying components that must not be present.</param>
    /// <returns>An enumerable of archetypes matching the specified masks.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal IEnumerable<Archetype> GetMatchingArchetypes(in ComponentMask allMask, in ComponentMask anyMask, in ComponentMask noneMask)
    {
        return QueryCache.GetMatchingArchetypes(in allMask, in anyMask, in noneMask);
    }

    /// <summary>
    /// Retrieves the archetypes that match the specified component masks by iterating through all archetypes.
    /// </summary>
    /// <param name="allMask">The component mask specifying components that must be present.</param>
    /// <param name="anyMask">The component mask specifying components that may be present.</param>
    /// <param name="noneMask">The component mask specifying components that must not be present.</param>
    /// <returns>An enumerable of archetypes matching the specified masks.</returns>
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

    /// <summary>
    /// Flushes the query cache if invalidation is required.
    /// </summary>
    internal void FlushCache()
    {
        if (!_cacheNeedsInvalidation)
            return;

        _cacheNeedsInvalidation = false;

        QueryCache.InvalidateCache();
    }

    /// <summary>
    /// Marks the query cache for invalidation.
    /// </summary>
    private void InvalidateCache()
    {
        _cacheNeedsInvalidation = true;
    }

    /// <summary>
    /// Releases unmanaged resources used by the ECS world.
    /// </summary>
    private void ReleaseUnmanagedResources()
    {
        lock (_entityLock)
        {
            _locations.Clear();
            _versions.Clear();
            _recycledIds.Clear();
            _nextId = Interlocked.Exchange(ref _nextId, 0);
        }

        lock (_archetypeLock)
        {
            foreach (var archetype in _archetypes.Values)
                archetype.Dispose();

            _archetypes.Clear();
        }

        QueryCache.Clear();
    }

    /// <summary>
    /// Disposes the ECS world, releasing unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
