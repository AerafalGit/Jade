// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents metadata for a component in the ECS (Entity Component System).
/// </summary>
/// <param name="Id">The unique identifier for the component.</param>
/// <param name="IsBlittable">Indicates whether the component is blittable (can be directly copied in memory).</param>
/// <param name="ComponentArrayFactory">A factory function that creates a component array for the specified capacity.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct ComponentMetadata(int Id, bool IsBlittable, Func<int, ComponentArray> ComponentArrayFactory);
