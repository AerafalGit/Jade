// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Relations;

/// <summary>
/// Represents a pool for managing relations in the ECS (Entity Component System).
/// This class provides methods for adding, retrieving, and removing relations,
/// and ensures proper disposal of resources.
/// </summary>
/// <typeparam name="T">The type of data associated with the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
internal sealed class RelationPool<T> : IRelationPool
    where T : struct, IRelation
{
    private readonly Dictionary<Relation, T> _data;

    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelationPool{T}"/> class.
    /// </summary>
    public RelationPool()
    {
        _data = [];
    }

    /// <summary>
    /// Finalizes the <see cref="RelationPool{T}"/> instance and disposes of resources.
    /// </summary>
    ~RelationPool()
    {
        Dispose();
    }

    /// <summary>
    /// Adds a relation and its associated data to the pool.
    /// </summary>
    /// <param name="relation">The relation to add.</param>
    /// <param name="data">The data associated with the relation.</param>
    public void Add(Relation relation, in T data)
    {
        _data[relation] = data;
    }

    /// <summary>
    /// Retrieves a reference to the data associated with the specified relation.
    /// </summary>
    /// <param name="relation">The relation whose data is to be retrieved.</param>
    /// <returns>A reference to the data associated with the relation.</returns>
    public ref T Get(Relation relation)
    {
        return ref CollectionsMarshal.GetValueRefOrNullRef(_data, relation);
    }

    /// <summary>
    /// Removes a relation from the pool.
    /// </summary>
    /// <param name="relation">The relation to remove.</param>
    /// <returns><c>true</c> if the relation was successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(Relation relation)
    {
        return _data.Remove(relation);
    }

    /// <summary>
    /// Disposes of the resources used by the pool.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        _data.Clear();

        GC.SuppressFinalize(this);
    }
}
