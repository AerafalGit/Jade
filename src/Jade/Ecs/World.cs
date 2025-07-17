// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs;

public sealed partial class World : IDisposable
{
    private static readonly Lazy<World> s_instance = new(() => new World(), LazyThreadSafetyMode.ExecutionAndPublication);

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

    private QueryCache QueryCache { get; }

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

    ~World()
    {
        ReleaseUnmanagedResources();
    }

    public WorldStatistics GetStatistics()
    {
        lock (_archetypeLock)
        {
            var archetypeCount = _archetypes.Count;
            var totalChunks = _archetypes.Values.Sum(static a => a.Chunks.Count);
            var totalEntities = _archetypes.Values.Sum(static a => a.EntityCount);

            return new WorldStatistics(archetypeCount, totalChunks, totalEntities, _recycledIds.Count);
        }
    }

    internal void FlushCache()
    {
        if (!_cacheNeedsInvalidation)
            return;

        _cacheNeedsInvalidation = false;

        QueryCache.InvalidateCache();
    }

    private void InvalidateCache()
    {
        _cacheNeedsInvalidation = true;
    }

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

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
