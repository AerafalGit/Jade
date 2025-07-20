// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a query in the ECS (Entity Component System).
/// Provides filtering capabilities using component masks.
/// </summary>
public ref partial struct Query
{
    private readonly World _world;

    private ComponentMask _all;
    private ComponentMask _any;
    private ComponentMask _none;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query"/> struct.
    /// </summary>
    /// <param name="world">The ECS world associated with this query.</param>
    public Query(World world)
    {
        _world = world;
        _all = new ComponentMask();
        _any = new ComponentMask();
        _none = new ComponentMask();
    }

    public readonly void ForEach(QueryEntityAction action)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var chunk in archetype.Chunks)
            {
                if (chunk.IsEmpty)
                    continue;

                var entities = chunk.Entities;

                for (var i = 0; i < chunk.Count; i++)
                    action(in entities[i]);
            }
        }
    }
}
