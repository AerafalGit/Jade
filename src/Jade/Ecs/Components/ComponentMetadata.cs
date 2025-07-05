// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Components;

/// <summary>
/// Represents metadata for a component in the ECS (Entity Component System).
/// This metadata includes the component's unique identifier, type, size, and whether it is blittable.
/// </summary>
/// <param name="Id">The unique identifier of the component.</param>
/// <param name="Type">The type of the component.</param>
/// <param name="Size">The size of the component in bytes.</param>
/// <param name="IsBlittable">Indicates whether the component is blittable (i.e., can be directly copied in memory).</param>
public readonly record struct ComponentMetadata(int Id, Type Type, int Size, bool IsBlittable);
