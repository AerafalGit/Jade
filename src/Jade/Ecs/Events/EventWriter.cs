// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Events;

public sealed class EventWriter<T>
    where T : unmanaged, IEvent
{
    private readonly EventBus _eventBus;

    internal EventWriter(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(T @event)
    {
        _eventBus.Send(@event);
    }
}
