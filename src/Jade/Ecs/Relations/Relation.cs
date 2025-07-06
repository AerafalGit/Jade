// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Entities;

namespace Jade.Ecs.Relations;

/// <summary>
/// Represents a relation between two entities in the ECS (Entity Component System).
/// A relation consists of a subject entity, a target entity, and a relation type identifier.
/// </summary>
/// <param name="Subject">The entity that is the subject of the relation.</param>
/// <param name="Target">The entity that is the target of the relation.</param>
/// <param name="RelationTypeId">An integer identifier representing the type of the relation.</param>
public readonly record struct Relation(Entity Subject, Entity Target, int RelationTypeId);
