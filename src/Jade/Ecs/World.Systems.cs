// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Systems;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world.
/// Provides methods for managing systems within the ECS world.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Adds multiple systems to the specified system stage.
    /// </summary>
    /// <param name="stage">The stage to which the systems will be added.</param>
    /// <param name="systems">The systems to add.</param>
    public void AddSystems(SystemStage stage, params IEnumerable<SystemBase> systems)
    {
        _systemRunner.AddSystems(stage, systems);
    }

    /// <summary>
    /// Adds a new system of type <typeparamref name="T"/> to the specified system stage.
    /// </summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="stage">The stage to which the system will be added.</param>
    public void AddSystem<T>(SystemStage stage)
        where T : SystemBase, new()
    {
        _systemRunner.AddSystem(stage, new T());
    }

    /// <summary>
    /// Adds an existing system instance to the specified system stage.
    /// </summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="stage">The stage to which the system will be added.</param>
    /// <param name="system">The system instance to add.</param>
    public void AddSystem<T>(SystemStage stage, T system)
        where T : SystemBase
    {
        _systemRunner.AddSystem(stage, system);
    }

    /// <summary>
    /// Checks whether a system of type <typeparamref name="T"/> is enabled.
    /// </summary>
    /// <typeparam name="T">The type of the system to check.</typeparam>
    /// <returns><c>true</c> if the system is enabled; otherwise, <c>false</c>.</returns>
    public bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return _systemRunner.IsSystemEnabled<T>();
    }

    /// <summary>
    /// Enables or disables a system of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the system to enable or disable.</typeparam>
    /// <param name="enabled"><c>true</c> to enable the system; <c>false</c> to disable it.</param>
    public void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        _systemRunner.SetSystemEnabled<T>(enabled);
    }

    /// <summary>
    /// Retrieves a system of type <typeparamref name="T"/> from the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the system to retrieve.</typeparam>
    /// <returns>The system instance.</returns>
    public T GetSystem<T>()
        where T : SystemBase
    {
        return _systemRunner.GetSystem<T>();
    }

    /// <summary>
    /// Executes all systems in the specified system stage.
    /// </summary>
    /// <param name="stage">The stage to execute.</param>
    internal void RunStage(SystemStage stage)
    {
        _systemRunner.RunStage(stage);
    }
}
