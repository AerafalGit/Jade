// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct QueryHash : IEquatable<QueryHash>
{
    private readonly ComponentMask _all;
    private readonly ComponentMask _any;
    private readonly ComponentMask _none;
    private readonly int _hash;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryHash(in ComponentMask all, in ComponentMask any, in ComponentMask none)
    {
        _all = all;
        _any = any;
        _none = none;
        _hash = HashCode.Combine(all, any, none, 2654435769);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(QueryHash other)
    {
        return _all == other._all &&
               _any == other._any &&
               _none == other._none;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is QueryHash other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return _hash;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(QueryHash left, QueryHash right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(QueryHash left, QueryHash right)
    {
        return !left.Equals(right);
    }
}
