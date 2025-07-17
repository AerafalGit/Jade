// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Queries;

public delegate void QueryAction<T1>(ref T1 component1);

public delegate void QueryAction<T1, T2>(ref T1 component1, ref T2 component2);

public delegate void QueryAction<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3);

public delegate void QueryAction<T1, T2, T3, T4>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);

public delegate void QueryAction<T1, T2, T3, T4, T5>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5);

public delegate void QueryAction<T1, T2, T3, T4, T5, T6>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6);

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7);

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8);

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9);

public delegate void QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9, ref T10 component10);
