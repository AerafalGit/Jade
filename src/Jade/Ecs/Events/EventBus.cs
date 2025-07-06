// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Events;

/// <summary>
/// Represents an event bus for managing events in the ECS (Entity Component System).
/// Provides methods for sending, reading, and swapping events between buffers.
/// </summary>
public sealed class EventBus : IDisposable
{
    private readonly Dictionary<Type, IEventQueue> _eventsA;
    private readonly Dictionary<Type, IEventQueue> _eventsB;

    private Dictionary<Type, IEventQueue> _writingBuffer;
    private Dictionary<Type, IEventQueue> _readingBuffer;

    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBus"/> class.
    /// </summary>
    public EventBus()
    {
        _eventsA = [];
        _eventsB = [];
        _writingBuffer = _eventsA;
        _readingBuffer = _eventsB;
    }

    /// <summary>
    /// Finalizes the <see cref="EventBus"/> instance and disposes of its resources.
    /// </summary>
    ~EventBus()
    {
        Dispose();
    }

    /// <summary>
    /// Sends an event to the writing buffer.
    /// </summary>
    /// <typeparam name="T">The type of the event, which must implement <see cref="IEvent"/>.</typeparam>
    /// <param name="event">The event to send.</param>
    public void Send<T>(T @event)
        where T : IEvent
    {
        var eventType = typeof(T);

        if (!_writingBuffer.TryGetValue(eventType, out var queue))
            _writingBuffer[eventType] = queue = new EventQueue<T>();

        ((EventQueue<T>)queue).Enqueue(@event);
    }

    /// <summary>
    /// Reads events of a specific type from the reading buffer.
    /// </summary>
    /// <typeparam name="T">The type of the events, which must implement <see cref="IEvent"/>.</typeparam>
    /// <returns>An enumerable of events of the specified type.</returns>
    public IEnumerable<T> Read<T>()
        where T : IEvent
    {
        if (!_readingBuffer.TryGetValue(typeof(T), out var buffer))
            yield break;

        var queue = (EventQueue<T>)buffer;

        while (!queue.IsEmpty)
            yield return queue.Dequeue();
    }

    /// <summary>
    /// Swaps the writing and reading buffers and clears the new writing buffer.
    /// </summary>
    internal void SwapEvents()
    {
        (_writingBuffer, _readingBuffer) = (_readingBuffer, _writingBuffer);

        foreach (var buffer in _writingBuffer.Values)
            buffer.Clear();
    }

    /// <summary>
    /// Disposes of the resources used by the event bus.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        _eventsA.Clear();
        _eventsB.Clear();

        GC.SuppressFinalize(this);
    }
}
