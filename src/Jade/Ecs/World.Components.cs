// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Archives;
using Jade.Ecs.Components;

namespace Jade.Ecs;

public sealed partial class World
{
    public void AddComponent<T>(in Entity entity, in T component = default)
        where T : unmanaged, IComponent
    {
        if (!IsAlive(entity))
            return;

        var componentId = Component<T>.Metadata.Id;
        var strategy = Archive.GetStrategy(componentId);

        if (strategy is ArchiveType.SparseSet)
        {
            var sparseSet = Archive.GetSparseSet(componentId)!;
            sparseSet.Add(entity, component);

            if (!_locations[entity.Id].Archetype.Mask.Has(componentId))
            {
                var newMask = _locations[entity.Id].Archetype.Mask.With(componentId);
                var targetArchetype = Archive.GetOrCreateArchetype(newMask);

                if (targetArchetype != _locations[entity.Id].Archetype)
                {
                    var oldLocation = _locations[entity.Id];
                    MoveEntityToNewArchetype(entity, oldLocation, targetArchetype);
                }
            }
        }
        else
            Transition(entity, archetype => archetype.AddTransitions.GetValueOrDefault(componentId), in component);
    }

    public void RemoveComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        if (!IsAlive(entity))
            return;

        var componentId = Component<T>.Metadata.Id;

        if (Archive.GetStrategy(componentId) is ArchiveType.SparseSet)
        {
            Archive.GetSparseSet(componentId)?.Remove(entity);

            if (_locations[entity.Id].Archetype.Mask.Has(componentId))
                Transition(entity, componentId, archetype => archetype.RemoveTransitions.GetValueOrDefault(componentId));
        }
        else
            Transition(entity, componentId, archetype => archetype.RemoveTransitions.GetValueOrDefault(componentId));
    }

    public ref T GetComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        if (!IsAlive(entity))
            throw new Exception($"Entity {entity} is not alive or does not exist in the world.");

        var componentId = Component<T>.Metadata.Id;

        if (Archive.GetStrategy(componentId) is ArchiveType.SparseSet)
            return ref Archive.GetSparseSet(componentId)!.Get<T>(entity);

        if (!_locations.TryGetValue(entity.Id, out var location))
            throw new Exception($"Entity {entity} does not have a valid location in the world.");

        return ref location.Archetype.Chunks[location.ChunkIndex].GetArray<T>().Get(location.IndexInChunk);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasComponent<T>(Entity entity)
        where T : unmanaged, IComponent
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.Has(Component<T>.Metadata.Id);
    }
}
