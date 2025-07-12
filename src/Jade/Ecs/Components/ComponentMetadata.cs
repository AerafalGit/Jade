// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

[StructLayout(LayoutKind.Sequential)]
internal readonly record struct ComponentMetadata(ComponentId Id, int Size, int Alignment, Func<int, ComponentArray> ComponentArrayFactory);
