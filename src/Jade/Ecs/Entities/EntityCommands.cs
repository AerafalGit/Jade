// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Relations;

namespace Jade.Ecs.Entities;

/// <summary>
/// Provides a fluent API for performing operations on an entity within the ECS (Entity Component System) world.
/// Allows adding and removing components, managing relations, applying bundles, and despawning entities.
/// </summary>
public sealed class EntityCommands
{
    private readonly World _world;
    private readonly Entity _entity;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCommands"/> class.
    /// </summary>
    /// <param name="world">The ECS world instance.</param>
    /// <param name="entity">The entity to manipulate.</param>
    public EntityCommands(World world, Entity entity)
    {
        _world = world;
        _entity = entity;
    }

    /// <summary>
    /// Adds a component of type <typeparamref name="T"/> to the entity.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="component">The component data to add.</param>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands With<T>(in T component)
        where T : struct, IComponent
    {
        _world.AddComponent(_entity, component);
        return this;
    }

    /// <summary>
    /// Removes a component of type <typeparamref name="T"/> from the entity.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands Without<T>()
        where T : struct, IComponent
    {
        _world.RemoveComponent<T>(_entity);
        return this;
    }

    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the entity and the target entity.
    /// Initializes the relation with default data.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands WithRelation<T>(Entity target)
        where T : struct, IRelation
    {
        _world.AddRelation(_entity, target, new T());
        return this;
    }

    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the entity and the target entity.
    /// Initializes the relation with the provided data.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="target">The target entity of the relation.</param>
    /// <param name="data">The data to initialize the relation with.</param>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands WithRelation<T>(Entity target, in T data)
        where T : struct, IRelation
    {
        _world.AddRelation(_entity, target, data);
        return this;
    }

    /// <summary>
    /// Removes a relation of type <typeparamref name="T"/> between the entity and the target entity.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands WithoutRelation<T>(Entity target)
        where T : struct, IRelation
    {
        _world.RemoveRelation<T>(_entity, target);
        return this;
    }

    /// <summary>
    /// Applies an entity bundle of type <typeparamref name="T"/> to the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle, which must inherit from <see cref="EntityBundle"/>.</typeparam>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands WithBundle<T>()
        where T : EntityBundle, new()
    {
        var bundle = new T();
        bundle.Configure(this);
        return this;
    }

    /// <summary>
    /// Applies the specified entity bundle to the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity bundle, which must inherit from <see cref="EntityBundle"/>.</typeparam>
    /// <param name="bundle">The entity bundle to apply.</param>
    /// <returns>The current <see cref="EntityCommands"/> instance for chaining.</returns>
    public EntityCommands WithBundle<T>(T bundle)
        where T : EntityBundle
    {
        bundle.Configure(this);
        return this;
    }

    /// <summary>
    /// Despawns the entity, removing it from the ECS world.
    /// </summary>
    public void Despawn()
    {
        _world.DestroyEntity(_entity);
    }

    /// <summary>
    /// Implicitly converts an <see cref="EntityCommands"/> instance to an <see cref="Entity"/>.
    /// </summary>
    /// <param name="commands">The <see cref="EntityCommands"/> instance to convert.</param>
    /// <returns>The entity associated with the commands.</returns>
    public static implicit operator Entity(EntityCommands commands)
    {
        return commands._entity;
    }
}
