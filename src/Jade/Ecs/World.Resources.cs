// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;

namespace Jade.Ecs;

/// <summary>
/// Provides methods for managing resources within the ECS (Entity Component System) world.
/// Allows adding, retrieving, checking, and removing resources of specific types.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Attempts to retrieve a resource of type <typeparamref name="T"/> from the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class.</typeparam>
    /// <param name="resource">The retrieved resource if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the resource was found; otherwise, <c>false</c>.</returns>
    public bool TryGetResource<T>([NotNullWhen(true)] out T? resource)
        where T : class
    {
        if (_resources.TryGetValue(typeof(T), out var res))
        {
            resource = (T)res;
            return true;
        }

        resource = null;
        return false;
    }

    /// <summary>
    /// Retrieves a resource of type <typeparamref name="T"/> from the world.
    /// Throws an exception if the resource is not registered.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class.</typeparam>
    /// <returns>The resource of type <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the resource is not registered in the world.</exception>
    public T GetResource<T>()
        where T : class
    {
        if (_resources.TryGetValue(typeof(T), out var resource))
            return (T)resource;

        throw new InvalidOperationException($"Resource of type {typeof(T).Name} is not registered in the world.");
    }

    /// <summary>
    /// Adds a new resource of type <typeparamref name="T"/> to the world.
    /// The resource is initialized using its default constructor.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class with a parameterless constructor.</typeparam>
    public void AddResource<T>()
        where T : class, new()
    {
        _resources[typeof(T)] = new T();
    }

    /// <summary>
    /// Adds a resource of type <typeparamref name="T"/> to the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class.</typeparam>
    /// <param name="resource">The resource instance to add.</param>
    public void AddResource<T>(T resource)
        where T : class
    {
        _resources[typeof(T)] = resource;
    }

    /// <summary>
    /// Removes a resource of type <typeparamref name="T"/> from the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class.</typeparam>
    public void RemoveResource<T>()
        where T : class
    {
        _resources.Remove(typeof(T));
    }

    /// <summary>
    /// Checks whether a resource of type <typeparamref name="T"/> is registered in the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource, which must be a class.</typeparam>
    /// <returns><c>true</c> if the resource is registered; otherwise, <c>false</c>.</returns>
    public bool HasResource<T>()
        where T : class
    {
        return _resources.ContainsKey(typeof(T));
    }
}
