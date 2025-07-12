// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? GetResource<T>()
        where T : class
    {
        return _resources.GetValueOrDefault(typeof(T)) as T;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetResource<T>([NotNullWhen(true)] out T? resource)
        where T : class
    {
        return (resource = _resources.GetValueOrDefault(typeof(T)) as T) is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetRequiredResource<T>()
        where T : class
    {
        if (_resources.TryGetValue(typeof(T), out var resource))
            return (T)resource;

        throw new KeyNotFoundException($"Resource of type {typeof(T).Name} not found.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T AddResource<T>()
        where T : class, new()
    {
        if (_resources.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Resource of type {typeof(T).Name} already exists.");

        var resource = new T();
        _resources[typeof(T)] = resource;
        return resource;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T SetResource<T>(T resource)
        where T : class
    {
        _resources[typeof(T)] = resource;
        return resource;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveResource<T>()
        where T : class
    {
        return _resources.Remove(typeof(T));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveResource<T>(out T? resource)
        where T : class
    {
        resource = null;

        if (_resources.Remove(typeof(T), out var res))
            return (resource = res as T) is not null;

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasResource<T>()
        where T : class
    {
        return _resources.ContainsKey(typeof(T));
    }
}
