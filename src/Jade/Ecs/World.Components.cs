// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Entities;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world, providing methods for managing components
/// associated with entities, including adding, removing, and retrieving components.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Adds a new component of type <typeparamref name="T"/> to the specified entity.
    /// If the entity already has the component, the existing component is returned.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="entity">The entity to which the component will be added.</param>
    /// <returns>A reference to the newly added or existing component.</returns>
    /// <exception cref="ArgumentException">Thrown if the entity is invalid or does not exist in the world.</exception>
    public ref T AddComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        var component = new T();

        if (!IsEntityValid(entity, out var oldLocation))
            throw new ArgumentException($"Entity {entity.Id} is not valid or does not exist in the world.", nameof(entity));

        var oldArchetype = oldLocation.Archetype;

        if (oldArchetype.Mask.Has(ComponentId<T>.Id))
            return ref GetComponent<T>(entity);

        var newMask = oldArchetype.Mask.With(ComponentId<T>.Id);

        if (!_archetypes.TryGetValue(newMask, out var newArchetype))
            newArchetype = CreateArchetypeFor(newMask);

        var newLocation = MoveEntity(entity, oldLocation, newArchetype);
        _entityIndex[entity.Id] = newLocation;

        ref var newComponent = ref GetComponent<T>(entity);
        newComponent = component;
        return ref newComponent;
    }

    /// <summary>
    /// Adds a new component of type <typeparamref name="T"/> to the specified entity and initializes it
    /// with the provided value. If the entity already has the component, the existing component is updated.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="entity">The entity to which the component will be added.</param>
    /// <param name="component">The value to initialize the component with.</param>
    /// <exception cref="ArgumentException">Thrown if the entity is invalid or does not exist in the world.</exception>
    public void AddComponent<T>(Entity entity, in T component)
        where T : struct, IComponent
    {
        if (!IsEntityValid(entity, out var oldLocation))
            throw new ArgumentException($"Entity {entity.Id} is not valid or does not exist in the world.", nameof(entity));

        var oldArchetype = oldLocation.Archetype;

        if (oldArchetype.Mask.Has(ComponentId<T>.Id))
        {
            ref var existingComponent = ref GetComponent<T>(entity);
            existingComponent = component;
            return;
        }

        var newMask = oldArchetype.Mask.With(ComponentId<T>.Id);

        if (!_archetypes.TryGetValue(newMask, out var newArchetype))
            newArchetype = CreateArchetypeFor(newMask);

        var newLocation = MoveEntity(entity, oldLocation, newArchetype);
        _entityIndex[entity.Id] = newLocation;

        ref var newComponent = ref GetComponent<T>(entity);
        newComponent = component;
    }

    /// <summary>
    /// Removes a component of type <typeparamref name="T"/> from the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="entity">The entity from which the component will be removed.</param>
    /// <returns><c>true</c> if the component was successfully removed; otherwise, <c>false</c>.</returns>
    public bool RemoveComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        if (!IsEntityValid(entity, out var oldLocation))
            return false;

        var oldArchetype = oldLocation.Archetype;

        if (!oldArchetype.Mask.Has(ComponentId<T>.Id))
            return false;

        var newMask = oldArchetype.Mask.Without(ComponentId<T>.Id);

        if (!_archetypes.TryGetValue(newMask, out var newArchetype))
            newArchetype = CreateArchetypeFor(newMask);

        var newLocation = MoveEntity(entity, oldLocation, newArchetype);
        _entityIndex[entity.Id] = newLocation;
        return true;
    }

    /// <summary>
    /// Retrieves a reference to the component of type <typeparamref name="T"/> associated with the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="entity">The entity whose component will be retrieved.</param>
    /// <returns>A reference to the component.</returns>
    /// <exception cref="ArgumentException">Thrown if the entity is invalid or does not exist in the world,
    /// or if the entity does not have the specified component.</exception>
    public ref T GetComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        if (!IsEntityValid(entity, out var location))
            throw new ArgumentException($"Entity {entity.Id} is not valid or does not exist in the world.", nameof(entity));

        var componentIndex = location.Archetype.GetComponentIndex<T>();

        if(componentIndex is -1)
            throw new ArgumentException($"Entity {entity.Id} does not have component of type {typeof(T).Name}.", nameof(entity));

        var chunk = location.Archetype.GetChunk(location.ChunkIndex);

        return ref chunk.GetComponents<T>(componentIndex)[location.IndexInChunk];
    }

    /// <summary>
    /// Checks whether the specified entity has a component of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <param name="entity">The entity to check.</param>
    /// <returns><c>true</c> if the entity has the component; otherwise, <c>false</c>.</returns>
    public bool HasComponent<T>(Entity entity)
        where T : struct, IComponent
    {
        return IsEntityValid(entity, out var location) && location.Archetype.Mask.Has(ComponentId<T>.Id);
    }
}
