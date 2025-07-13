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
public class EntityLifecycleBenchmarks
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

    [Benchmark(Baseline = true)]
    [Arguments(1000)]
    [Arguments(10000)]
    [Arguments(100000)]
    public void CreateEntities(int count)
    {
        for (var i = 0; i < count; i++)
        {
            _world.CreateEntity();
        }
    }

    [Benchmark]
    [Arguments(1000)]
    [Arguments(10000)]
    public void CreateEntitiesWithComponents(int count)
    {
        for (var i = 0; i < count; i++)
        {
            _world.Spawn()
                .With(new Position(Vector3.Zero))
                .With(new Velocity(Vector3.One))
                .With(new Health(100f));
        }
    }

    [Benchmark]
    [Arguments(1000)]
    public void CreateAndDestroyEntities(int count)
    {
        var entities = new Entity[count];

        // Create
        for (var i = 0; i < count; i++)
        {
            entities[i] = _world.CreateEntity();
        }

        // Destroy
        for (var i = 0; i < count; i++)
        {
            _world.DestroyEntity(entities[i]);
        }
    }
}
