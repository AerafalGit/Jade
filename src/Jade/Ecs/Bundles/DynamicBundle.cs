// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions;

namespace Jade.Ecs.Bundles;

public readonly struct DynamicBundle : IBundle
{
    private readonly Action<World, Entity>[] _componentAdders;

    public DynamicBundle(Action<World, Entity>[] componentAdders)
    {
        _componentAdders = componentAdders;
    }

    public void AddToEntity(World world, Entity entity)
    {
        foreach (var componentAdder in _componentAdders)
            componentAdder(world, entity);
    }
}
