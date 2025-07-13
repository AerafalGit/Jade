// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Systems;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddSystems(SystemStage stage, params IEnumerable<SystemBase> systems)
    {
        SystemRunner.AddSystems(stage, systems);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddSystem<T>(SystemStage stage, T system)
        where T : SystemBase
    {
        SystemRunner.AddSystem(stage, system);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddSystem<T>(SystemStage stage)
        where T : SystemBase, new()
    {
        SystemRunner.AddSystem<T>(stage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return SystemRunner.IsSystemEnabled<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        SystemRunner.SetSystemEnabled<T>(enabled);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? GetSystem<T>()
        where T : SystemBase
    {
        return SystemRunner.GetSystem<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetRequiredSystem<T>()
        where T : SystemBase
    {
        return SystemRunner.GetRequiredSystem<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSystem<T>()
        where T : SystemBase
    {
        return SystemRunner.HasSystem<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RunStage(SystemStage stage)
    {
        SystemRunner.RunStage(stage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SystemStatistics GetSystemStats()
    {
        return SystemRunner.GetSystemStats();
    }
}
