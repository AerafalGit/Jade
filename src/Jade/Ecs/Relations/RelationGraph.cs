// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Components;

namespace Jade.Ecs.Relations;

internal sealed class RelationGraph : IDisposable
{
    private readonly Dictionary<Entity, List<Relation>> _outgoingRelations;
    private readonly Dictionary<Relation, List<Entity>> _incomingRelations;

    public RelationGraph()
    {
        _outgoingRelations = [];
        _incomingRelations = [];
    }

    ~RelationGraph()
    {
        ReleaseUnmanagedResources();
    }

    public void AddRelation(Entity source, ComponentId relationType, Entity target)
    {
        var relation = new Relation(relationType, target);

        if (!_outgoingRelations.TryGetValue(source, out var outgoing))
            _outgoingRelations[source] = outgoing = [];

        if (!outgoing.Contains(relation))
            outgoing.Add(relation);

        if (!_incomingRelations.TryGetValue(relation, out var incoming))
            _incomingRelations[relation] = incoming = [];

        if (!incoming.Contains(source))
            incoming.Add(source);
    }

    public bool RemoveRelation(Entity source, ComponentId relationType, Entity target)
    {
        var relation = new Relation(relationType, target);
        var removed = false;

        if (_outgoingRelations.TryGetValue(source, out var outgoing))
        {
            removed = outgoing.Remove(relation);

            if (outgoing.Count is 0)
                _outgoingRelations.Remove(source);
        }

        if (_incomingRelations.TryGetValue(relation, out var incoming))
        {
            incoming.Remove(source);

            if (incoming.Count is 0)
                _incomingRelations.Remove(relation);
        }

        return removed;
    }

    public void RemoveAllRelations(Entity entity)
    {
        if (_outgoingRelations.Remove(entity, out var outgoing))
        {
            foreach (var relation in outgoing)
            {
                if (_incomingRelations.TryGetValue(relation, out var incoming))
                {
                    incoming.Remove(entity);

                    if (incoming.Count is 0)
                        _incomingRelations.Remove(relation);
                }
            }
        }

        var keysToRemove = new List<Relation>();

        foreach (var (relation, sources) in _incomingRelations)
        {
            if (relation.Target == entity)
            {
                foreach (var source in sources)
                {
                    if (_outgoingRelations.TryGetValue(source, out var sourceOutgoing))
                    {
                        sourceOutgoing.RemoveAll(r => r.RelationId == relation.RelationId && r.Target == entity);

                        if (sourceOutgoing.Count is 0)
                            _outgoingRelations.Remove(source);
                    }
                }
                keysToRemove.Add(relation);
            }
        }

        foreach (var key in keysToRemove)
            _incomingRelations.Remove(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<Relation> GetOutgoingRelations(Entity source)
    {
        return _outgoingRelations.TryGetValue(source, out var relations)
            ? CollectionsMarshal.AsSpan(relations)
            : ReadOnlySpan<Relation>.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<Entity> GetIncomingRelations(ComponentId relationType, Entity target)
    {
        return _incomingRelations.TryGetValue(new Relation(relationType, target), out var sources)
            ? CollectionsMarshal.AsSpan(sources)
            : ReadOnlySpan<Entity>.Empty;
    }

    public ReadOnlySpan<Entity> GetTargets(Entity source, ComponentId relationType)
    {
        if (!_outgoingRelations.TryGetValue(source, out var relations))
            return [];

        var targets = new List<Entity>();

        foreach (var relation in relations)
        {
            if (relation.RelationId == relationType)
                targets.Add(relation.Target);
        }

        return CollectionsMarshal.AsSpan(targets);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasRelation(Entity source, ComponentId relationType, Entity target)
    {
        if (!_outgoingRelations.TryGetValue(source, out var relations))
            return false;

        return relations.Contains(new Relation(relationType, target));
    }

    public Entity GetFirstTarget(Entity source, ComponentId relationType)
    {
        if (!_outgoingRelations.TryGetValue(source, out var relations))
            return Entity.Null;

        foreach (var relation in relations)
        {
            if (relation.RelationId == relationType)
                return relation.Target;
        }

        return Entity.Null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetRelationCount()
    {
        return _outgoingRelations.Values.Sum(static list => list.Count);
    }

    private void ReleaseUnmanagedResources()
    {
        _outgoingRelations.Clear();
        _incomingRelations.Clear();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
