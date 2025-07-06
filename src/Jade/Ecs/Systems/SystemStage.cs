// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the various stages in the lifecycle of ECS (Entity Component System) systems.
/// Defines the order in which systems are executed during the application's runtime.
/// </summary>
[Flags]
public enum SystemStage
{
    /// <summary>
    /// Represents no specific stage. This can be used to indicate that a system does not belong to any stage.
    /// </summary>
    None = 0,

    /// <summary>
    /// The stage where systems perform initialization tasks before the main application loop starts.
    /// </summary>
    Startup = 1,

    /// <summary>
    /// The stage where systems execute tasks before the main update logic.
    /// </summary>
    PreUpdate = 2,

    /// <summary>
    /// The primary update stage where systems perform their main logic.
    /// </summary>
    Update = 4,

    /// <summary>
    /// The stage where systems execute tasks after the main update logic.
    /// </summary>
    PostUpdate = 8,

    /// <summary>
    /// The stage where systems perform fixed interval updates, typically used for physics calculations.
    /// </summary>
    FixedUpdate = 16,

    /// <summary>
    /// The stage where systems handle rendering tasks.
    /// </summary>
    Render = 32,

    /// <summary>
    /// The stage where systems perform cleanup tasks after the application loop ends.
    /// </summary>
    Cleanup = 64
}
