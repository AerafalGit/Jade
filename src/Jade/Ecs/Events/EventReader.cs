// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Collections;

namespace Jade.Ecs.Events;

public sealed class EventReader<T> : IEnumerable<T>
    where T : unmanaged, IEvent
{
    private readonly EventBus _eventBus;

    internal EventReader(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _eventBus.Read<T>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
