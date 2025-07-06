// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Relations;

/// <summary>
/// Provides a unique identifier for a specific relation type in the ECS (Entity Component System).
/// The identifier is generated and retrieved from the <see cref="RelationRegistry"/>.
/// </summary>
/// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
public static class RelationId<T>
    where T : struct, IRelation
{
    /// <summary>
    /// The unique identifier for the relation type <typeparamref name="T"/>.
    /// </summary>
    public static readonly int Id = RelationRegistry.GetId<T>();
}
