// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Collections;

namespace Jade.Ecs.Events;

/// <summary>
/// Provides a reader for events of a specific type in the ECS (Entity Component System).
/// Allows enumeration over events of the specified type.
/// </summary>
/// <typeparam name="T">The type of events to read, which must implement <see cref="IEvent"/>.</typeparam>
public sealed class EventReader<T> : IEnumerable<T>
    where T : IEvent
{
    private readonly EventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventReader{T}"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus to read events from.</param>
    internal EventReader(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the events of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>An enumerator for the events of type <typeparamref name="T"/>.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _eventBus.Read<T>().GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the events.
    /// </summary>
    /// <returns>An enumerator for the events.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
