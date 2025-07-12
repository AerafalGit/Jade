// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Prefabs;

public struct PrefabOverrideComponent<T> : IComponent
    where T : unmanaged, IComponent
{
    public T Component;

    public PrefabOverrideComponent(T component)
    {
        Component = component;
    }
}
