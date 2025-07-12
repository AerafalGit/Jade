// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Components;

namespace Jade.Ecs.Relations;

[StructLayout(LayoutKind.Sequential)]
internal readonly record struct Relation(ComponentId RelationId, Entity Target);
