// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics;
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
    /// Checks if the specified entity is alive.
    /// An entity is considered alive if it is valid, its ID is within the range of existing versions,
    /// and its version matches the stored version.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <returns>True if the entity is alive; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive(in Entity entity)
    {
        lock (_entityLock)
            return entity.IsValid && entity.Id < _versions.Count && entity.Version == _versions[(int)entity.Id];
    }

    /// <summary>
    /// Despawns the specified entity, removing it from its current archetype and recycling its ID.
    /// Updates the location of any moved entities, and invalidates the query cache.
    /// </summary>
    /// <param name="entity">The entity to despawn.</param>
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

    /// <summary>
    /// Spawns a new entity with an empty component mask.
    /// </summary>
    /// <returns>The newly spawned entity.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Spawn()
    {
        return Spawn(new ComponentMask());
    }

    /// <summary>
    /// Spawns a new entity with the specified component mask.
    /// Allocates a new ID or reuses a recycled ID, assigns a version, and adds the entity to the appropriate archetype.
    /// </summary>
    /// <param name="mask">The component mask for the new entity.</param>
    /// <returns>The newly spawned entity.</returns>
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

        var (chunkIndex, indexInChunk) = archetype.Add(in entity);

        lock (_entityLock)
            _locations[id] = new EntityLocation(archetype, chunkIndex, indexInChunk);

        return entity;
    }

    /// <summary>
    /// Retrieves a reference to a component of the specified type from the given entity.
    /// Validates the entity and its location before accessing the component.
    /// </summary>
    /// <typeparam name="T1">The type of the component to retrieve.</typeparam>
    /// <param name="entity">The entity from which to retrieve the component.</param>
    /// <returns>A reference to the component.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the entity does not have the specified component.
    /// </exception>
    public ref T1 Get<T1>(in Entity entity)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var location);

        var componentId = Component<T1>.Id;

        if (!location.Archetype.Mask.Has(componentId))
            throw new InvalidOperationException($"Entity {entity} does not have component {typeof(T1).Name}.");

        return ref location.Archetype.Chunks[location.ChunkIndex].GetArray<T1>().GetRef(location.IndexInChunk);
    }

    /// <summary>
    /// Sets multiple components for the specified entity based on the given component mask.
    /// Moves the entity to a new archetype if necessary.
    /// </summary>
    /// <param name="entity">The entity for which to set components.</param>
    /// <param name="mask">The component mask specifying the components to set.</param>
    public void Set(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.HasAll(in mask))
            return;

        var newMask = currentMask.Union(in mask);
        var newArchetype = GetOrCreateArchetype(newMask);

        MoveEntityToArchetype(in entity, in currentLocation, newArchetype);
    }

    /// <summary>
    /// Sets a single component for the specified entity.
    /// Moves the entity to a new archetype if the component type is not already present.
    /// </summary>
    /// <typeparam name="T1">The type of the component to set.</typeparam>
    /// <param name="entity">The entity for which to set the component.</param>
    /// <param name="component">The component value to set.</param>
    public void Set<T1>(
        in Entity entity,
        in T1? component = default)
    {
        Debug.Assert(component is not null);

        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var componentType = Component<T1>.Id;

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.Has(componentType))
            currentLocation.Archetype.Chunks[currentLocation.ChunkIndex].GetArray<T1>().SetRef(currentLocation.IndexInChunk, component);
        else
        {
            var newMask = currentMask.With(componentType);
            var targetArchetype = GetOrCreateArchetype(newMask);
            var newLocation = MoveEntityToArchetype(entity, currentLocation, targetArchetype);

            newLocation.Archetype.Chunks[newLocation.ChunkIndex].GetArray<T1>().SetRef(newLocation.IndexInChunk, component);
        }
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
    /// Retrieves multiple components from an entity based on the specified component mask.
    /// Validates the entity and its location before accessing the components.
    /// </summary>
    /// <param name="entity">The entity from which to retrieve components.</param>
    /// <param name="mask">The component mask specifying the components to retrieve.</param>
    /// <returns>
    /// A tuple containing the archetype chunk and the index within the chunk
    /// where the components are located.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the entity does not have all the components specified in the mask.
    /// </exception>
    private (ArchetypeChunk archetypeChunk, int indexInChunk) GetMultiple(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var location);

        if (!location.Archetype.Mask.HasAll(in mask))
            throw new InvalidOperationException($"Entity {entity} does not have components {mask}.");

        return (location.Archetype.Chunks[location.ChunkIndex], location.IndexInChunk);
    }

    /// <summary>
    /// Sets multiple components for an entity based on the specified component mask.
    /// Validates the entity and its location before updating the components.
    /// Moves the entity to a new archetype if necessary.
    /// </summary>
    /// <param name="entity">The entity for which to set components.</param>
    /// <param name="mask">The component mask specifying the components to set.</param>
    /// <returns>
    /// A tuple containing the archetype chunk and the index within the chunk
    /// where the components are located.
    /// </returns>
    private (ArchetypeChunk archetypeChunk, int indexInChunk) SetMultiple(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.HasAll(in mask))
            return (currentLocation.Archetype.Chunks[currentLocation.ChunkIndex], currentLocation.IndexInChunk);

        var newMask = currentMask.Union(in mask);
        var targetArchetype = GetOrCreateArchetype(newMask);
        var newLocation = MoveEntityToArchetype(entity, currentLocation, targetArchetype);

        return (newLocation.Archetype.Chunks[newLocation.ChunkIndex], newLocation.IndexInChunk);
    }

    /// <summary>
    /// Retrieves or creates an archetype based on the specified component mask.
    /// </summary>
    /// <param name="mask">The component mask used to identify or create the archetype.</param>
    /// <returns>The existing or newly created archetype.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreateArchetype(in ComponentMask mask)
    {
        return _archetypes.TryGetValue(mask, out var existing)
            ? existing
            : GetOrCreateArchetypeSlow(in mask);
    }

    /// <summary>
    /// Creates a new archetype based on the specified component mask.
    /// This method is slower and involves locking for thread safety.
    /// </summary>
    /// <param name="mask">The component mask used to create the archetype.</param>
    /// <returns>The newly created archetype.</returns>
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

    /// <summary>
    /// Moves an entity from its current archetype to a target archetype.
    /// Updates the entity's location and transfers its components.
    /// </summary>
    /// <param name="entity">The entity to move.</param>
    /// <param name="currentLocation">The current location of the entity.</param>
    /// <param name="targetArchetype">The target archetype to move the entity to.</param>
    /// <returns>The new location of the entity in the target archetype.</returns>
    private EntityLocation MoveEntityToArchetype(in Entity entity, in EntityLocation currentLocation, Archetype targetArchetype)
    {
        lock (_entityLock)
        {
            var (newChunkIndex, newIndexInChunk) = targetArchetype.Add(in entity);

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

    /// <summary>
    /// Validates that the specified entity is alive.
    /// Throws an exception if the entity is dead.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <param name="operation">The name of the operation being performed, used for error reporting.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateEntity(in Entity entity, [CallerMemberName] string operation = "")
    {
        if (!IsAlive(entity))
            throw new InvalidOperationException($"Cannot {operation} on dead entity {entity}.");
    }

    /// <summary>
    /// Validates the location of the specified entity.
    /// Throws an exception if the entity has no location.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <param name="location">The location of the entity, output parameter.</param>
    /// <param name="operation">The name of the operation being performed, used for error reporting.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateEntityLocation(in Entity entity, out EntityLocation location, [CallerMemberName] string operation = "")
    {
        if (!_locations.TryGetValue(entity.Id, out location))
            throw new InvalidOperationException($"Entity {entity} has no location for operation {operation}.");
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
