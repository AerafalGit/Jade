// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;
using Jade.Ecs.Entities;
using Jade.Ecs.Events;
using Jade.Ecs.Relations;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world, providing methods for managing entities,
/// components, archetypes, and relations. Handles entity lifecycle operations and resource management.
/// </summary>
public sealed partial class World : IDisposable
{
    private readonly Dictionary<ComponentMask, Archetype> _archetypes;
    private readonly Dictionary<uint, EntityLocation> _entityIndex;
    private readonly Dictionary<Type, object> _resources;
    private readonly Queue<uint> _freeIds;
    private readonly List<ushort> _versions;
    private readonly RelationGraph _relationGraph;
    private readonly EventBus _eventBus;

    private bool _isDisposed;
    private uint _nextEntityId = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="World"/> class.
    /// </summary>
    public World()
    {
        _archetypes = [];
        _entityIndex = [];
        _resources = [];
        _freeIds = [];
        _versions = [0];
        _relationGraph = new RelationGraph();
        _eventBus = new EventBus();

        CreateArchetypeFor(new ComponentMask());
    }

    /// <summary>
    /// Finalizes the <see cref="World"/> instance, ensuring proper disposal of resources.
    /// </summary>
    ~World()
    {
        Dispose();
    }

    /// <summary>
    /// Retrieves a list of archetypes that match the specified component masks.
    /// </summary>
    /// <param name="withMask">The mask specifying required components.</param>
    /// <param name="withoutMask">The mask specifying excluded components.</param>
    /// <returns>A list of matching archetypes.</returns>
    internal List<Archetype> GetMatchingArchetypes(in ComponentMask withMask, in ComponentMask withoutMask)
    {
        var matching = new List<Archetype>();

        foreach (var archetype in _archetypes.Values)
        {
            if (archetype.Mask.HasAll(withMask) && !archetype.Mask.HasAny(withoutMask))
                matching.Add(archetype);
        }

        return matching;
    }

    /// <summary>
    /// Creates a new archetype for the specified component mask.
    /// </summary>
    /// <param name="mask">The component mask defining the archetype.</param>
    /// <returns>The newly created archetype.</returns>
    private Archetype CreateArchetypeFor(ComponentMask mask)
    {
        var componentTypes = new List<ComponentType>();

        for (var i = 0; i < ComponentMask.MaxComponents; i++)
        {
            if (mask.Has(i))
                componentTypes.Add(new ComponentType(ComponentRegistry.GetMetadata(i), componentTypes.Count));
        }

        var archetype = new Archetype(mask, CollectionsMarshal.AsSpan(componentTypes));
        _archetypes[mask] = archetype;
        return archetype;
    }

    /// <summary>
    /// Moves an entity from one archetype to another.
    /// </summary>
    /// <param name="entity">The entity to move.</param>
    /// <param name="oldLocation">The current location of the entity.</param>
    /// <param name="newArchetype">The target archetype.</param>
    /// <returns>The new location of the entity.</returns>
    private EntityLocation MoveEntity(Entity entity, EntityLocation oldLocation, Archetype newArchetype)
    {
        var newLocation = newArchetype.AddEntity(entity);

        var oldChunk = oldLocation.Archetype.GetChunk(oldLocation.ChunkIndex);
        var newChunk = newLocation.Archetype.GetChunk(newLocation.ChunkIndex);

        foreach (var componentType in oldLocation.Archetype.ComponentTypes)
        {
            var newComponentIndex = newArchetype.GetComponentIndex(componentType.Id);

            if (newComponentIndex is not -1)
            {
                unsafe
                {
                    var sourcePtr = (byte*)oldChunk.GetComponentPtr(componentType.IndexInArchetype);
                    var destPtr = (byte*)newChunk.GetComponentPtr(newComponentIndex);

                    var sourceComponentPtr = sourcePtr + oldLocation.IndexInChunk * componentType.Size;
                    var destComponentPtr = destPtr + newLocation.IndexInChunk * componentType.Size;

                    Unsafe.CopyBlock(destComponentPtr, sourceComponentPtr, (uint)componentType.Size);
                }
            }
        }

        var movedEntityInfo = oldLocation.Archetype.RemoveEntity(oldLocation);

        if (movedEntityInfo.HasValue)
            _entityIndex[movedEntityInfo.Value.movedEntity.Id] = movedEntityInfo.Value.newLocation;

        return newLocation;
    }

    /// <summary>
    /// Disposes the ECS world, releasing all resources and clearing data structures.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        foreach (var archetype in _archetypes.Values)
            archetype.Dispose();

        foreach (var resource in _resources.Values)
        {
            if (resource is IDisposable disposable)
                disposable.Dispose();
        }

        _relationGraph.Dispose();
        _eventBus.Dispose();

        _archetypes.Clear();
        _entityIndex.Clear();
        _versions.Clear();
        _freeIds.Clear();

        GC.SuppressFinalize(this);
    }
}
