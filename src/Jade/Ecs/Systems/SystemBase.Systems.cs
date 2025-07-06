// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for enabling, disabling, and retrieving systems within the ECS world.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Checks whether a system of type <typeparamref name="T"/> is enabled.
    /// </summary>
    /// <typeparam name="T">The type of the system to check.</typeparam>
    /// <returns><c>true</c> if the system is enabled; otherwise, <c>false</c>.</returns>
    protected bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return World.IsSystemEnabled<T>();
    }

    /// <summary>
    /// Enables or disables a system of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the system to enable or disable.</typeparam>
    /// <param name="enabled"><c>true</c> to enable the system; <c>false</c> to disable it.</param>
    protected void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        World.SetSystemEnabled<T>(enabled);
    }

    /// <summary>
    /// Enables a system of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the system to enable.</typeparam>
    protected void EnableSystem<T>()
        where T : SystemBase
    {
        World.SetSystemEnabled<T>(true);
    }

    /// <summary>
    /// Disables a system of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the system to disable.</typeparam>
    protected void DisableSystem<T>()
        where T : SystemBase
    {
        World.SetSystemEnabled<T>(false);
    }

    /// <summary>
    /// Retrieves a system of type <typeparamref name="T"/> from the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the system to retrieve.</typeparam>
    /// <returns>The system instance.</returns>
    protected T GetSystem<T>()
        where T : SystemBase
    {
        return World.GetSystem<T>();
    }
}
