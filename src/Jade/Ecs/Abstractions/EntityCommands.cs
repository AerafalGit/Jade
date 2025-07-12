// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Abstractions;

public sealed class EntityCommands
{
    private readonly World _world;
    private readonly Entity _entity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands(World world, Entity entity)
    {
        _world = world;
        _entity = entity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands With<T>(in T component = default)
        where T : unmanaged, IComponent
    {
        _world.AddComponent(_entity, component);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands WithIf<T>(bool condition, in T component = default)
        where T : unmanaged, IComponent
    {
        if (condition)
            _world.AddComponent(_entity, component);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands Without<T>()
        where T : unmanaged, IComponent
    {
        _world.RemoveComponent<T>(_entity);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityCommands WithoutIf<T>(bool condition)
        where T : unmanaged, IComponent
    {
        if (condition)
            _world.RemoveComponent<T>(_entity);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity AsPrefab(in string name)
    {
        return _world.CreatePrefab(name, _entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>()
        where T : unmanaged, IComponent
    {
        return _world.HasComponent<T>(_entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive()
    {
       return _world.IsAlive(_entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy()
    {
       _world.DestroyEntity(_entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entity(EntityCommands entityCommands)
    {
        return entityCommands._entity;
    }
}
