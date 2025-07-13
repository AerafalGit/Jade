// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Jade.Benchmarks.Components;
using Jade.Ecs;
using Jade.Ecs.Abstractions;

namespace Jade.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class StressTestBenchmarks
{
    private World _world = null!;

    [GlobalSetup]
    public void Setup()
    {
        _world = new World();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _world.Dispose();
    }

    [Benchmark]
    [Arguments(1_000_000)]
    public void BulletHellSimulation(int bulletCount)
    {
        // Simulate bullet hell game with many entities
        var bullets = new Entity[bulletCount];

        // Create bullets
        for (var i = 0; i < bulletCount; i++)
        {
            bullets[i] = _world.Spawn()
                .With(new Position(new Vector3(
                    Random.Shared.NextSingle() * 1920,
                    Random.Shared.NextSingle() * 1080,
                    0)))
                .With(new Velocity(new Vector3(
                    Random.Shared.NextSingle() * 200 - 100,
                    Random.Shared.NextSingle() * 200 - 100,
                    0)))
                .With(new Damage(1f));
        }

        // Update positions (simulate frame)
        _world.Query()
            .ForEach((ref Position pos, ref Velocity vel) =>
            {
                pos.Value += vel.Value * 0.016f;
            });

        // Cleanup
        for (var i = 0; i < bulletCount; i++)
        {
            _world.DestroyEntity(bullets[i]);
        }
    }
}
