// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions;
using Jade.Ecs.Archives;
using Jade.Ecs.Events;
using Jade.Ecs.Prefabs;
using Jade.Ecs.Relations;

namespace Jade.Ecs;

public sealed partial class World : IDisposable
{
    private readonly Dictionary<uint, EntityLocation> _locations;
    private readonly Dictionary<Type, object> _resources;
    private readonly List<uint> _versions;
    private readonly Queue<uint> _recycledIds;

    private uint _nextId;
    private ulong _structuralVersion;

    private RelationGraph RelationGraph { get; }

    private PrefabRegistry PrefabRegistry { get; }

    private EventBus EventBus { get; }

    internal Archive Archive { get; }

    internal int EntityCount { get; private set; }

    internal ulong StructuralVersion =>
        _structuralVersion;

    public World()
    {
        Archive = new Archive(this);
        RelationGraph = new RelationGraph();
        PrefabRegistry = new PrefabRegistry(this);
        EventBus = new EventBus();

        _locations = [];
        _resources = [];
        _versions = [0];
        _recycledIds = [];
        _nextId = 1;
        _structuralVersion = 1;
    }

    ~World()
    {
        ReleaseUnmanagedResources();
    }

    internal void InvalidateStructuralVersion()
    {
        Interlocked.Increment(ref _structuralVersion);
    }

    private void ReleaseUnmanagedResources()
    {
        Archive.Dispose();
        RelationGraph.Dispose();

        foreach (var resource in _resources.Values)
        {
            if (resource is IDisposable disposable)
                disposable.Dispose();
        }

        _locations.Clear();
        _resources.Clear();
        _versions.Clear();
        _recycledIds.Clear();
        _nextId = 1;

        EntityCount = 0;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
