// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Archetypes;

namespace Jade.Ecs.Entities;

public readonly record struct EntityLocation(Archetype Archetype, int ChunkIndex, int IndexInChunk);
