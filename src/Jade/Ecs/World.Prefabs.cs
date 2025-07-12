// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Assets;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs;

public sealed partial class World
{
    public Entity CreatePrefab(in string name, in Entity prefabEntity)
    {
        return PrefabRegistry.RegisterPrefab(name, prefabEntity);
    }

    public bool DestroyPrefab(in string name)
    {
        return PrefabRegistry.UnregisterPrefab(name);
    }

    public bool DestroyPrefab(in Handle<string> nameId)
    {
        return PrefabRegistry.UnregisterPrefab(nameId);
    }

    public Entity GetPrefab(in string name)
    {
        return PrefabRegistry.GetPrefab(name);
    }

    public Entity GetPrefab(in Handle<string> nameId)
    {
        return PrefabRegistry.GetPrefab(nameId);
    }

    public Entity InstantiatePrefab(in string name)
    {
        return PrefabRegistry.Instantiate(name);
    }

    public Entity InstantiatePrefab(in Handle<string> nameId)
    {
        return PrefabRegistry.Instantiate(nameId);
    }

    public Entity InstantiatePrefabOverride<T>(in string name, in T componentOverride)
        where T : unmanaged, IComponent
    {
        return PrefabRegistry.Instantiate(name, componentOverride);
    }

    public Entity InstantiatePrefabOverride<T>(in Handle<string> nameId, in T componentOverride)
        where T : unmanaged, IComponent
    {
        return PrefabRegistry.Instantiate(nameId, componentOverride);
    }

    public Entity InstantiatePrefabEntity(in Entity prefabEntity)
    {
        return PrefabRegistry.InstantiatePrefabEntity(prefabEntity);
    }

    public IEnumerable<string> GetPrefabNames()
    {
        return PrefabRegistry.GetPrefabNames();
    }

    public bool HasPrefab(in string name)
    {
        return PrefabRegistry.HasPrefab(name);
    }

    public bool HasPrefab(in Handle<string> nameId)
    {
        return PrefabRegistry.HasPrefab(nameId);
    }
}
