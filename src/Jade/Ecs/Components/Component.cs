// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Provides a static container for accessing metadata of a specific component type in the ECS (Entity Component System).
/// </summary>
/// <typeparam name="T">The type of the component.</typeparam>
public static class Component<T>
{
    private static readonly Lazy<ComponentMetadata> s_metadata = new(ComponentRegistry.GetMetadata<T>, LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the unique ID of the component type <typeparamref name="T"/>.
    /// </summary>
    public static int Id
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => s_metadata.Value.Id;
    }

    /// <summary>
    /// Gets a value indicating whether the component type <typeparamref name="T"/> is blittable.
    /// Blittable components can be directly copied in memory.
    /// </summary>
    public static bool IsBlittable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => s_metadata.Value.IsBlittable;
    }
}
