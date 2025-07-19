// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents metadata for a component in the ECS (Entity Component System).
/// </summary>
/// <param name="Id">The unique identifier for the component.</param>
/// <param name="Size">The size of the component in bytes.</param>
/// <param name="ComponentArrayFactory">A factory function that creates a component array for the specified capacity.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct ComponentMetadata(int Id, int Size, Func<int, ComponentArray> ComponentArrayFactory);
