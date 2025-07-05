// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Components;

/// <summary>
/// Provides static information about a specific component type in the ECS (Entity Component System).
/// This includes the component's unique identifier, size, and whether it is blittable.
/// </summary>
/// <typeparam name="T">The type of the component, which must implement <see cref="IComponent"/> and be a value type.</typeparam>
public static class ComponentId<T>
    where T : struct, IComponent
{
    /// <summary>
    /// The unique identifier of the component type.
    /// </summary>
    public static readonly int Id;

    /// <summary>
    /// The size of the component type in bytes.
    /// </summary>
    public static readonly int Size;

    /// <summary>
    /// Indicates whether the component type is blittable (i.e., can be directly copied in memory).
    /// </summary>
    public static readonly bool IsBlittable;

    /// <summary>
    /// Static constructor that initializes the component metadata by querying the <see cref="ComponentRegistry"/>.
    /// </summary>
    static ComponentId()
    {
        var metadata = ComponentRegistry.GetMetadata(ComponentRegistry.Register<T>());

        Id = metadata.Id;
        Size = metadata.Size;
        IsBlittable = metadata.IsBlittable;
    }
}
