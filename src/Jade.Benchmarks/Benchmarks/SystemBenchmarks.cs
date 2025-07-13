// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Jade.Benchmarks.Components;
using Jade.Benchmarks.Systems;
using Jade.Ecs;
using Jade.Ecs.Systems;

namespace Jade.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class SystemBenchmarks
{
    private World _world = null!;

    [Params(10000)]
    public int EntityCount;

    [GlobalSetup]
    public void Setup()
    {
        _world = new World();

        _world.AddSystem<MovementSystem>(SystemStage.Update);
        _world.AddSystem<HealthSystem>(SystemStage.Update);

        for (var i = 0; i < EntityCount; i++)
        {
            _world.Spawn()
                .With(new Position(Vector3.Zero))
                .With(new Velocity(Vector3.One))
                .With(new Health(100f));
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _world.Dispose();
    }

    [Benchmark(Baseline = true)]
    public void RunSingleSystem()
    {
        _world.RunStage(SystemStage.Update);
    }

    [Benchmark]
    public void RunMultipleSystems()
    {
        _world.RunStage(SystemStage.PreUpdate);
        _world.RunStage(SystemStage.Update);
        _world.RunStage(SystemStage.PostUpdate);
    }
}
