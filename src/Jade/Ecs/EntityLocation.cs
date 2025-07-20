// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;
using Jade.Ecs.Archetypes;

namespace Jade.Ecs;

/// <summary>
/// Represents the location of an entity within the ECS (Entity Component System).
/// Stores the archetype, chunk index, and index within the chunk.
/// </summary>
/// <param name="Archetype">The archetype where the entity is located.</param>
/// <param name="ChunkIndex">The index of the chunk within the archetype.</param>
/// <param name="IndexInChunk">The index of the entity within the chunk.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct EntityLocation(Archetype Archetype, int ChunkIndex, int IndexInChunk);
