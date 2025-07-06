// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Entities;

namespace Jade.Ecs.Relations;

/// <summary>
/// Represents a graph structure for managing relations between entities in the ECS (Entity Component System).
/// Provides methods for adding, removing, and querying relations.
/// </summary>
public sealed class RelationGraph : IDisposable
{
    private readonly Dictionary<int, IRelationPool> _pools;
    private readonly Dictionary<Entity, List<Relation>> _subjectIndex;
    private readonly Dictionary<Entity, List<Relation>> _targetIndex;

    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelationGraph"/> class.
    /// </summary>
    public RelationGraph()
    {
        _pools = [];
        _subjectIndex = [];
        _targetIndex = [];
    }

    /// <summary>
    /// Adds a relation between two entities with associated data.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <param name="subject">The entity that is the subject of the relation.</param>
    /// <param name="target">The entity that is the target of the relation.</param>
    /// <param name="data">The data associated with the relation.</param>
    public void Add<T>(Entity subject, Entity target, T data)
        where T : struct, IRelation
    {
        var pool = GetOrCreatePool<T>();
        var relation = new Relation(subject, target, RelationId<T>.Id);

        if (!pool.Remove(relation))
        {
            if (!_subjectIndex.TryGetValue(subject, out var subjectRelations))
                _subjectIndex[subject] = subjectRelations = [];

            subjectRelations.Add(relation);

            if (!_targetIndex.TryGetValue(target, out var targetRelations))
                _targetIndex[target] = targetRelations = [];

            targetRelations.Add(relation);
        }

        pool.Add(relation, data);
    }

    /// <summary>
    /// Removes a relation between two entities.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <param name="subject">The entity that is the subject of the relation.</param>
    /// <param name="target">The entity that is the target of the relation.</param>
    /// <returns><c>true</c> if the relation was successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        var pool = GetOrCreatePool<T>();
        var relation = new Relation(subject, target, RelationId<T>.Id);

        if (!pool.Remove(relation))
            return false;

        if (_subjectIndex.TryGetValue(subject, out var subjectRelations))
            subjectRelations.Remove(relation);

        if (_targetIndex.TryGetValue(target, out var targetRelations))
            targetRelations.Remove(relation);

        return true;
    }

    /// <summary>
    /// Retrieves a reference to the data associated with a relation between two entities.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <param name="subject">The entity that is the subject of the relation.</param>
    /// <param name="target">The entity that is the target of the relation.</param>
    /// <returns>A reference to the data associated with the relation.</returns>
    public ref T Get<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        var pool = GetOrCreatePool<T>();
        var relation = new Relation(subject, target, RelationId<T>.Id);
        return ref pool.Get(relation);
    }

    /// <summary>
    /// Determines whether a relation exists between two entities.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <param name="subject">The entity that is the subject of the relation.</param>
    /// <param name="target">The entity that is the target of the relation.</param>
    /// <returns><c>true</c> if the relation exists; otherwise, <c>false</c>.</returns>
    public bool Has<T>(Entity subject, Entity target)
        where T : struct, IRelation
    {
        var pool = GetOrCreatePool<T>();
        var relation = new Relation(subject, target, RelationId<T>.Id);
        return !Unsafe.IsNullRef(ref pool.Get(relation));
    }

    /// <summary>
    /// Retrieves all target entities for a given subject entity and relation type.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <param name="subject">The entity that is the subject of the relations.</param>
    /// <returns>An enumerable of target entities.</returns>
    public IEnumerable<Entity> GetTargets<T>(Entity subject)
        where T : struct, IRelation
    {
        if (!_subjectIndex.TryGetValue(subject, out var relations))
            yield break;

        var id = RelationId<T>.Id;

        for (var i = 0; i < relations.Count; i++)
        {
            var relation = relations[i];

            if (relation.RelationTypeId == id)
                yield return relation.Target;
        }
    }

    /// <summary>
    /// Removes all relations involving a specified entity.
    /// </summary>
    /// <param name="entity">The entity whose relations are to be removed.</param>
    public void RemoveAllRelationsFor(Entity entity)
    {
        var relationsToRemove = new HashSet<Relation>();

        if (_subjectIndex.TryGetValue(entity, out var asSubject))
        {
            foreach(var rel in asSubject)
                relationsToRemove.Add(rel);
        }

        if (_targetIndex.TryGetValue(entity, out var asTarget))
        {
            foreach(var rel in asTarget)
                relationsToRemove.Add(rel);
        }

        if (relationsToRemove.Count is 0)
            return;

        foreach (var relation in relationsToRemove)
        {
            if (_pools.TryGetValue(relation.RelationTypeId, out var pool))
                pool.Remove(relation);

            if (_subjectIndex.TryGetValue(relation.Subject, out var subjectRels))
                subjectRels.Remove(relation);

            if (_targetIndex.TryGetValue(relation.Target, out var targetRels))
                targetRels.Remove(relation);
        }

        _subjectIndex.Remove(entity);
        _targetIndex.Remove(entity);
    }

    /// <summary>
    /// Retrieves or creates a relation pool for a specific relation type.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <returns>The relation pool for the specified relation type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RelationPool<T> GetOrCreatePool<T>()
        where T : struct, IRelation
    {
        var id = RelationId<T>.Id;

        if (!_pools.TryGetValue(id, out var pool))
            _pools[id] = pool = new RelationPool<T>();

        return (RelationPool<T>)pool;
    }

    /// <summary>
    /// Disposes of the resources used by the graph.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        foreach (var pool in _pools.Values)
            pool.Dispose();

        _pools.Clear();
        _subjectIndex.Clear();
        _targetIndex.Clear();
    }
}
