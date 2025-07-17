// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct WorldStatistics(int ArchetypeCount, int ChunkCount, int EntityCount, int RecycledEntityCount);
