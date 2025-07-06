// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Events;

/// <summary>
/// Provides a writer for events of a specific type in the ECS (Entity Component System).
/// Allows sending events to the event bus.
/// </summary>
/// <typeparam name="T">The type of events to write, which must implement <see cref="IEvent"/>.</typeparam>
public sealed class EventWriter<T>
    where T : IEvent
{
    private readonly EventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventWriter{T}"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus to send events to.</param>
    internal EventWriter(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    /// <summary>
    /// Sends an event of type <typeparamref name="T"/> to the event bus.
    /// </summary>
    /// <param name="event">The event to send.</param>
    public void Write(T @event)
    {
        _eventBus.Send(@event);
    }
}
