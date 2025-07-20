// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Microsoft.Extensions.ObjectPool;

namespace Jade.Ecs.Archetypes;

/// <summary>
/// Represents a policy for pooling <see cref="ArchetypeChunk"/> objects.
/// Provides methods for creating and resetting pooled objects.
/// </summary>
public sealed class ArchetypeChunkPolicy : PooledObjectPolicy<ArchetypeChunk>
{
    private readonly int[] _componentIds;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchetypeChunkPolicy"/> class.
    /// </summary>
    /// <param name="componentIds">The IDs of the components to be used in the archetype chunk.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArchetypeChunkPolicy(int[] componentIds)
    {
        _componentIds = componentIds;
    }

    /// <summary>
    /// Creates a new <see cref="ArchetypeChunk"/> object.
    /// </summary>
    /// <returns>A new instance of <see cref="ArchetypeChunk"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ArchetypeChunk Create()
    {
        return new ArchetypeChunk(_componentIds);
    }

    /// <summary>
    /// Resets the specified <see cref="ArchetypeChunk"/> object and returns it to the pool.
    /// </summary>
    /// <param name="obj">The <see cref="ArchetypeChunk"/> object to reset and return.</param>
    /// <returns><c>true</c> if the object was successfully returned to the pool; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Return(ArchetypeChunk obj)
    {
        obj.Reset();
        return true;
    }
}
