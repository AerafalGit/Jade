// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Assets;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity CreatePrefab(in string name, in Entity prefabEntity)
    {
        return World.CreatePrefab(name, prefabEntity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool DestroyPrefab(in string name)
    {
        return World.DestroyPrefab(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool DestroyPrefab(in Handle<string> nameId)
    {
        return World.DestroyPrefab(nameId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity GetPrefab(in string name)
    {
        return World.GetPrefab(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity GetPrefab(in Handle<string> nameId)
    {
        return World.GetPrefab(nameId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity InstantiatePrefab(in string name)
    {
        return World.InstantiatePrefab(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity InstantiatePrefab(in Handle<string> nameId)
    {
        return World.InstantiatePrefab(nameId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity InstantiatePrefabOverride<T>(in string name, in T componentOverride)
        where T : unmanaged, IComponent
    {
        return World.InstantiatePrefabOverride(name, componentOverride);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity InstantiatePrefabOverride<T>(in Handle<string> nameId, in T componentOverride)
        where T : unmanaged, IComponent
    {
        return World.InstantiatePrefabOverride(nameId, componentOverride);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Entity InstantiatePrefabEntity(in Entity prefabEntity)
    {
        return World.InstantiatePrefabEntity(prefabEntity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IEnumerable<string> GetPrefabNames()
    {
        return World.GetPrefabNames();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasPrefab(in string name)
    {
        return World.HasPrefab(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool HasPrefab(in Handle<string> nameId)
    {
        return World.HasPrefab(nameId);
    }
}
