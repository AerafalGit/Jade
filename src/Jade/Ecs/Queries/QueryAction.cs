// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions;

namespace Jade.Ecs.Queries;

public delegate void QueryAction<T1>(ref T1 component1)
    where T1 : unmanaged;

public delegate void QueryAction<T1, T2>(ref T1 component1, ref T2 component2)
    where T1 : unmanaged
    where T2 : unmanaged;

public delegate void QueryAction<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5, T6>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged
    where T9 : unmanaged;

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9, ref T10 component10)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged
    where T9 : unmanaged
    where T10 : unmanaged;

public delegate void QueryEntityAction<T1>(in Entity entity, ref T1 component1)
    where T1 : unmanaged;

public delegate void QueryEntityAction<T1, T2>(in Entity entity, ref T1 component1, ref T2 component2)
    where T1 : unmanaged
    where T2 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5, T6>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5, T6, T7>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged
    where T9 : unmanaged;

public delegate void QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9, ref T10 component10)
    where T1 : unmanaged
    where T2 : unmanaged
    where T3 : unmanaged
    where T4 : unmanaged
    where T5 : unmanaged
    where T6 : unmanaged
    where T7 : unmanaged
    where T8 : unmanaged
    where T9 : unmanaged
    where T10 : unmanaged;
