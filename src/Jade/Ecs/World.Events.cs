// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Ecs;

/// <summary>
/// Provides event handling functionality within the ECS (Entity Component System) world.
/// Allows subscribing to and publishing events, as well as swapping event buffers.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Retrieves an event reader for the specified event type.
    /// The reader allows subscribing to events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the event, which must implement <see cref="IEvent"/>.</typeparam>
    /// <returns>An <see cref="EventReader{T}"/> instance for reading events.</returns>
    public EventReader<T> GetSubscriber<T>()
        where T : IEvent
    {
        return new EventReader<T>(_eventBus);
    }

    /// <summary>
    /// Retrieves an event writer for the specified event type.
    /// The writer allows publishing events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the event, which must implement <see cref="IEvent"/>.</typeparam>
    /// <returns>An <see cref="EventWriter{T}"/> instance for writing events.</returns>
    public EventWriter<T> GetPublisher<T>()
        where T : IEvent
    {
        return new EventWriter<T>(_eventBus);
    }

    /// <summary>
    /// Swaps the event buffers in the event bus.
    /// This operation is typically used to process events in a double-buffered system.
    /// </summary>
    internal void SwapEvents()
    {
        _eventBus.SwapEvents();
    }
}
