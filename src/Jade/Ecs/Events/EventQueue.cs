// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Events;

/// <summary>
/// Represents a queue for managing events in the ECS (Entity Component System).
/// Provides methods for adding, retrieving, and clearing events.
/// </summary>
/// <typeparam name="T">The type of events stored in the queue, which must implement <see cref="IEvent"/>.</typeparam>
internal sealed class EventQueue<T> : IEventQueue
    where T : IEvent
{
    private readonly Queue<T> _queue;

    /// <summary>
    /// Gets the number of events currently in the queue.
    /// </summary>
    public bool IsEmpty =>
        _queue.Count is 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventQueue{T}"/> class.
    /// </summary>
    public EventQueue()
    {
        _queue = [];
    }

    /// <summary>
    /// Adds an event to the queue.
    /// </summary>
    /// <param name="event">The event to enqueue.</param>
    public void Enqueue(T @event)
    {
        _queue.Enqueue(@event);
    }

    /// <summary>
    /// Removes and retrieves the event at the front of the queue.
    /// </summary>
    /// <returns>The event at the front of the queue.</returns>
    public T Dequeue()
    {
        return _queue.Dequeue();
    }

    /// <summary>
    /// Clears all events from the queue.
    /// </summary>
    public void Clear()
    {
        _queue.Clear();
    }
}
