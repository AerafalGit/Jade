// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;
using Jade.Ecs.Relations;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands Spawn()
    {
        return new EntityCommands(this, CreateEntity());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity()
    {
        return CreateEntity(null);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DestroyEntity(in Entity entity)
    {
        DestroyEntityRecursive(entity, []);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive(in Entity entity)
    {
        return entity.IsValid && entity.Id < _versions.Count && entity.Version == _versions[(int)entity.Id];
    }

    internal Entity CreateEntity(Archetype? archetype)
    {
        archetype ??= Archive.Graph.Root;

        var id = _recycledIds.TryDequeue(out var recycledId)
            ? recycledId
            : _nextId++;

        if (id >= _versions.Count)
        {
            while (id >= _versions.Count)
                _versions.Add(0);
        }

        var newVersion = _versions[(int)id] + 1;

        if (newVersion is 0)
            newVersion = 1;

        _versions[(int)id] = newVersion;

        var entity = new Entity(id, newVersion);

        var (chunkIndex, indexInChunk) = archetype.Add(entity);

        _locations[id] = new EntityLocation(archetype, chunkIndex, indexInChunk);

        EntityCount++;
        Interlocked.Increment(ref _structuralVersion);
        return entity;
    }

    internal bool TryGetEntityLocation(in Entity entity, out EntityLocation location)
    {
        location = default;

        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out location);
    }

    internal void AddComponentDataToArchetype(in Entity entity, ComponentId componentId, ReadOnlySpan<byte> data)
    {
        Transition(entity, componentId, archetype => archetype.AddTransitions.GetValueOrDefault(componentId));

        var location = _locations[entity.Id];
        var array = location.Archetype.Chunks[location.ChunkIndex].GetArray(componentId);
        array.SetFromBytes(location.IndexInChunk, data);
    }

    internal void RemoveComponentFromArchetype(in Entity entity, ComponentId componentId)
    {
        Transition(entity, componentId, archetype => archetype.RemoveTransitions.GetValueOrDefault(componentId));
    }

    private void DestroyEntityRecursive(Entity entity, HashSet<Entity> visited)
    {
        if (!IsAlive(entity) || !visited.Add(entity))
            return;

        RelationGraph.RemoveAllRelations(entity);

        var dependentEntities = RelationGraph.GetIncomingRelations(RelationProperty.DependsOn, entity).ToArray();

        foreach (var dependent in dependentEntities)
            DestroyEntityRecursive(dependent, visited);

        var id = entity.Id;

        if (_locations.Remove(id, out var location))
        {
            var movedEntity = location.Archetype.Remove(location.ChunkIndex, location.IndexInChunk);

            if(movedEntity.IsValid)
                _locations[movedEntity.Id] = new EntityLocation(location.Archetype, location.ChunkIndex, location.IndexInChunk);
        }

        var newVersion = _versions[(int)id] + 1;

        if (newVersion is 0)
            newVersion = 1;

        _versions[(int)id] = newVersion;
        _recycledIds.Enqueue(id);

        EntityCount--;
        Interlocked.Increment(ref _structuralVersion);
    }

    private void Transition<T>(in Entity entity, Func<Archetype, Archetype?> transitionFn, in T newComponent)
        where T : unmanaged, IComponent
    {
        if (!_locations.TryGetValue(entity.Id, out var oldLocation))
            return;

        var targetArchetype = transitionFn(oldLocation.Archetype);

        if (targetArchetype is null)
        {
            var newMask = oldLocation.Archetype.Mask.With(Component<T>.Metadata.Id);
            targetArchetype = Archive.GetOrCreateArchetype(newMask);
        }

        if (targetArchetype == oldLocation.Archetype)
            return;

        var newLocation = MoveEntityToNewArchetype(entity, oldLocation, targetArchetype);

        newLocation.Archetype.Chunks[newLocation.ChunkIndex]
            .GetArray<T>()
            .Set(newLocation.IndexInChunk, in newComponent);
    }

    private void Transition(in Entity entity, in ComponentId componentId, Func<Archetype, Archetype?> transitionFn)
    {
        if (!_locations.TryGetValue(entity.Id, out var oldLocation))
            return;

        var targetArchetype = transitionFn(oldLocation.Archetype);

        if (targetArchetype is null)
        {
            var newMask = oldLocation.Archetype.Mask.Without(componentId);
            targetArchetype = Archive.GetOrCreateArchetype(newMask);
        }

        if (targetArchetype == oldLocation.Archetype)
            return;

        MoveEntityToNewArchetype(entity, oldLocation, targetArchetype);
    }

    private EntityLocation MoveEntityToNewArchetype(in Entity entity, in EntityLocation oldLocation, Archetype targetArchetype)
    {
        var (newChunkIndex, newIndexInChunk) = targetArchetype.Add(entity);
        var newLocation = new EntityLocation(targetArchetype, newChunkIndex, newIndexInChunk);
        var newChunk = newLocation.Archetype.Chunks[newChunkIndex];
        var oldChunk = oldLocation.Archetype.Chunks[oldLocation.ChunkIndex];

        foreach (var componentType in targetArchetype.ComponentIds)
        {
            if (oldLocation.Archetype.Mask.Has(componentType))
            {
                var sourceArray = oldChunk.GetArray(componentType);
                var targetArray = newChunk.GetArray(componentType);
                sourceArray.MoveTo(oldLocation.IndexInChunk, targetArray, newIndexInChunk);
            }
        }

        var movedEntity = oldLocation.Archetype.Remove(oldLocation.ChunkIndex, oldLocation.IndexInChunk);

        if (movedEntity.IsValid)
            _locations[movedEntity.Id] = new EntityLocation(oldLocation.Archetype, oldLocation.ChunkIndex, oldLocation.IndexInChunk);

        _locations[entity.Id] = newLocation;
        return newLocation;
    }
}
