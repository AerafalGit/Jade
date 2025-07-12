// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Bundles;

public sealed class BundleBuilder
{
    private readonly List<Action<World, Entity>> _componentAdders;

    public BundleBuilder()
    {
        _componentAdders = [];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder With<T>(T component = default)
        where T : unmanaged, IComponent
    {
        _componentAdders.Add((world, entity) => world.AddComponent(entity, component));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder WithIf<T>(bool condition, T component = default)
        where T : unmanaged, IComponent
    {
        if (condition)
            _componentAdders.Add((world, entity) => world.AddComponent(entity, component));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder With<T>(Func<T> componentFactory)
        where T : unmanaged, IComponent
    {
        _componentAdders.Add((world, entity) => world.AddComponent(entity, componentFactory()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder WithIf<T>(bool condition, Func<T> componentFactory)
        where T : unmanaged, IComponent
    {
        if (condition)
            _componentAdders.Add((world, entity) => world.AddComponent(entity, componentFactory()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder Without<T>()
        where T : unmanaged, IComponent
    {
        _componentAdders.Add((world, entity) => world.RemoveComponent<T>(entity));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder WithoutIf<T>(bool condition)
        where T : unmanaged, IComponent
    {
        if (condition)
            _componentAdders.Add((world, entity) => world.RemoveComponent<T>(entity));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BundleBuilder Merge(BundleBuilder other)
    {
        _componentAdders.AddRange(other._componentAdders);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DynamicBundle Build()
    {
        return new DynamicBundle([.. _componentAdders]);
    }
}
