// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Events;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EventReader<T> GetSubscriber<T>()
        where T : unmanaged, IEvent
    {
        return new EventReader<T>(EventBus);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EventWriter<T> GetPublisher<T>()
        where T : unmanaged, IEvent
    {
        return new EventWriter<T>(EventBus);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SwapEvents()
    {
        EventBus.SwapEvents();
    }
}
