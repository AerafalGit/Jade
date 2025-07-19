// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs;

/// <summary>
/// Represents an entity in the ECS (Entity Component System).
/// An entity is uniquely identified by an ID and a version.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public readonly partial struct Entity : IEquatable<Entity>, IComparable<Entity>
{
    /// <summary>
    /// A null entity instance, representing an invalid or uninitialized entity.
    /// </summary>
    internal static readonly Entity Null = new(0UL);

    private readonly ulong _packed;

    /// <summary>
    /// Gets the unique ID of the entity.
    /// </summary>
    public uint Id
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)(_packed & uint.MaxValue);
    }

    /// <summary>
    /// Gets the version of the entity, used to differentiate between entities
    /// with the same ID that have been reused.
    /// </summary>
    public uint Version
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)(_packed >> 32);
    }

    /// <summary>
    /// Indicates whether the entity is valid (not null).
    /// </summary>
    internal bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _packed is not 0UL;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> struct with the specified ID and version.
    /// </summary>
    /// <param name="id">The unique ID of the entity.</param>
    /// <param name="version">The version of the entity.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity(uint id, uint version)
    {
        _packed = id | ((ulong)version << 32);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> struct with the specified packed value.
    /// </summary>
    /// <param name="packed">The packed value containing both ID and version.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity(ulong packed)
    {
        _packed = packed;
    }

    /// <summary>
    /// Determines whether the current entity is equal to another entity.
    /// </summary>
    /// <param name="other">The other entity to compare.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Entity other)
    {
        return _packed == other._packed;
    }

    /// <summary>
    /// Compares the current entity to another entity.
    /// </summary>
    /// <param name="other">The other entity to compare.</param>
    /// <returns>
    /// A value less than zero if this entity is less than the other entity;
    /// zero if they are equal; greater than zero if this entity is greater.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Entity other)
    {
        return _packed.CompareTo(other._packed);
    }

    /// <summary>
    /// Determines whether the current entity is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><c>true</c> if the object is an <see cref="Entity"/> and is equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Entity other && Equals(other);
    }

    /// <summary>
    /// Gets the hash code for the current entity.
    /// </summary>
    /// <returns>The hash code of the entity.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return (int)(_packed ^ (_packed >> 32));
    }

    /// <summary>
    /// Returns a string representation of the entity.
    /// </summary>
    /// <returns>A string in the format "Entity(ID:Version)".</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"Entity({Id}:{Version})";
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity.</param>
    /// <param name="right">The second entity.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    /// <param name="left">The first entity.</param>
    /// <param name="right">The second entity.</param>
    /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }
}
