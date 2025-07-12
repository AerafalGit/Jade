// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    [UnscopedRef]
    public ref Query Any<T>()
        where T : unmanaged, IComponent
    {
        _any = _any.With(Component<T>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5, T6>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id)
            .With(Component<T9>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query Any<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
        where T10 : unmanaged, IComponent
    {
        _any = _any
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id)
            .With(Component<T9>.Metadata.Id)
            .With(Component<T10>.Metadata.Id);
        return ref this;
    }
}
