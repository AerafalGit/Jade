// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Entities;
using Jade.Ecs.Relations;

namespace Jade.Ecs;

/// <summary>
/// Provides methods for managing relations between entities in the ECS (Entity Component System) world.
/// Allows adding, removing, retrieving, and checking relations, as well as querying relation targets.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the subject and target entities.
    /// Initializes the relation with default data.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <exception cref="ArgumentException">Thrown if either the subject or target entity is invalid or does not exist in the world.</exception>
    public void AddRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        AddRelation(subject, target, new T());
    }

    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the subject and target entities.
    /// Initializes the relation with the provided data.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <param name="data">The data to initialize the relation with.</param>
    /// <exception cref="ArgumentException">Thrown if either the subject or target entity is invalid or does not exist in the world.</exception>
    public void AddRelation<T>(Entity subject, Entity target, T data)
        where T : struct, IRelation
    {
        if (!IsEntityValid(subject))
            throw new ArgumentException($"Entity {subject.Id} is not valid or does not exist in the world.", nameof(subject));

        if (!IsEntityValid(target))
            throw new ArgumentException($"Entity {target.Id} is not valid or does not exist in the world.", nameof(target));

        _relationGraph.Add(subject, target, data);
    }

    /// <summary>
    /// Removes a relation of type <typeparamref name="T"/> between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns><c>true</c> if the relation was successfully removed; otherwise, <c>false</c>.</returns>
    public bool RemoveRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        return _relationGraph.Remove<T>(subject, target);
    }

    /// <summary>
    /// Retrieves a reference to the relation of type <typeparamref name="T"/> between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns>A reference to the relation data.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no relation of the specified type exists between the subject and target entities.</exception>
    public ref T GetRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        ref var data = ref _relationGraph.Get<T>(subject, target);

        if (Unsafe.IsNullRef(ref data))
            throw new InvalidOperationException($"No relation of type {typeof(T).Name} exists between subject {subject.Id} and target {target.Id}.");

        return ref data;
    }

    /// <summary>
    /// Checks whether a relation of type <typeparamref name="T"/> exists between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns><c>true</c> if the relation exists; otherwise, <c>false</c>.</returns>
    public bool HasRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        return _relationGraph.Has<T>(subject, target);
    }

    /// <summary>
    /// Retrieves the target entities of relations of type <typeparamref name="T"/> for the specified subject entity.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/>.</typeparam>
    /// <param name="subject">The subject entity of the relations.</param>
    /// <returns>An enumerable of target entities for the specified relations.</returns>
    public IEnumerable<Entity> GetRelationTargets<T>(Entity subject)
        where T : struct, IRelation
    {
        return !IsEntityValid(subject)
            ? []
            : _relationGraph.GetTargets<T>(subject);
    }
}
