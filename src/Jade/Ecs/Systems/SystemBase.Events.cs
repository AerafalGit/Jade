// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Events;

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected EventReader<T> GetSubscriber<T>()
        where T : unmanaged, IEvent
    {
        return World.GetSubscriber<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected EventWriter<T> GetPublisher<T>()
        where T : unmanaged, IEvent
    {
        return World.GetPublisher<T>();
    }
}
