// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs;

public sealed partial class World
{
    public void Set(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.HasAll(in mask))
            return;

        var newMask = currentMask | mask;
        var newArchetype = GetOrCreateArchetype(newMask);

        MoveEntityToArchetype(in entity, in currentLocation, newArchetype);
    }

    public void Set<T1>(
        in Entity entity,
        in T1? component = default)
    {
        Debug.Assert(component is not null);

        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var componentType = Component<T1>.Metadata.Id;

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.Has(componentType))
            currentLocation.Archetype.Chunks[currentLocation.ChunkIndex].GetArray<T1>().SetRef(currentLocation.IndexInChunk, component);
        else
        {
            var newMask = currentMask.With(componentType);
            var targetArchetype = GetOrCreateArchetype(newMask);
            var newLocation = MoveEntityToArchetype(entity, currentLocation, targetArchetype);

            newLocation.Archetype.Chunks[newLocation.ChunkIndex].GetArray<T1>().SetRef(newLocation.IndexInChunk, component);
        }
    }

    public void Set<T1, T2>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
    }

    public void Set<T1, T2, T3>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
    }

    public void Set<T1, T2, T3, T4>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
    }

    public void Set<T1, T2, T3, T4, T5>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
    }

    public void Set<T1, T2, T3, T4, T5, T6>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);
        Debug.Assert(component6 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
        archetypeChunk.GetArray<T6>().SetRef(indexInChunk, component6);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);
        Debug.Assert(component6 is not null);
        Debug.Assert(component7 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
        archetypeChunk.GetArray<T6>().SetRef(indexInChunk, component6);
        archetypeChunk.GetArray<T7>().SetRef(indexInChunk, component7);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);
        Debug.Assert(component6 is not null);
        Debug.Assert(component7 is not null);
        Debug.Assert(component8 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
        archetypeChunk.GetArray<T6>().SetRef(indexInChunk, component6);
        archetypeChunk.GetArray<T7>().SetRef(indexInChunk, component7);
        archetypeChunk.GetArray<T8>().SetRef(indexInChunk, component8);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);
        Debug.Assert(component6 is not null);
        Debug.Assert(component7 is not null);
        Debug.Assert(component8 is not null);
        Debug.Assert(component9 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
        archetypeChunk.GetArray<T6>().SetRef(indexInChunk, component6);
        archetypeChunk.GetArray<T7>().SetRef(indexInChunk, component7);
        archetypeChunk.GetArray<T8>().SetRef(indexInChunk, component8);
        archetypeChunk.GetArray<T9>().SetRef(indexInChunk, component9);
    }

    public void Set<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        in Entity entity,
        in T1? component1 = default,
        in T2? component2 = default,
        in T3? component3 = default,
        in T4? component4 = default,
        in T5? component5 = default,
        in T6? component6 = default,
        in T7? component7 = default,
        in T8? component8 = default,
        in T9? component9 = default,
        in T10? component10 = default)
    {
        Debug.Assert(component1 is not null);
        Debug.Assert(component2 is not null);
        Debug.Assert(component3 is not null);
        Debug.Assert(component4 is not null);
        Debug.Assert(component5 is not null);
        Debug.Assert(component6 is not null);
        Debug.Assert(component7 is not null);
        Debug.Assert(component8 is not null);
        Debug.Assert(component9 is not null);
        Debug.Assert(component10 is not null);

        var (archetypeChunk, indexInChunk) = SetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>());

        archetypeChunk.GetArray<T1>().SetRef(indexInChunk, component1);
        archetypeChunk.GetArray<T2>().SetRef(indexInChunk, component2);
        archetypeChunk.GetArray<T3>().SetRef(indexInChunk, component3);
        archetypeChunk.GetArray<T4>().SetRef(indexInChunk, component4);
        archetypeChunk.GetArray<T5>().SetRef(indexInChunk, component5);
        archetypeChunk.GetArray<T6>().SetRef(indexInChunk, component6);
        archetypeChunk.GetArray<T7>().SetRef(indexInChunk, component7);
        archetypeChunk.GetArray<T8>().SetRef(indexInChunk, component8);
        archetypeChunk.GetArray<T9>().SetRef(indexInChunk, component9);
        archetypeChunk.GetArray<T10>().SetRef(indexInChunk, component10);
    }

    public void Remove(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var currentMask = currentLocation.Archetype.Mask;

        if (!currentMask.HasAny(in mask))
            return;

        var newMask = currentMask & ~mask;
        var newArchetype = GetOrCreateArchetype(newMask);

        MoveEntityToArchetype(in entity, in currentLocation, newArchetype);
    }

    public void Remove<T1>(in Entity entity)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var componentType = Component<T1>.Metadata.Id;

        var currentMask = currentLocation.Archetype.Mask;

        if (!currentMask.Has(in componentType))
            return;

        var newMask = currentMask.Without(componentType);
        var newArchetype = GetOrCreateArchetype(newMask);

        MoveEntityToArchetype(entity, currentLocation, newArchetype);
    }

    public void Remove<T1, T2>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2>());
    }

    public void Remove<T1, T2, T3>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3>());
    }

    public void Remove<T1, T2, T3, T4>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4>());
    }

    public void Remove<T1, T2, T3, T4, T5>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5>());
    }

    public void Remove<T1, T2, T3, T4, T5, T6>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6>());
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7>());
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8>());
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>());
    }

    public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Entity entity)
    {
        Remove(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>());
    }

    public bool Has(in Entity entity, in ComponentMask mask)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(in mask);
    }

    public bool Has<T1>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.Has(Component<T1>.Metadata.Id);
    }

    public bool Has<T1, T2>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2>());
    }

    public bool Has<T1, T2, T3>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3>());
    }

    public bool Has<T1, T2, T3, T4>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4>());
    }

    public bool Has<T1, T2, T3, T4, T5>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5>());
    }

    public bool Has<T1, T2, T3, T4, T5, T6>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5, T6>());
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7>());
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8>());
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>());
    }

    public bool Has<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Entity entity)
    {
        return IsAlive(entity) && _locations.TryGetValue(entity.Id, out var location) && location.Archetype.Mask.HasAll(ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>());
    }

    public ref T1 Get<T1>(in Entity entity)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var location);

        var componentId = Component<T1>.Metadata.Id;

        if (!location.Archetype.Mask.Has(componentId))
            throw new InvalidOperationException($"Entity {entity} does not have component {typeof(T1).Name}.");

        return ref location.Archetype.Chunks[location.ChunkIndex].GetArray<T1>().GetRef(location.IndexInChunk);
    }

    public Components<T1, T2> Get<T1, T2>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2>());

        return new Components<T1, T2>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3> Get<T1, T2, T3>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3>());

        return new Components<T1, T2, T3>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4> Get<T1, T2, T3, T4>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4>());

        return new Components<T1, T2, T3, T4>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5> Get<T1, T2, T3, T4, T5>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5>());

        return new Components<T1, T2, T3, T4, T5>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6> Get<T1, T2, T3, T4, T5, T6>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6>());

        return new Components<T1, T2, T3, T4, T5, T6>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7> Get<T1, T2, T3, T4, T5, T6, T7>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7>());

        return new Components<T1, T2, T3, T4, T5, T6, T7>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8> Get<T1, T2, T3, T4, T5, T6, T7, T8>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8>());

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>());

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T9>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Entity entity)
    {
        var (archetypeChunk, indexInChunk) = GetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>());

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T9>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T10>().GetRef(indexInChunk));
    }

    public ref T1 TryGet<T1>(in Entity entity, out bool exists)
    {
        if (!IsAlive(entity) || !_locations.TryGetValue(entity.Id, out var location) || !location.Archetype.Mask.Has(Component<T1>.Metadata.Id))
        {
            exists = false;
            return ref Unsafe.NullRef<T1>();
        }

        exists = true;
        return ref location.Archetype.Chunks[location.ChunkIndex].GetArray<T1>().GetRef(location.IndexInChunk);
    }

    public Components<T1, T2> TryGet<T1, T2>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>());
        }

        exists = true;

        return new Components<T1, T2>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3> TryGet<T1, T2, T3>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>());
        }

        exists = true;

        return new Components<T1, T2, T3>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4> TryGet<T1, T2, T3, T4>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5> TryGet<T1, T2, T3, T4, T5>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6> TryGet<T1, T2, T3, T4, T5, T6>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5, T6>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>(), ref Unsafe.NullRef<T6>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5, T6>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7> TryGet<T1, T2, T3, T4, T5, T6, T7>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5, T6, T7>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>(), ref Unsafe.NullRef<T6>(), ref Unsafe.NullRef<T7>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5, T6, T7>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8> TryGet<T1, T2, T3, T4, T5, T6, T7, T8>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5, T6, T7, T8>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>(), ref Unsafe.NullRef<T6>(), ref Unsafe.NullRef<T7>(), ref Unsafe.NullRef<T8>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9> TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>(), ref Unsafe.NullRef<T6>(), ref Unsafe.NullRef<T7>(), ref Unsafe.NullRef<T8>(), ref Unsafe.NullRef<T9>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T9>().GetRef(indexInChunk));
    }

    public Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> TryGet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Entity entity, out bool exists)
    {
        if (!TryGetMultiple(in entity, ComponentMask.Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(), out var archetypeChunk, out var indexInChunk))
        {
            exists = false;
            return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref Unsafe.NullRef<T1>(), ref Unsafe.NullRef<T2>(), ref Unsafe.NullRef<T3>(), ref Unsafe.NullRef<T4>(), ref Unsafe.NullRef<T5>(), ref Unsafe.NullRef<T6>(), ref Unsafe.NullRef<T7>(), ref Unsafe.NullRef<T8>(), ref Unsafe.NullRef<T9>(), ref Unsafe.NullRef<T10>());
        }

        exists = true;

        return new Components<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            ref archetypeChunk.GetArray<T1>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T2>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T3>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T4>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T5>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T6>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T7>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T8>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T9>().GetRef(indexInChunk),
            ref archetypeChunk.GetArray<T10>().GetRef(indexInChunk));
    }

    private bool TryGetMultiple(in Entity entity, in ComponentMask mask, [NotNullWhen(true)] out ArchetypeChunk? archetypeChunk, out int indexInChunk)
    {
        archetypeChunk = null;
        indexInChunk = -1;

        if (!IsAlive(entity) || !_locations.TryGetValue(entity.Id, out var location) || !location.Archetype.Mask.HasAll(in mask))
            return false;

        archetypeChunk = location.Archetype.Chunks[location.ChunkIndex];
        indexInChunk = location.IndexInChunk;
        return true;
    }

    private (ArchetypeChunk archetypeChunk, int indexInChunk) GetMultiple(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var location);

        if (!location.Archetype.Mask.HasAll(in mask))
            throw new InvalidOperationException($"Entity {entity} does not have components {mask}.");

        return (location.Archetype.Chunks[location.ChunkIndex], location.IndexInChunk);
    }

    private (ArchetypeChunk archetypeChunk, int indexInChunk) SetMultiple(in Entity entity, in ComponentMask mask)
    {
        ValidateEntity(in entity);
        ValidateEntityLocation(in entity, out var currentLocation);

        var currentMask = currentLocation.Archetype.Mask;

        if (currentMask.HasAll(in mask))
            return (currentLocation.Archetype.Chunks[currentLocation.ChunkIndex], currentLocation.IndexInChunk);

        var newMask = currentMask | mask;
        var targetArchetype = GetOrCreateArchetype(newMask);
        var newLocation = MoveEntityToArchetype(entity, currentLocation, targetArchetype);

        return (newLocation.Archetype.Chunks[newLocation.ChunkIndex], newLocation.IndexInChunk);
    }
}
