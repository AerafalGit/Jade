// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Entities;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for managing components associated with entities.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Adds a new component of type <typeparamref name="T"/> to the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to which the component will be added.</param>
    /// <returns>A reference to the newly added component.</returns>
    protected ref T AddComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        return ref World.AddComponent<T>(entity);
    }

    /// <summary>
    /// Adds a new component of type <typeparamref name="T"/> to the specified entity and initializes it with the provided value.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to which the component will be added.</param>
    /// <param name="component">The initial value of the component.</param>
    protected void AddComponent<T>(Entity entity, T component)
        where T : struct, IComponent
    {
        World.AddComponent(entity, component);
    }

    /// <summary>
    /// Removes the component of type <typeparamref name="T"/> from the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the component to remove.</typeparam>
    /// <param name="entity">The entity from which the component will be removed.</param>
    /// <returns><c>true</c> if the component was successfully removed; otherwise, <c>false</c>.</returns>
    protected bool RemoveComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        return World.RemoveComponent<T>(entity);
    }

    /// <summary>
    /// Retrieves a reference to the component of type <typeparamref name="T"/> associated with the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <param name="entity">The entity whose component will be retrieved.</param>
    /// <returns>A reference to the component.</returns>
    protected ref T GetComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        return ref World.GetComponent<T>(entity);
    }

    /// <summary>
    /// Checks whether the specified entity has a component of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the component to check for.</typeparam>
    /// <param name="entity">The entity to check.</param>
    /// <returns><c>true</c> if the entity has the component; otherwise, <c>false</c>.</returns>
    protected bool HasComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        return World.HasComponent<T>(entity);
    }
}
