// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Entities;

/// <summary>
/// Represents an entity in the ECS (Entity Component System).
/// An entity is defined by a unique identifier and a version number.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Entity : IEquatable<Entity>
{
    /// <summary>
    /// A null entity, represented by an ID of 0 and a version of 0.
    /// </summary>
    public static readonly Entity Null = new(0, 0);

    /// <summary>
    /// The unique identifier of the entity.
    /// </summary>
    public readonly uint Id;

    /// <summary>
    /// The version number of the entity, used for distinguishing between different generations of the same ID.
    /// </summary>
    public readonly ushort Version;

    /// <summary>
    /// Indicates whether the entity is null (i.e., has an ID of 0).
    /// </summary>
    public bool IsNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Id is 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> struct with the specified ID and version.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="version">The version number of the entity.</param>
    public Entity(uint id, ushort version)
    {
        Id = id;
        Version = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> struct with the specified ID and a default version of 0.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    public Entity(uint id)
    {
        Id = id;
        Version = 0;
    }

    /// <summary>
    /// Determines whether the current entity is equal to another entity.
    /// </summary>
    /// <param name="other">The entity to compare with.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Entity other)
    {
        return Id == other.Id && Version == other.Version;
    }

    /// <summary>
    /// Determines whether the current entity is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if the object is an <see cref="Entity"/> and is equal to the current entity; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Equals(entity);
    }

    /// <summary>
    /// Returns the hash code for the current entity.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Version);
    }

    /// <summary>
    /// Returns a string representation of the entity.
    /// </summary>
    /// <returns>A string in the format "Entity(IDvVersion)".</returns>
    public override string ToString()
    {
        return $"Entity({Id}v{Version})";
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }
}
