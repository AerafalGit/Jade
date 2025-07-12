// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected EntityCommands Spawn()
    {
        return World.Spawn();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity CreateEntity()
    {
        return World.CreateEntity();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void DestroyEntity(Entity entity)
    {
        World.DestroyEntity(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool IsAlive(Entity entity)
    {
        return World.IsAlive(entity);
    }
}
