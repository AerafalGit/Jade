// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

[StructLayout(LayoutKind.Sequential)]
public readonly struct ComponentId : IEquatable<ComponentId>, IComparable<ComponentId>
{
    public readonly int Id;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentId(int id)
    {
        Id = id;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(ComponentId id)
    {
        return id.Id;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComponentId(int value)
    {
        return new ComponentId(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ComponentId other)
    {
        return Id == other.Id;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(ComponentId other)
    {
        return Id.CompareTo(other.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is ComponentId id && Equals(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"ComponentId({Id})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ComponentId left, ComponentId right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ComponentId left, ComponentId right)
    {
        return !left.Equals(right);
    }
}
