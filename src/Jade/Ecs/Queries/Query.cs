// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    private readonly World _world;

    private ComponentMask _all;
    private ComponentMask _any;
    private ComponentMask _none;

    public Query(World world)
    {
        _world = world;
        _all = new ComponentMask();
        _any = new ComponentMask();
        _none = new ComponentMask();
    }

    public readonly IEnumerator<Archetype> GetEnumerator()
    {
        return _world.GetMatchingArchetypes(in _all, in _any, in _none).GetEnumerator();
    }
}
