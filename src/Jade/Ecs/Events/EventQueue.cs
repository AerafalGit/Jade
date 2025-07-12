// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Events;

internal sealed class EventQueue<T> : IEventQueue
    where T : unmanaged, IEvent
{
    private readonly Queue<T> _queue;

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _queue.Count is 0;
    }

    public EventQueue()
    {
        _queue = [];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Enqueue(T @event)
    {
        _queue.Enqueue(@event);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Dequeue()
    {
        return _queue.Dequeue();
    }

    public void Clear()
    {
        _queue.Clear();
    }
}
