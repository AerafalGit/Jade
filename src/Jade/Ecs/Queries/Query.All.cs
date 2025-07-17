// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    [UnscopedRef]
    public ref Query All<T>()
    {
        _all = _all.With(Component<T>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2>()
    {
        _all = _all
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2, T3>()
    {
        _all = _all
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2, T3, T4>()
    {
        _all = _all
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2, T3, T4, T5>()
    {
        _all = _all
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2, T3, T4, T5, T6>()
    {
        _all = _all
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id);
        return ref this;
    }

    [UnscopedRef]
    public ref Query All<T1, T2, T3, T4, T5, T6, T7>()
    {
        _all = _all
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
    public ref Query All<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        _all = _all
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
    public ref Query All<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        _all = _all
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
    public ref Query All<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        _all = _all
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
