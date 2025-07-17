// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;
using Jade.Ecs.Archetypes;

namespace Jade.Ecs;

[StructLayout(LayoutKind.Sequential)]
internal readonly record struct EntityLocation(Archetype Archetype, int ChunkIndex, int IndexInChunk);
