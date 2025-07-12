// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions;

namespace Jade.Ecs.Bundles;

public interface IBundle
{
    void AddToEntity(World world, Entity entity);
}
