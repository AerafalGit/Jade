// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Benchmarks.Components;
using Jade.Ecs.Systems;

namespace Jade.Benchmarks.Systems;

public sealed class MovementSystem : SystemBase
{
    public override void Update()
    {
        World.Query()
            .ForEach((ref Position pos, ref Velocity vel) =>
            {
                pos.Value += vel.Value * 0.016f;
            });
    }
}
