// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides lifecycle methods for systems and access to the ECS world.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Gets or sets the ECS world associated with this system.
    /// </summary>
    protected internal World World { get; set; } = null!;

    /// <summary>
    /// Called when the system is started up.
    /// Override this method to define custom startup logic.
    /// </summary>
    public virtual void Startup()
    {
    }

    /// <summary>
    /// Called before the system's update logic is executed.
    /// Override this method to define custom pre-update logic.
    /// </summary>
    public virtual void PreUpdate()
    {
    }

    /// <summary>
    /// Called to execute the system's update logic.
    /// Override this method to define custom update logic.
    /// </summary>
    public virtual void Update()
    {
    }

    /// <summary>
    /// Called after the system's update logic is executed.
    /// Override this method to define custom post-update logic.
    /// </summary>
    public virtual void PostUpdate()
    {
    }

    /// <summary>
    /// Called to execute fixed update logic, typically used for physics updates.
    /// Override this method to define custom fixed update logic.
    /// </summary>
    public virtual void FixedUpdate()
    {
    }

    /// <summary>
    /// Called to execute rendering logic for the system.
    /// Override this method to define custom rendering logic.
    /// </summary>
    public virtual void Render()
    {
    }

    /// <summary>
    /// Called when the system is cleaned up.
    /// Override this method to define custom cleanup logic.
    /// </summary>
    public virtual void Cleanup()
    {
    }
}
