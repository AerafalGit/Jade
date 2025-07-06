// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for subscribing to and publishing events within the ECS world.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Retrieves an event reader for subscribing to events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the event to subscribe to.</typeparam>
    /// <returns>An <see cref="EventReader{T}"/> instance for reading events.</returns>
    protected EventReader<T> GetSubscriber<T>()
        where T : IEvent
    {
        return World.GetSubscriber<T>();
    }

    /// <summary>
    /// Retrieves an event writer for publishing events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the event to publish.</typeparam>
    /// <returns>An <see cref="EventWriter{T}"/> instance for writing events.</returns>
    protected EventWriter<T> GetPublisher<T>()
        where T : IEvent
    {
        return World.GetPublisher<T>();
    }
}
