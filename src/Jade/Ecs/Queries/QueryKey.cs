// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct QueryKey : IEquatable<QueryKey>
{
    public readonly ComponentMask All;
    public readonly ComponentMask Any;
    public readonly ComponentMask None;

    public QueryKey(ComponentMask all, ComponentMask any, ComponentMask none)
    {
        All = all;
        Any = any;
        None = none;
    }

    public bool Equals(QueryKey other)
    {
        return All.Equals(other.All) && Any.Equals(other.Any) && None.Equals(other.None);
    }

    public override bool Equals(object? obj)
    {
        return obj is QueryKey other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(All, Any, None);
    }

    public static bool operator ==(QueryKey left, QueryKey right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(QueryKey left, QueryKey right)
    {
        return !left.Equals(right);
    }
}
