// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Jade.Ecs.Components;

namespace Jade.Ecs;

public readonly partial struct Entity
{
    public void Set(in ComponentMask mask)
    {
        World.Instance.Set(in this, in mask);
    }

    public void Set<T1>(in T1? component = default)
    {
        World.Instance.Set(in this, in component);
    }

    public void Set<T1, T2>(
        in T1? component1 = default,
        in T2? component2 = default)
    {
        World.Instance.Set(in this, in component1, in component2);
    }

    public void Set<T1, T2, T3>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3);
    }

    public void Set<T1, T2, T3, T4>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4);
    }

    public void Set<T1, T2, T3, T4, T5>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5);
    }

    public void Set<T1, T2, T3, T4, T5, T6>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5, in component6);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5, in component6, in component7);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, in component9);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default,
        in T10? component10 = default)
    {
        World.Instance.Set(in this, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, in component9, in component10);
    }

    public void Remove(in ComponentMask mask)
    {
        World.Instance.Remove(in this, in mask);
    }

    public void Remove<T1>()
    {
        World.Instance.Remove<T1>(in this);
    }

    public void Remove<T1, T2>()
    {
        World.Instance.Remove<T1, T2>(in this);
    }

    public void Remove<T1, T2, T3>()
    {
        World.Instance.Remove<T1, T2, T3>(in this);
    }

    public void Remove<T1, T2, T3, T4>()
    {
        World.Instance.Remove<T1, T2, T3, T4>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5, T6>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5, T6>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5, T6, T7>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5, T6, T7, T8>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in this);
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        World.Instance.Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in this);
    }

    public bool Has(in ComponentMask mask)
    {
        return World.Instance.Has(in this, in mask);
    }

    public bool Has<T1>()
    {
        return World.Instance.Has<T1>(in this);
    }

    public bool Has<T1, T2>()
    {
        return World.Instance.Has<T1, T2>(in this);
    }

    public bool Has<T1, T2, T3>()
    {
        return World.Instance.Has<T1, T2, T3>(in this);
    }

    public bool Has<T1, T2, T3, T4>()
    {
        return World.Instance.Has<T1, T2, T3, T4>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5, T6>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5, T6>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5, T6, T7>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5, T6, T7, T8>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in this);
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        return World.Instance.Has<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in this);
    }

    [UnscopedRef]
    public ref T1 Get<T1>()
    {
        return ref World.Instance.Get<T1>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2> Get<T1, T2>()
    {
        return World.Instance.Get<T1, T2>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3> Get<T1, T2, T3>()
    {
        return World.Instance.Get<T1, T2, T3>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4> Get<T1, T2, T3, T4>()
    {
        return World.Instance.Get<T1, T2, T3, T4>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5> Get<T1, T2, T3, T4, T5>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6> Get<T1, T2, T3, T4, T5, T6>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5, T6>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7> Get<T1, T2, T3, T4, T5, T6, T7>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5, T6, T7>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8> Get<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5, T6, T7, T8>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in this);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        return World.Instance.Get<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in this);
    }

    [UnscopedRef]
    public ref T1 TryGet<T1>(out bool exists)
    {
        return ref World.Instance.TryGet<T1>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2> TryGet<T1, T2>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3> TryGet<T1, T2, T3>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4> TryGet<T1, T2, T3, T4>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5> TryGet<T1, T2, T3, T4, T5>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6> TryGet<T1, T2, T3, T4, T5, T6>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5, T6>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7> TryGet<T1, T2, T3, T4, T5, T6, T7>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5, T6, T7>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8> TryGet<T1, T2, T3, T4, T5, T6, T7, T8>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5, T6, T7, T8>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9> TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in this, out exists);
    }

    [UnscopedRef]
    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out bool exists)
    {
        return World.Instance.TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in this, out exists);
    }
}
