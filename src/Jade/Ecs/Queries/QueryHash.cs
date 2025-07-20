// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a hash for an ECS (Entity Component System) query.
/// Encapsulates component masks for filtering entities and provides equality comparison.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct QueryHash : IEquatable<QueryHash>
{
    private readonly ComponentMask _all;
    private readonly ComponentMask _any;
    private readonly ComponentMask _none;
    private readonly int _hash;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryHash"/> struct.
    /// </summary>
    /// <param name="all">The component mask for required components.</param>
    /// <param name="any">The component mask for optional components.</param>
    /// <param name="none">The component mask for excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryHash(in ComponentMask all, in ComponentMask any, in ComponentMask none)
    {
        _all = all;
        _any = any;
        _none = none;
        _hash = HashCode.Combine(all, any, none);
    }

    /// <summary>
    /// Determines whether the current <see cref="QueryHash"/> is equal to another <see cref="QueryHash"/>.
    /// </summary>
    /// <param name="other">The other <see cref="QueryHash"/> to compare.</param>
    /// <returns><c>true</c> if the hashes are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(QueryHash other)
    {
        return _all == other._all &&
               _any == other._any &&
               _none == other._none;
    }

    /// <summary>
    /// Determines whether the current <see cref="QueryHash"/> is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><c>true</c> if the object is a <see cref="QueryHash"/> and is equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is QueryHash other && Equals(other);
    }

    /// <summary>
    /// Returns the hash code for the current <see cref="QueryHash"/>.
    /// </summary>
    /// <returns>The hash code.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return _hash;
    }

    /// <summary>
    /// Determines whether two <see cref="QueryHash"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="QueryHash"/>.</param>
    /// <param name="right">The second <see cref="QueryHash"/>.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(QueryHash left, QueryHash right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="QueryHash"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="QueryHash"/>.</param>
    /// <param name="right">The second <see cref="QueryHash"/>.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(QueryHash left, QueryHash right)
    {
        return !left.Equals(right);
    }
}
