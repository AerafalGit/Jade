// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Benchmarks.Components;
using Jade.Ecs.Systems;

namespace Jade.Benchmarks.Systems;

public sealed class HealthSystem : SystemBase
{
    public override void Update()
    {
        World.Query()
            .ForEach((ref Health health) =>
            {
                health.Current = Math.Min(health.Max, health.Current + 0.1f);
            });
    }
}
