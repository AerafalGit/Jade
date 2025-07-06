// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Entities;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for spawning, creating, destroying, and validating entities.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Spawns a new entity using the specified entity bundle type.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle to use for spawning.</typeparam>
    /// <returns>An <see cref="EntityCommands"/> instance for managing the spawned entity.</returns>
    protected EntityCommands SpawnBundle<T>()
        where T : EntityBundle, new()
    {
        return World.SpawnBundle<T>();
    }

    /// <summary>
    /// Spawns a new entity using the provided entity bundle instance.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle.</typeparam>
    /// <param name="bundle">The entity bundle instance to use for spawning.</param>
    /// <returns>An <see cref="EntityCommands"/> instance for managing the spawned entity.</returns>
    protected EntityCommands SpawnBundle<T>(T bundle)
        where T : EntityBundle
    {
        return World.SpawnBundle(bundle);
    }

    /// <summary>
    /// Spawns a new entity without any associated bundle.
    /// </summary>
    /// <returns>An <see cref="EntityCommands"/> instance for managing the spawned entity.</returns>
    protected EntityCommands Spawn()
    {
        return World.Spawn();
    }

    /// <summary>
    /// Creates a new entity in the ECS world.
    /// </summary>
    /// <returns>The newly created <see cref="Entity"/> instance.</returns>
    protected Entity CreateEntity()
    {
        return World.CreateEntity();
    }

    /// <summary>
    /// Destroys the specified entity in the ECS world.
    /// </summary>
    /// <param name="entity">The entity to destroy.</param>
    protected void DestroyEntity(Entity entity)
    {
        World.DestroyEntity(entity);
    }

    /// <summary>
    /// Checks whether the specified entity is valid in the ECS world.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
    protected bool IsEntityValid(Entity entity)
    {
        return World.IsEntityValid(entity);
    }
}
