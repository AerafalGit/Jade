// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void AddComponent<T>(Entity entity, T component = default)
        where T : unmanaged, IComponent
    {
        World.AddComponent(entity, component);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void RemoveComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        World.RemoveComponent<T>(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ref T GetComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        return ref World.GetComponent<T>(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        return World.HasComponent<T>(entity);
    }
}
