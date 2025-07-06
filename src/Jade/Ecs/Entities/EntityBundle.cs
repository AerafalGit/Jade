// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Entities;

/// <summary>
/// Represents a bundle of components for one entity in the ECS (Entity Component System).
/// Provides an abstract method for configuring entity commands.
/// </summary>
public abstract class EntityBundle
{
    /// <summary>
    /// Configures the entity commands for this bundle.
    /// </summary>
    /// <param name="commands">The <see cref="EntityCommands"/> instance used to configure the entity.</param>
    public abstract void Configure(EntityCommands commands);
}
