// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Plugins;

/// <summary>
/// Represents the base class for ECS (Entity Component System) plugins.
/// Provides a method for building the ECS world using the plugin.
/// </summary>
public abstract class PluginBase
{
    /// <summary>
    /// Builds the ECS world using the plugin.
    /// Override this method to define custom logic for configuring the ECS world.
    /// </summary>
    /// <param name="world">The ECS world to configure.</param>
    public abstract void Build(World world);
}
