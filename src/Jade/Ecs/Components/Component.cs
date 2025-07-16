// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Components;

public static class Component<T>
{
    public static readonly ComponentMetadata Metadata = ComponentRegistry.GetMetadata<T>();
}
