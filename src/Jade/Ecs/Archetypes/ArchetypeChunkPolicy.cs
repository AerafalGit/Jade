// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Components;
using Microsoft.Extensions.ObjectPool;

namespace Jade.Ecs.Archetypes;

public sealed class ArchetypeChunkPolicy : PooledObjectPolicy<ArchetypeChunk>
{
    private readonly ComponentId[] _ids;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArchetypeChunkPolicy(ComponentId[] ids)
    {
        _ids = ids;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ArchetypeChunk Create()
    {
        return new ArchetypeChunk(_ids);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Return(ArchetypeChunk obj)
    {
        obj.Reset();
        return true;
    }
}
