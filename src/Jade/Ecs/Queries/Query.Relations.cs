// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Relations;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    [UnscopedRef]
    public ref Query ChildOf(Entity parent)
    {
        var world = _world;
        _filters.Add(entity => world.HasRelation(entity, RelationProperty.ChildOf, parent));
        return ref this;
    }

    [UnscopedRef]
    public ref Query ParentOf(Entity child)
    {
        var world = _world;
        _filters.Add(entity => world.HasRelation(entity, RelationProperty.ParentOf, child));
        return ref this;
    }

    [UnscopedRef]
    public ref Query PrefabInstanceOf(Entity prefab)
    {
        var world = _world;
        _filters.Add(entity => world.HasRelation(entity, RelationProperty.InstanceOf, prefab));
        return ref this;
    }

    [UnscopedRef]
    public ref Query DependsOn(Entity dependency)
    {
        var world = _world;
        _filters.Add(entity => world.HasRelation(entity, RelationProperty.DependsOn, dependency));
        return ref this;
    }

    [UnscopedRef]
    public ref Query HasChildren()
    {
        var world = _world;
        _filters.Add(entity => world.HasAnyRelation(entity, RelationProperty.ChildOf));
        return ref this;
    }

    [UnscopedRef]
    public ref Query HasParent()
    {
        var world = _world;
        _filters.Add(entity => world.HasAnyRelation(entity, RelationProperty.ParentOf));
        return ref this;
    }

    [UnscopedRef]
    public ref Query HasPrefabInstance()
    {
        var world = _world;
        _filters.Add(entity => world.HasAnyRelation(entity, RelationProperty.InstanceOf));
        return ref this;
    }
}
