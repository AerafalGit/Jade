// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs.Archives;

internal sealed class Archive : IDisposable
{
    private const float DensityToArchetypeThreshold = 0.15f; // 15% threshold for migrating to Archetype

    private const float DensityToSparseSetThreshold = 0.05f; // 5% threshold for migrating to SparseSet

    private const int MinEntitiesForAnalysis = 256; // Minimum entities to analyze for migration

    private readonly World _world;
    private readonly Dictionary<ComponentId, SparseSet> _sparseSets;
    private readonly Dictionary<ComponentId, ArchiveType> _storageStrategy;

    public ArchetypeGraph Graph { get; }

    public Archive(World world)
    {
        Graph = new ArchetypeGraph();

        _world = world;
        _sparseSets = [];
        _storageStrategy = [];
    }

    ~Archive()
    {
        ReleaseUnmanagedResources();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype GetOrCreateArchetype(in ComponentMask mask)
    {
        return Graph.GetOrCreateArchetype(mask);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArchiveType GetStrategy(ComponentId componentId)
    {
        if (_storageStrategy.TryGetValue(componentId, out var strategy))
            return strategy;

        var metadata = ComponentRegistry.GetMetadata(componentId);

        if (metadata.Size > 0)
            _sparseSets.TryAdd(componentId, new SparseSet(metadata.Size, metadata.Alignment));

        return _storageStrategy[componentId] = ArchiveType.SparseSet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseSet? GetSparseSet(ComponentId type)
    {
        return _sparseSets.GetValueOrDefault(type);
    }

    public void Maintain()
    {
        if (_world.EntityCount < MinEntitiesForAnalysis)
            return;

        foreach (var componentId in ComponentRegistry.GetAllComponentIds())
        {
            var meta = ComponentRegistry.GetMetadata(componentId);

            if(meta.Size is 0)
                continue;

            var currentStrategy = GetStrategy(componentId);
            var componentEntityCount = GetComponentEntityCount(componentId);
            var density = (float)componentEntityCount / _world.EntityCount;

            switch (currentStrategy)
            {
                case ArchiveType.SparseSet when density > DensityToArchetypeThreshold:
                    MigrateToArchetype(componentId);
                    break;
                case ArchiveType.Archetype when density < DensityToSparseSetThreshold:
                    MigrateToSparseSet(componentId);
                    break;
            }
        }
    }

    private void MigrateToArchetype(in ComponentId componentId)
    {
        if (!_sparseSets.Remove(componentId, out var sparseSet))
            return;

        var entitiesToMigrate = sparseSet.Entities.ToArray();

        foreach (var entity in entitiesToMigrate)
            _world.AddComponentDataToArchetype(entity, componentId, sparseSet.GetAsBytes(entity));

        sparseSet.Dispose();

        _storageStrategy[componentId] = ArchiveType.Archetype;
    }

    private void MigrateToSparseSet(in ComponentId componentId)
    {
        var metadata = ComponentRegistry.GetMetadata(componentId);
        var sparseSet = new SparseSet(metadata.Size, metadata.Alignment);
        var mask = new ComponentMask().With(componentId);
        var archetypes = Graph.GetArchetypesWith(mask);

        foreach (var archetype in archetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks.ToArray())
            {
                var entitiesToMigrate = archetypeChunk.Entities.ToArray();
                var componentArray = archetypeChunk.GetArray(componentId);

                for (var i = 0; i < entitiesToMigrate.Length; i++)
                {
                    var entity = entitiesToMigrate[i];
                    var componentData = componentArray.GetAsBytes(i);
                    sparseSet.AddFromBytes(entity, componentData);
                    _world.RemoveComponentFromArchetype(entity, componentId);
                }
            }
        }

        _sparseSets[componentId] = sparseSet;
        _storageStrategy[componentId] = ArchiveType.SparseSet;
    }

    private int GetComponentEntityCount(in ComponentId id)
    {
        var strategy = GetStrategy(id);

        if (strategy is ArchiveType.SparseSet)
            return _sparseSets.TryGetValue(id, out var set) ? set.Count : 0;

        var mask = new ComponentMask().With(id);

        return Graph
            .GetArchetypesWith(mask)
            .Sum(static a => a.EntityCount);
    }

    private void ReleaseUnmanagedResources()
    {
        foreach (var set in _sparseSets.Values)
            set.Dispose();

        Graph.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}

