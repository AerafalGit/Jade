// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents a mask for components in the ECS (Entity Component System).
/// Provides methods for manipulating and querying component masks using bitwise operations.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 32)]
public readonly partial struct ComponentMask :
    IEquatable<ComponentMask>,
    IBitwiseOperators<ComponentMask, ComponentMask, ComponentMask>,
    IEqualityOperators<ComponentMask, ComponentMask, bool>
{
    private static readonly Vector256<ulong> s_zero = Vector256<ulong>.Zero;

    /// <summary>
    /// The divisor used for dividing by 64.
    /// </summary>
    private const byte Div64 = 6;

    /// <summary>
    /// The modulus used for calculating remainders when dividing by 64.
    /// </summary>
    private const byte Mod64 = 63;

    /// <summary>
    /// The maximum number of components supported by the mask.
    /// </summary>
    public const int MaxComponents = 256;

    private readonly Vector256<ulong> _bits;

    /// <summary>
    /// Gets a value indicating whether the mask is empty (contains no components).
    /// </summary>
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _bits.Equals(s_zero);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentMask"/> struct with an empty mask.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask()
    {
        _bits = Vector256<ulong>.Zero;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentMask"/> struct with the specified bits.
    /// </summary>
    /// <param name="bits">The vector containing the bits for the mask.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComponentMask(Vector256<ulong> bits)
    {
        _bits = bits;
    }

    /// <summary>
    /// Returns a new mask with the specified component ID added.
    /// </summary>
    /// <param name="componentId">The ID of the component to add.</param>
    /// <returns>A new <see cref="ComponentMask"/> with the component added.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the component ID is negative or exceeds the maximum number of components.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask With(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(componentId, MaxComponents);
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);

        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue | (1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    /// <summary>
    /// Returns a new mask with the specified component ID removed.
    /// </summary>
    /// <param name="componentId">The ID of the component to remove.</param>
    /// <returns>A new <see cref="ComponentMask"/> with the component removed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the component ID is negative or exceeds the maximum number of components.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Without(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(componentId, MaxComponents);
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);

        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue & ~(1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    /// <summary>
    /// Determines whether the mask contains the specified component ID.
    /// </summary>
    /// <param name="componentId">The ID of the component to check.</param>
    /// <returns><c>true</c> if the mask contains the component; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the component ID is negative or exceeds the maximum number of components.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(componentId, MaxComponents);
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);

        var wordIndex = componentId >> Div64;
        var bitIndex = componentId & Mod64;

        return (_bits.GetElement(wordIndex) & (1UL << bitIndex)) is not 0;
    }

    /// <summary>
    /// Determines whether the mask contains all components in another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if the mask contains all components in the other mask; otherwise, <c>false</c>.</returns>
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

    /// <summary>
    /// Determines whether the mask contains any components in another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if the mask contains any components in the other mask; otherwise, <c>false</c>.</returns>
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

    /// <summary>
    /// Retrieves all component IDs present in the mask.
    /// </summary>
    /// <returns>An array of integers representing the component IDs.</returns>
    public int[] GetComponents()
    {
        var count = PopCount();

        if (count is 0)
            return [];

        var components = new int[count];
        var index = 0;

        if (Avx2.IsSupported)
        {
            var bits = _bits.As<ulong, ulong>();

            for (var i = 0; i < 4; i++)
            {
                var bit = bits.GetElement(i);

                if (bit is not 0)
                {
                    for (var j = 0; j < 64; j++)
                    {
                        if ((bit & (1UL << j)) is not 0)
                        {
                            if (index < components.Length)
                                components[index++] = i * 64 + j;
                        }
                    }
                }
            }

            return components;
        }

        for (var i = 0; i < MaxComponents && index < components.Length; i++)
        {
            if (Has(i))
                components[index++] = i;
        }

        return components;
    }

    /// <summary>
    /// Counts the number of set bits in the mask.
    /// </summary>
    /// <returns>The number of set bits in the mask.</returns>
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

    /// <summary>
    /// Finds the index of the first set bit in the mask.
    /// </summary>
    /// <returns>
    /// The index of the first set bit, or -1 if no bits are set.
    /// </returns>
    public int FirstSetBit()
    {
        if (Bmi1.X64.IsSupported)
        {
            var bits = _bits.As<ulong, ulong>();

            for (var i = 0; i < 4; i++)
            {
                var bit = bits.GetElement(i);

                if (bit is not 0)
                    return i * 64 + (int)Bmi1.X64.TrailingZeroCount(bit);
            }
        }

        for (var i = 0; i < MaxComponents; i++)
        {
            if (Has(i))
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Adds a component to the mask.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new mask with the component added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask And<T>()
    {
        return With(Component<T>.Id);
    }

    /// <summary>
    /// Removes a component from the mask.
    /// </summary>
    /// <typeparam name="T">The type of the component to remove.</typeparam>
    /// <returns>A new mask with the component removed.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask But<T>()
    {
        return Without(Component<T>.Id);
    }

    /// <summary>
    /// Determines whether the mask is a subset of another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns>
    /// <c>true</c> if the mask is a subset of the other mask; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSubsetOf(ComponentMask other)
    {
        return other.HasAll(in this);
    }

    /// <summary>
    /// Determines whether the mask is a superset of another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns>
    /// <c>true</c> if the mask is a superset of the other mask; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSupersetOf(ComponentMask other)
    {
        return HasAll(other);
    }

    /// <summary>
    /// Determines whether the mask overlaps with another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns>
    /// <c>true</c> if the mask overlaps with the other mask; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(ComponentMask other)
    {
        return HasAny(other);
    }

    /// <summary>
    /// Computes the union of the mask with another mask.
    /// </summary>
    /// <param name="other">The other mask to union with.</param>
    /// <returns>A new mask representing the union.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Union(ComponentMask other)
    {
        return this | other;
    }

    /// <summary>
    /// Computes the intersection of the mask with another mask.
    /// </summary>
    /// <param name="other">The other mask to intersect with.</param>
    /// <returns>A new mask representing the intersection.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Intersect(ComponentMask other)
    {
        return this & other;
    }

    /// <summary>
    /// Computes the difference between the mask and another mask.
    /// </summary>
    /// <param name="other">The other mask to subtract.</param>
    /// <returns>A new mask representing the difference.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Except(ComponentMask other)
    {
        return this & ~other;
    }

    /// <summary>
    /// Determines whether the current mask is equal to another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if the masks are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ComponentMask other)
    {
        return _bits.Equals(other._bits);
    }

    /// <summary>
    /// Determines whether the current mask is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><c>true</c> if the object is a <see cref="ComponentMask"/> and is equal to the current mask; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is ComponentMask mask && Equals(mask);
    }

    /// <summary>
    /// Computes the hash code for the current mask.
    /// </summary>
    /// <returns>The hash code for the mask.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(_bits.GetElement(0), _bits.GetElement(1));
    }

    /// <summary>
    /// Determines whether two masks are equal.
    /// </summary>
    /// <param name="left">The first mask to compare.</param>
    /// <param name="right">The second mask to compare.</param>
    /// <returns><c>true</c> if the masks are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ComponentMask left, ComponentMask right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two masks are not equal.
    /// </summary>
    /// <param name="left">The first mask to compare.</param>
    /// <param name="right">The second mask to compare.</param>
    /// <returns><c>true</c> if the masks are not equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ComponentMask left, ComponentMask right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Computes the bitwise AND of two masks.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns>A new mask representing the bitwise AND of the two masks.</returns>
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

    /// <summary>
    /// Computes the bitwise OR of two masks.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns>A new mask representing the bitwise OR of the two masks.</returns>
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

    /// <summary>
    /// Computes the bitwise XOR of two masks.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns>A new mask representing the bitwise XOR of the two masks.</returns>
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

    /// <summary>
    /// Computes the bitwise NOT of a mask.
    /// </summary>
    /// <param name="value">The mask to negate.</param>
    /// <returns>A new mask representing the bitwise NOT of the mask.</returns>
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
