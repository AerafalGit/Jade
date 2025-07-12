// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Jade.Ecs.Components;

[StructLayout(LayoutKind.Sequential, Size = 32)]
internal readonly struct ComponentMask :
    IEquatable<ComponentMask>,
    IBitwiseOperators<ComponentMask, ComponentMask, ComponentMask>,
    IEqualityOperators<ComponentMask, ComponentMask, bool>
{
    private static readonly Vector256<ulong> s_zero = Vector256<ulong>.Zero;

    private const byte Div64 = 6;

    private const byte Mod64 = 63;

    private const int MaxComponentId = MaxComponents - 1;

    public const int MaxComponents = 256;

    private readonly Vector256<ulong> _bits;

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _bits.Equals(s_zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask()
    {
        _bits = Vector256<ulong>.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComponentMask(Vector256<ulong> bits)
    {
        _bits = bits;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask With(in ComponentId componentId)
    {
        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue | (1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Without(in ComponentId componentId)
    {
        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue & ~(1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(in ComponentId componentId)
    {
        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        return (_bits.GetElement(wordIndex) & (1UL << bitIndex)) is not 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasAll(in ComponentMask other)
    {
        if (Avx2.IsSupported)
        {
            var and = Avx2.And(_bits, other._bits);
            var cmp = Avx2.CompareEqual(and, other._bits);
            return (uint)Avx2.MoveMask(cmp.AsByte()) is uint.MaxValue;
        }

        for (var i = 0; i < Vector256<ulong>.Count; i++)
        {
            if ((_bits.GetElement(i) & other._bits.GetElement(i)) != other._bits.GetElement(i))
                return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasAny(in ComponentMask other)
    {
        if (Avx2.IsSupported)
            return !Avx.TestZ(_bits, other._bits);

        for (var i = 0; i < Vector256<ulong>.Count; i++)
        {
            if ((_bits.GetElement(i) & other._bits.GetElement(i)) is not 0)
                return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSubsetOf(in ComponentMask other)
    {
        return (_bits & other._bits).Equals(_bits);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(in ComponentMask other)
    {
        return HasAny(other);
    }

    public int PopCount()
    {
        if (Popcnt.X64.IsSupported)
        {
            var bits = _bits.As<ulong, ulong>();

            return (int)(Popcnt.X64.PopCount(bits.GetElement(0)) +
                    Popcnt.X64.PopCount(bits.GetElement(1)) +
                    Popcnt.X64.PopCount(bits.GetElement(2)) +
                    Popcnt.X64.PopCount(bits.GetElement(3)));
        }

        var count = 0;

        for (var i = 0; i < MaxComponents; i++)
        {
            if (Has(i))
                count++;
        }

        return count;
    }

    public ComponentId FirstSetBit()
    {
        if (Bmi1.X64.IsSupported)
        {
            var bits = _bits.As<ulong, ulong>();

            for (var i = 0; i < 4; i++)
            {
                var bit = bits.GetElement(i);

                if (bit is not 0)
                    return new ComponentId(i * 64 + (int)Bmi1.X64.TrailingZeroCount(bit));
            }
        }

        for (var i = 0; i < MaxComponents; i++)
        {
            var id = new ComponentId(i);

            if (Has(id))
                return id;
        }

        return new ComponentId(-1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ComponentMask other)
    {
        return _bits.Equals(other._bits);
    }

    public override bool Equals(object? obj)
    {
        return obj is ComponentMask mask && Equals(mask);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(_bits.GetElement(0), _bits.GetElement(1));
    }

    public static bool operator ==(ComponentMask left, ComponentMask right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ComponentMask left, ComponentMask right)
    {
        return !left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMask operator &(ComponentMask left, ComponentMask right)
    {
        if (Avx2.IsSupported)
            return new ComponentMask(Avx2.And(left._bits, right._bits));

        var result = new ulong[Vector256<ulong>.Count];

        for (var i = 0; i < Vector256<ulong>.Count; i++)
            result[i] = left._bits.GetElement(i) & right._bits.GetElement(i);

        return new ComponentMask(Vector256.Create(result[0], result[1], result[2], result[3]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMask operator |(ComponentMask left, ComponentMask right)
    {
        if (Avx2.IsSupported)
            return new ComponentMask(Avx2.Or(left._bits, right._bits));

        var result = new ulong[Vector256<ulong>.Count];

        for (var i = 0; i < Vector256<ulong>.Count; i++)
            result[i] = left._bits.GetElement(i) | right._bits.GetElement(i);

        return new ComponentMask(Vector256.Create(result[0], result[1], result[2], result[3]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMask operator ^(ComponentMask left, ComponentMask right)
    {
        if (Avx2.IsSupported)
            return new ComponentMask(Avx2.Xor(left._bits, right._bits));

        var result = new ulong[Vector256<ulong>.Count];

        for (var i = 0; i < Vector256<ulong>.Count; i++)
            result[i] = left._bits.GetElement(i) ^ right._bits.GetElement(i);

        return new ComponentMask(Vector256.Create(result[0], result[1], result[2], result[3]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMask operator ~(ComponentMask value)
    {
        if (Avx2.IsSupported)
            return new ComponentMask(Avx2.Xor(value._bits, Avx2.CompareEqual(Vector256<ulong>.Zero, Vector256<ulong>.Zero)));

        var result = new ulong[Vector256<ulong>.Count];

        for (var i = 0; i < Vector256<ulong>.Count; i++)
            result[i] = ~value._bits.GetElement(i);

        return new ComponentMask(Vector256.Create(result[0], result[1], result[2], result[3]));
    }
}
