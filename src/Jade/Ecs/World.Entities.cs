// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs;

public sealed partial class World
{
    public void Despawn(in Entity entity)
    {
        ValidateEntity(in entity);

        lock (_entityLock)
        {
            if (_locations.TryGetValue(entity.Id, out var location))
            {
                var movedEntity = location.Archetype.Remove(location.ChunkIndex, location.IndexInChunk);

                if (IsAlive(movedEntity) && _locations.TryGetValue(movedEntity.Id, out var movedLocation))
                    _locations[movedEntity.Id] = new EntityLocation(movedLocation.Archetype, movedLocation.ChunkIndex, location.IndexInChunk);

                _locations.Remove(entity.Id);
            }

            _recycledIds.Enqueue(entity.Id);
        }

        InvalidateCache();
    }

    public Entity Spawn()
    {
        return Spawn(in ComponentMask.Empty);
    }

    public Entity Spawn(in ComponentMask mask)
    {
        uint id;
        uint version;

        lock (_entityLock)
        {
            id = _recycledIds.TryDequeue(out var recycled)
                ? recycled
                : Interlocked.Increment(ref _nextId);

            while (id >= _versions.Count)
                _versions.Add(0);

            version = ++_versions[(int)id];

            if (version is 0)
                version = ++_versions[(int)id];
        }

        var entity = new Entity(id, version);
        var archetype = GetOrCreateArchetype(in mask);

        var (chunkIndex, indexInChunk) = archetype.Add(entity);

        lock (_entityLock)
        {
            _locations[id] = new EntityLocation(archetype, chunkIndex, indexInChunk);
        }

        return entity;
    }

    public Entity Spawn<T1>(in T1? component = default)
    {
        var entity = Spawn();
        Set(entity, component);
        return entity;
    }

    public Entity Spawn<T1, T2>(
        in T1? component1 = default,
        in T2? component2 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2);
        return entity;
    }

    public Entity Spawn<T1, T2, T3>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5, T6>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5, component6);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5, T6, T7>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5, component6, component7);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5, T6, T7, T8>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5, component6, component7, component8);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5, component6, component7, component8, component9);
        return entity;
    }

    public Entity Spawn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default,
        in T10? component10 = default)
    {
        var entity = Spawn();
        Set(entity, component1, component2, component3, component4, component5, component6, component7, component8, component9, component10);
        return entity;
    }

    public bool IsAlive(in Entity entity)
    {
        if (!entity.IsValid)
            return false;

        lock (_entityLock)
        {
            return entity.Id < _versions.Count && entity.Version == _versions[(int)entity.Id];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreateArchetype(in ComponentMask mask)
    {
        if (_archetypes.TryGetValue(mask, out var existing))
            return existing;

        return GetOrCreateArchetypeSlow(in mask);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private Archetype GetOrCreateArchetypeSlow(in ComponentMask mask)
    {
        lock (_archetypeLock)
        {
            if (_archetypes.TryGetValue(mask, out var existing))
                return existing;

            var archetype = new Archetype(mask);
            _archetypes[mask] = archetype;

            InvalidateCache();
            return archetype;
        }
    }

    private EntityLocation MoveEntityToArchetype(in Entity entity, in EntityLocation currentLocation, Archetype targetArchetype)
    {
        lock (_entityLock)
        {
            var (newChunkIndex, newIndexInChunk) = targetArchetype.Add(entity);

            var newLocation = new EntityLocation(targetArchetype, newChunkIndex, newIndexInChunk);

            var newChunk = newLocation.Archetype.Chunks[newChunkIndex];
            var oldChunk = currentLocation.Archetype.Chunks[currentLocation.ChunkIndex];

            foreach (var componentType in targetArchetype.ComponentIds)
            {
                if (currentLocation.Archetype.Mask.Has(componentType))
                {
                    var sourceArray = oldChunk.GetArray(componentType);
                    var targetArray = newChunk.GetArray(componentType);
                    sourceArray.MoveTo(currentLocation.IndexInChunk, targetArray, newIndexInChunk);
                }
            }

            var movedEntity = currentLocation.Archetype.Remove(currentLocation.ChunkIndex, currentLocation.IndexInChunk);

            if (IsAlive(movedEntity))
                _locations[movedEntity.Id] = new EntityLocation(currentLocation.Archetype, currentLocation.ChunkIndex, currentLocation.IndexInChunk);

            _locations[entity.Id] = newLocation;

            InvalidateCache();

            return newLocation;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateEntity(in Entity entity, [CallerMemberName] string operation = "")
    {
        if (!IsAlive(entity))
            throw new InvalidOperationException($"Cannot {operation} on dead entity {entity}.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateEntityLocation(in Entity entity, out EntityLocation location, [CallerMemberName] string operation = "")
    {
        if (!_locations.TryGetValue(entity.Id, out location))
            throw new InvalidOperationException($"Entity {entity} has no location for operation {operation}.");
    }
}
