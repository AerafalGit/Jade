// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public Components(ref T1 component1, ref T2 component2)
    {
        Component1 = ref component1;
        Component2 = ref component2;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5, T6>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public ref T6 Component6;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5, T6, T7>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public ref T6 Component6;

    public ref T7 Component7;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
        Component7 = ref component7;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public ref T6 Component6;

    public ref T7 Component7;

    public ref T8 Component8;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
        Component7 = ref component7;
        Component8 = ref component8;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public ref T6 Component6;

    public ref T7 Component7;

    public ref T8 Component8;

    public ref T9 Component9;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
        Component7 = ref component7;
        Component8 = ref component8;
        Component9 = ref component9;
    }
}

[StructLayout(LayoutKind.Sequential)]
public ref struct Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    public ref T1 Component1;

    public ref T2 Component2;

    public ref T3 Component3;

    public ref T4 Component4;

    public ref T5 Component5;

    public ref T6 Component6;

    public ref T7 Component7;

    public ref T8 Component8;

    public ref T9 Component9;

    public ref T10 Component10;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9, ref T10 component10)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
        Component7 = ref component7;
        Component8 = ref component8;
        Component9 = ref component9;
        Component10 = ref component10;
    }
}
