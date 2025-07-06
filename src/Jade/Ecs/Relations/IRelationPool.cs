// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Relations;

/// <summary>
/// Represents a pool of relations in the ECS (Entity Component System).
/// Provides methods for managing relations and supports disposal of resources.
/// </summary>
public interface IRelationPool : IDisposable
{
    /// <summary>
    /// Removes a specified relation from the pool.
    /// </summary>
    /// <param name="relation">The relation to remove.</param>
    /// <returns><c>true</c> if the relation was successfully removed; otherwise, <c>false</c>.</returns>
    bool Remove(Relation relation);
}
