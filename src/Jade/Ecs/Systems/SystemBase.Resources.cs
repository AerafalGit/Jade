// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS systems, providing resource management functionality.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Attempts to retrieve a resource of the specified type from the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to retrieve.</typeparam>
    /// <param name="resource">The retrieved resource if found; otherwise, null.</param>
    /// <returns>True if the resource was successfully retrieved; otherwise, false.</returns>
    protected bool TryGetResource<T>([NotNullWhen(true)] out T? resource)
        where T : class
    {
        return World.TryGetResource(out resource);
    }

    /// <summary>
    /// Retrieves a resource of the specified type from the world.
    /// Throws an exception if the resource is not found.
    /// </summary>
    /// <typeparam name="T">The type of the resource to retrieve.</typeparam>
    /// <returns>The retrieved resource.</returns>
    protected T GetResource<T>()
        where T : class
    {
        return World.GetResource<T>();
    }

    /// <summary>
    /// Adds a new resource of the specified type to the world.
    /// The resource is created using its default constructor.
    /// </summary>
    /// <typeparam name="T">The type of the resource to add.</typeparam>
    protected void AddResource<T>()
        where T : class, new()
    {
        World.AddResource<T>();
    }

    /// <summary>
    /// Adds the specified resource to the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to add.</typeparam>
    /// <param name="resource">The resource instance to add.</param>
    protected void AddResource<T>(T resource)
        where T : class
    {
        World.AddResource(resource);
    }

    /// <summary>
    /// Removes a resource of the specified type from the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to remove.</typeparam>
    /// <returns>True if the resource was successfully removed; otherwise, false.</returns>
    protected bool RemoveResource<T>()
        where T : class
    {
        return World.RemoveResource<T>();
    }

    /// <summary>
    /// Checks if a resource of the specified type exists in the world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to check.</typeparam>
    /// <returns>True if the resource exists; otherwise, false.</returns>
    protected bool HasResource<T>()
        where T : class
    {
        return World.HasResource<T>();
    }
}
