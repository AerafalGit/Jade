// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Assets;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Components;
using Jade.Ecs.Relations;

namespace Jade.Ecs.Prefabs;

public sealed class PrefabRegistry
{
    private readonly Dictionary<Handle<string>, Entity> _prefabsByName;
    private readonly Assets<string> _assets;
    private readonly World _world;

    public PrefabRegistry(World world)
    {
        _prefabsByName = [];
        _world = world;
        _assets = world.SetResource(new Assets<string>());
    }

    public Entity RegisterPrefab(string name, Entity prefabEntity)
    {
        var nameId = _assets.GetOrAdd(name);

        _world.AddComponent(prefabEntity, new PrefabComponent(nameId, true));

        _prefabsByName[nameId] = prefabEntity;
        return prefabEntity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetPrefab(string name)
    {
        return _prefabsByName.GetValueOrDefault(_assets.GetOrAdd(name), Entity.Null);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetPrefab(Handle<string> nameId)
    {
        return _prefabsByName.GetValueOrDefault(nameId, Entity.Null);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPrefab(string name)
    {
        return _prefabsByName.ContainsKey(_assets.GetOrAdd(name));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPrefab(Handle<string> nameId)
    {
        return _prefabsByName.ContainsKey(nameId);
    }

    public Entity Instantiate(string prefabName)
    {
        var prefab = GetPrefab(prefabName);

        if (!_world.IsAlive(prefab))
            throw new ArgumentException($"Prefab '{prefabName}' not found or is not alive.");

        return InstantiatePrefabEntity(prefab);
    }

    public Entity Instantiate(Handle<string> prefabNameId)
    {
        var prefab = GetPrefab(prefabNameId);

        if (!_world.IsAlive(prefab))
            throw new ArgumentException($"Prefab with ID '{prefabNameId}' not found or is not alive.");

        return InstantiatePrefabEntity(prefab);
    }

    public Entity Instantiate<T>(string prefabName, in T componentOverride)
        where T : unmanaged, IComponent
    {
        var instance = Instantiate(prefabName);

        _world.AddComponent(instance, componentOverride);
        _world.AddComponent(instance, new PrefabOverrideComponent<T>(componentOverride));

        return instance;
    }

    public Entity Instantiate<T>(Handle<string> prefabNameId, in T componentOverride)
        where T : unmanaged, IComponent
    {
        var instance = Instantiate(prefabNameId);

        _world.AddComponent(instance, componentOverride);
        _world.AddComponent(instance, new PrefabOverrideComponent<T>(componentOverride));

        return instance;
    }

    public Entity InstantiatePrefabEntity(Entity prefabTemplate)
    {
        if (!_world.HasComponent<PrefabComponent>(prefabTemplate))
            throw new ArgumentException("Entity is not a prefab template.");

        var prefabComponent = _world.GetComponent<PrefabComponent>(prefabTemplate);
        if (!prefabComponent.IsTemplate)
            throw new ArgumentException("Entity is not a prefab template.");

        var instance = _world.CreateEntity();

        _world.AddComponent(instance, new PrefabComponent(prefabComponent.NameId, false));
        _world.AddRelation(instance, RelationProperty.InstanceOf, prefabTemplate);

        CopyComponentsFromTemplate(prefabTemplate, instance);

        CopyHierarchyFromTemplate(prefabTemplate, instance);

        return instance;
    }

    public IEnumerable<string> GetPrefabNames()
    {
        return _prefabsByName.Keys.Select(nameId => _assets.Get(nameId)!);
    }

    public bool UnregisterPrefab(string name)
    {
        return _prefabsByName.Remove(_assets.GetOrAdd(name));
    }

    public bool UnregisterPrefab(Handle<string> nameId)
    {
        return _prefabsByName.Remove(nameId);
    }

    private void CopyComponentsFromTemplate(Entity template, Entity instance)
    {
        if (!_world.TryGetEntityLocation(template, out var templateLocation))
            return;

        var templateChunk = templateLocation.Archetype.Chunks[templateLocation.ChunkIndex];

        foreach (var componentId in templateLocation.Archetype.ComponentIds)
        {
            if (componentId.IsRelation || componentId == Component<PrefabComponent>.Metadata.Id)
                continue;

            var componentArray = templateChunk.GetArray(componentId);
            var componentData = componentArray.GetAsBytes(templateLocation.IndexInChunk);

            _world.AddComponentFromBytes(instance, componentId, componentData);
        }
    }

    private void CopyHierarchyFromTemplate(Entity template, Entity instance)
    {
        var children = _world.GetChildren(template);

        foreach (var childTemplate in children)
            _world.SetParent(InstantiatePrefabEntity(childTemplate), instance);
    }
}
