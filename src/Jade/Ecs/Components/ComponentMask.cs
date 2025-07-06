// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents a mask used to track components in an ECS (Entity Component System).
/// Provides efficient operations for adding, removing, and checking components using SIMD instructions.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 32)]
public readonly struct ComponentMask : IEquatable<ComponentMask>
{
    /// <summary>
    /// The maximum number of components that can be tracked by the mask.
    /// </summary>
    public const int MaxComponents = 256;

    /// <summary>
    /// The maximum valid component ID.
    /// </summary>
    public const int MaxComponentId = MaxComponents - 1;

    private readonly Vector256<ulong> _bits;

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentMask"/> struct with all bits set to zero.
    /// </summary>
    public ComponentMask()
    {
        _bits = Vector256<ulong>.Zero;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentMask"/> struct with the specified bits.
    /// </summary>
    /// <param name="bits">The vector containing the component bits.</param>
    private ComponentMask(Vector256<ulong> bits)
    {
        _bits = bits;
    }

    /// <summary>
    /// Returns a new mask with the specified component ID added.
    /// </summary>
    /// <param name="componentId">The ID of the component to add.</param>
    /// <returns>A new <see cref="ComponentMask"/> with the component added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask With(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(componentId, MaxComponentId);

        var wordIndex = componentId >> 6;
        var bitIndex = componentId & 63;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue | (1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    /// <summary>
    /// Returns a new mask with the specified component ID removed.
    /// </summary>
    /// <param name="componentId">The ID of the component to remove.</param>
    /// <returns>A new <see cref="ComponentMask"/> with the component removed.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentMask Without(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(componentId, MaxComponentId);

        var wordIndex = componentId >> 6;
        var bitIndex = componentId & 63;

        var oldValue = _bits.GetElement(wordIndex);
        var newValue = oldValue & ~(1UL << bitIndex);

        return new ComponentMask(_bits.WithElement(wordIndex, newValue));
    }

    /// <summary>
    /// Checks if the mask contains the specified component ID.
    /// </summary>
    /// <param name="componentId">The ID of the component to check.</param>
    /// <returns><c>true</c> if the component is present; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int componentId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(componentId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(componentId, MaxComponentId);

        var wordIndex = componentId >> 6;
        var bitIndex = componentId & 63;

        return (_bits.GetElement(wordIndex) & (1UL << bitIndex)) is not 0;
    }

    /// <summary>
    /// Checks if the mask contains all components in another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if all components are present; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasAll(in ComponentMask other)
    {
        if (!Avx2.IsSupported)
            return HasAllScalar(in other);

        var and = Avx2.And(_bits, other._bits);
        var cmp = Avx2.CompareEqual(and, other._bits);

        return (uint)Avx2.MoveMask(cmp.AsByte()) is uint.MaxValue;
    }

    /// <summary>
    /// Scalar fallback for <see cref="HasAll"/> when SIMD is not supported.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if all components are present; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool HasAllScalar(in ComponentMask other)
    {
        for (var i = 0; i < Vector256<ulong>.Count; i++)
        {
            if ((_bits.GetElement(i) & other._bits.GetElement(i)) != other._bits.GetElement(i))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the mask contains any components in another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if any components are present; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasAny(in ComponentMask other)
    {
        if (Avx2.IsSupported)
            return !Avx.TestZ(_bits, other._bits);

        return HasAnyScalar(in other);
    }

    /// <summary>
    /// Scalar fallback for <see cref="HasAny"/> when SIMD is not supported.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if any components are present; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool HasAnyScalar(in ComponentMask other)
    {
        for (var i = 0; i < Vector256<ulong>.Count; i++)
        {
            if ((_bits.GetElement(i) & other._bits.GetElement(i)) is not 0)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether the current mask is equal to another mask.
    /// </summary>
    /// <param name="other">The other mask to compare.</param>
    /// <returns><c>true</c> if the masks are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ComponentMask other)
    {
        if (!Avx2.IsSupported)
            return _bits.Equals(other._bits);

        var cmp = Avx2.CompareEqual(_bits, other._bits);

        return (uint)Avx2.MoveMask(cmp.AsByte()) is uint.MaxValue;
    }

    /// <summary>
    /// Determines whether the current mask is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><c>true</c> if the object is a <see cref="ComponentMask"/> and is equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is ComponentMask other && Equals(other);
    }

    /// <summary>
    /// Computes the hash code for the current mask.
    /// </summary>
    /// <returns>The hash code of the mask.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(_bits.GetElement(0), _bits.GetElement(1), _bits.GetElement(2), _bits.GetElement(3));
    }

    /// <summary>
    /// Determines whether two masks are equal.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns><c>true</c> if the masks are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ComponentMask left, ComponentMask right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two masks are not equal.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns><c>true</c> if the masks are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ComponentMask left, ComponentMask right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Combines two masks using a bitwise OR operation.
    /// </summary>
    /// <param name="left">The first mask.</param>
    /// <param name="right">The second mask.</param>
    /// <returns>A new <see cref="ComponentMask"/> representing the combined mask.</returns>
    public static ComponentMask operator |(ComponentMask left, ComponentMask right)
    {
        if (Avx2.IsSupported)
            return new ComponentMask(Avx2.Or(left._bits, right._bits));

        var result = new ulong[Vector256<ulong>.Count];

        for (var i = 0; i < Vector256<ulong>.Count; i++)
            result[i] = left._bits.GetElement(i) | right._bits.GetElement(i);

        return new ComponentMask(Vector256.Create(result[0], result[1], result[2], result[3]));
    }
}
