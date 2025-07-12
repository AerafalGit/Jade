// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Components;
using Jade.Ecs.Relations;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity SetParent(in Entity child, in Entity parent)
    {
        RelationGraph.AddRelation(child, RelationProperty.ChildOf, parent);
        RelationGraph.AddRelation(parent, RelationProperty.ParentOf, child);
        return child;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveParent(in Entity child)
    {
        return IsAlive(child) &&
               GetParent(child) is var parent &&
               IsAlive(parent) &&
               RemoveRelation(child, RelationProperty.ChildOf, parent) &&
               RemoveRelation(parent, RelationProperty.ParentOf, child);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetParent(in Entity child)
    {
        return GetFirstTarget(child, RelationProperty.ChildOf);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity AddChild(in Entity parent, in Entity child)
    {
        AddRelation(parent, RelationProperty.ParentOf, child);
        AddRelation(child, RelationProperty.ChildOf, parent);
        return child;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveChild(in Entity parent, in Entity child)
    {
        return IsAlive(parent) && IsAlive(child) &&
               RemoveRelation(parent, RelationProperty.ParentOf, child) &&
               RemoveRelation(child, RelationProperty.ChildOf, parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<Entity> GetChildren(in Entity parent)
    {
        return GetSources(RelationProperty.ChildOf, parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasChildren(in Entity parent)
    {
        return GetChildren(parent).Length > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateChildOf(in Entity parent)
    {
        return SetParent(CreateEntity(), parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity AddRelation(in Entity source, in ComponentId relationType, in Entity target)
    {
        if (!IsAlive(source) || !IsAlive(target))
            throw new ArgumentException("Both source and target must be alive entities.");

        RelationGraph.AddRelation(source, relationType, target);
        return source;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DestroyWithChildren(in Entity parent)
    {
        var children = GetChildren(parent);

        foreach (var child in children)
            DestroyWithChildren(child);

        DestroyEntity(parent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool RemoveRelation(in Entity source, in ComponentId relationType, in Entity target)
    {
        if (!IsAlive(source) || !IsAlive(target))
            return false;

        return RelationGraph.RemoveRelation(source, relationType, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool HasRelation(in Entity source, in ComponentId relationType, in Entity target)
    {
        if (!IsAlive(source) || !IsAlive(target))
            return false;

        return RelationGraph.HasRelation(source, relationType, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Entity> GetTargets(in Entity source, in ComponentId relationType)
    {
        if (!IsAlive(source))
            throw new ArgumentException("Source entity must be alive.");

        return RelationGraph.GetTargets(source, relationType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Relation> GetOutgoingRelations(in Entity source)
    {
        if (!IsAlive(source))
            throw new ArgumentException("Source entity must be alive.");

        return RelationGraph.GetOutgoingRelations(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Entity> GetSources(in ComponentId relationType, in Entity target)
    {
        if (!IsAlive(target))
            throw new ArgumentException("Target entity must be alive.");

        return RelationGraph.GetIncomingRelations(relationType, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity GetFirstTarget(in Entity source, in ComponentId relationType)
    {
        if (!IsAlive(source))
            throw new ArgumentException("Source entity must be alive.");

        return RelationGraph.GetFirstTarget(source, relationType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AddComponentFromBytes(in Entity entity, in ComponentId componentId, in ReadOnlySpan<byte> data)
    {
        if (!IsAlive(entity))
            throw new ArgumentException("Entity must be alive.");

        AddComponentDataToArchetype(entity, componentId, data);
    }
}
