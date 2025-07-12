// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity SetDependsOn(in Entity source, in Entity target)
    {
        return World.SetDependsOn(source, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool RemoveDependsOn(in Entity source, in Entity target)
    {
        return World.RemoveDependsOn(source, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasDependsOn(in Entity source, in Entity target)
    {
        return World.HasDependsOn(source, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ReadOnlySpan<Entity> GetDependsOn(in Entity source)
    {
        return World.GetDependsOn(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity SetParent(in Entity child, in Entity parent)
    {
        return World.SetParent(child, parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool RemoveParent(in Entity child)
    {
        return World.RemoveParent(child);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity GetParent(in Entity child)
    {
        return World.GetParent(child);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity AddChild(in Entity parent, in Entity child)
    {
        return World.AddChild(parent, child);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool RemoveChild(in Entity parent, in Entity child)
    {
        return World.RemoveChild(parent, child);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ReadOnlySpan<Entity> GetChildren(in Entity parent)
    {
        return World.GetChildren(parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasChildren(in Entity parent)
    {
        return World.HasChildren(parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity CreateChildOf(in Entity parent)
    {
        return World.CreateChildOf(parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void DestroyWithChildren(in Entity parent)
    {
        World.DestroyEntity(parent);
    }
}
