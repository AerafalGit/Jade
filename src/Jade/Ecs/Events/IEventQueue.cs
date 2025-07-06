// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Events;

/// <summary>
/// Defines the interface for an event queue in the ECS (Entity Component System).
/// Provides a method to clear all events from the queue.
/// </summary>
public interface IEventQueue
{
    /// <summary>
    /// Clears all events from the queue.
    /// </summary>
    void Clear();
}
