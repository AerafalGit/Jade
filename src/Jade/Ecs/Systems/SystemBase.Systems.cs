// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return World.IsSystemEnabled<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        World.SetSystemEnabled<T>(enabled);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T? GetSystem<T>()
        where T : SystemBase
    {
        return World.GetSystem<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected T GetRequiredSystem<T>()
        where T : SystemBase
    {
        return World.GetRequiredSystem<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasSystem<T>()
        where T : SystemBase
    {
        return World.HasSystem<T>();
    }
}
