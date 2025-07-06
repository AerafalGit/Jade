// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Entities;
using Jade.Ecs.Relations;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for managing relations between entities.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation to add.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    protected void AddRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        World.AddRelation<T>(subject, target);
    }

    /// <summary>
    /// Adds a relation of type <typeparamref name="T"/> between the subject and target entities,
    /// and initializes it with the provided data.
    /// </summary>
    /// <typeparam name="T">The type of the relation to add.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <param name="data">The initial data for the relation.</param>
    protected void AddRelation<T>(Entity subject, Entity target, T data)
        where T : struct, IRelation
    {
        World.AddRelation(subject, target, data);
    }

    /// <summary>
    /// Removes the relation of type <typeparamref name="T"/> between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation to remove.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns><c>true</c> if the relation was successfully removed; otherwise, <c>false</c>.</returns>
    protected bool RemoveRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        return World.RemoveRelation<T>(subject, target);
    }

    /// <summary>
    /// Retrieves a reference to the relation of type <typeparamref name="T"/> between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation to retrieve.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns>A reference to the relation.</returns>
    protected ref T GetRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        return ref World.GetRelation<T>(subject, target);
    }

    /// <summary>
    /// Checks whether a relation of type <typeparamref name="T"/> exists between the subject and target entities.
    /// </summary>
    /// <typeparam name="T">The type of the relation to check.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <param name="target">The target entity of the relation.</param>
    /// <returns><c>true</c> if the relation exists; otherwise, <c>false</c>.</returns>
    protected bool HasRelation<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        return World.HasRelation<T>(subject, target);
    }

    /// <summary>
    /// Retrieves all target entities that have a relation of type <typeparamref name="T"/> with the specified subject entity.
    /// </summary>
    /// <typeparam name="T">The type of the relation to query.</typeparam>
    /// <param name="subject">The subject entity of the relation.</param>
    /// <returns>An enumerable collection of target entities.</returns>
    protected IEnumerable<Entity> GetRelationTargets<T>(Entity subject)
        where T : struct, IRelation
    {
        return World.GetRelationTargets<T>(subject);
    }
}
