// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Entities;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world, providing methods for managing entities,
/// spawning entity bundles, and handling entity lifecycle operations.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Spawns a new entity bundle of the specified type and configures it.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle, which must inherit from <see cref="EntityBundle"/> and have a parameterless constructor.</typeparam>
    /// <returns>An <see cref="EntityCommands"/> instance for configuring the spawned entities.</returns>
    public EntityCommands SpawnBundle<T>()
        where T : EntityBundle, new()
    {
        var bundle = new T();
        var commands = Spawn();
        bundle.Configure(commands);
        return commands;
    }

    /// <summary>
    /// Spawns a new entity bundle using the provided instance and configures it.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle, which must inherit from <see cref="EntityBundle"/>.</typeparam>
    /// <param name="bundle">The instance of the entity bundle to configure.</param>
    /// <returns>An <see cref="EntityCommands"/> instance for configuring the spawned entities.</returns>
    public EntityCommands SpawnBundle<T>(T bundle)
        where T : EntityBundle
    {
        var commands = Spawn();
        bundle.Configure(commands);
        return commands;
    }

    /// <summary>
    /// Spawns a new entity and returns an <see cref="EntityCommands"/> instance for configuring it.
    /// </summary>
    /// <returns>An <see cref="EntityCommands"/> instance for configuring the spawned entity.</returns>
    public EntityCommands Spawn()
    {
        return new EntityCommands(this, CreateEntity());
    }

    /// <summary>
    /// Creates a new entity and assigns it a unique ID and version.
    /// </summary>
    /// <returns>The newly created <see cref="Entity"/> instance.</returns>
    public Entity CreateEntity()
    {
        uint id;

        if (_freeIds.Count > 0)
            id = _freeIds.Dequeue();
        else
        {
            id = _nextEntityId++;

            if (id >= _versions.Count)
                _versions.Add(0);
        }

        var entity = new Entity(id, _versions[(int)id]);
        var emptyArchetype = _archetypes[new ComponentMask()];

        var location = emptyArchetype.AddEntity(entity);
        _entityIndex[id] = location;

        return entity;
    }

    /// <summary>
    /// Destroys the specified entity, removing it from the ECS world and updating its version.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to destroy.</param>
    public void DestroyEntity(Entity entity)
    {
        if (!IsEntityValid(entity, out var location))
            return;

        _relationGraph.RemoveAllRelationsFor(entity);

        var movedEntityInfo = location.Archetype.RemoveEntity(location);

        if (movedEntityInfo.HasValue)
        {
            var (movedEntity, newLocation) = movedEntityInfo.Value;
            _entityIndex[movedEntity.Id] = newLocation;
        }

        _entityIndex.Remove(entity.Id);
        _versions[(int)entity.Id]++;
        _freeIds.Enqueue(entity.Id);
    }

    /// <summary>
    /// Checks whether the specified entity is valid in the ECS world.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to validate.</param>
    /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
    public bool IsEntityValid(Entity entity)
    {
        return !entity.IsNull &&
               _entityIndex.ContainsKey(entity.Id) &&
               _versions[(int)entity.Id] == entity.Version;
    }

    /// <summary>
    /// Checks whether the specified entity is valid in the ECS world and retrieves its location.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to validate.</param>
    /// <param name="location">The location of the entity if valid.</param>
    /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
    private bool IsEntityValid(Entity entity, out EntityLocation location)
    {
        location = default;

        return !entity.IsNull &&
               _entityIndex.TryGetValue(entity.Id, out location) &&
               _versions[(int)entity.Id] == entity.Version;
    }
}
