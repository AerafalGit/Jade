// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Events;

internal sealed class EventBus : IDisposable
{
    private readonly Dictionary<Type, IEventQueue> _eventsA;
    private readonly Dictionary<Type, IEventQueue> _eventsB;
    private readonly ReaderWriterLockSlim _lock;

    private Dictionary<Type, IEventQueue> _writingBuffer;
    private Dictionary<Type, IEventQueue> _readingBuffer;

    public EventBus()
    {
        _eventsA = [];
        _eventsB = [];
        _writingBuffer = _eventsA;
        _readingBuffer = _eventsB;
        _lock = new ReaderWriterLockSlim();
    }

    ~EventBus()
    {
        ReleaseUnmanagedResources();
    }

    public void Send<T>(T @event)
        where T : unmanaged, IEvent
    {
        _lock.EnterWriteLock();

        try
        {
            var eventType = typeof(T);

            if (!_writingBuffer.TryGetValue(eventType, out var queue))
                _writingBuffer[eventType] = queue = new EventQueue<T>();

            ((EventQueue<T>)queue).Enqueue(@event);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public IEnumerable<T> Read<T>()
        where T : unmanaged, IEvent
    {
        _lock.EnterReadLock();

        try
        {
            if (!_readingBuffer.TryGetValue(typeof(T), out var buffer))
                yield break;

            var queue = (EventQueue<T>)buffer;

            while (!queue.IsEmpty)
                yield return queue.Dequeue();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void SwapEvents()
    {
        _lock.EnterWriteLock();

        try
        {
            (_writingBuffer, _readingBuffer) = (_readingBuffer, _writingBuffer);

            foreach (var buffer in _writingBuffer.Values)
                buffer.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void ReleaseUnmanagedResources()
    {
        _lock.EnterWriteLock();

        try
        {
            _eventsA.Clear();
            _eventsB.Clear();
            _writingBuffer.Clear();
            _readingBuffer.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
            _lock.Dispose();
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
