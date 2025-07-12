// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public readonly struct Entity : IEquatable<Entity>, IComparable<Entity>
{
    public static readonly Entity Null = new(0UL);

    private readonly ulong _packed;

    public uint Id
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)(_packed & uint.MaxValue);
    }

    public uint Version
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)(_packed >> 32);
    }

    public bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _packed is not 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity(uint id, uint version)
    {
        _packed = id | ((ulong)version << 32);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity(ulong packed)
    {
        _packed = packed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Entity other)
    {
        return _packed == other._packed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Entity other)
    {
        return _packed.CompareTo(other._packed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Entity other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return (int)(_packed ^ (_packed >> 32));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"Entity({Id}:{Version})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }
}
