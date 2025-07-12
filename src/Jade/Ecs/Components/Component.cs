// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Components;

internal static class Component<T>
    where T : unmanaged, IComponent
{
    public static readonly ComponentMetadata Metadata = ComponentRegistry.GetMetadata<T>();
}
