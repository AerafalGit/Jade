// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T? GetResource<T>()
        where T : class
    {
        return World.GetResource<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool TryGetResource<T>([NotNullWhen(true)] out T? resource)
        where T : class
    {
        return World.TryGetResource(out resource);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T GetRequiredResource<T>()
        where T : class
    {
        return World.GetRequiredResource<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T AddResource<T>()
        where T : class, new()
    {
        return World.AddResource<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T SetResource<T>(T resource)
        where T : class
    {
        return World.SetResource(resource);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool RemoveResource<T>()
        where T : class
    {
        return World.RemoveResource<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasResource<T>()
        where T : class
    {
        return World.HasResource<T>();
    }
}
