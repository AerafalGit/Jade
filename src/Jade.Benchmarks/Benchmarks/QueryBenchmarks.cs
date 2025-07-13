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
[SimpleJob(RuntimeMoniker.Net80)]
public class QueryBenchmarks
{
    private World _world = null!;

    [Params(1000, 10000, 100000)]
    public int EntityCount;

    [GlobalSetup]
    public void Setup()
    {
        _world = new World();

        // Create entities with different component combinations
        for (var i = 0; i < EntityCount; i++)
        {
            var entity = _world.CreateEntity();

            // All entities have Position
            _world.AddComponent(entity, new Position(new Vector3(i, i, i)));

            // 80% have Velocity
            if (i % 5 != 0)
                _world.AddComponent(entity, new Velocity(Vector3.One));

            // 50% have Health
            if (i % 2 == 0)
                _world.AddComponent(entity, new Health(100f));

            // 20% have Transform (large component)
            if (i % 5 == 0)
                _world.AddComponent(entity, new Transform(Matrix4x4.Identity));
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _world.Dispose();
    }

    [Benchmark(Baseline = true)]
    public void QuerySingleComponent()
    {
        _world.Query()
              .ForEach((ref Position pos) =>
        {
            pos.Value += Vector3.One;
        });
    }

    [Benchmark]
    public void QueryTwoComponents()
    {
        _world.Query()
              .ForEach((ref Position pos, ref Velocity vel) =>
        {
            pos.Value += vel.Value;
        });
    }

    [Benchmark]
    public void QueryThreeComponents()
    {
        _world.Query()
              .ForEach((ref Position pos, ref Velocity vel, ref Health health) =>
        {
            pos.Value += vel.Value;
            health.Current = Math.Max(0, health.Current - 1);
        });
    }

    [Benchmark]
    public void QueryWithEntityAccess()
    {
        _world.Query()
              .ForEach((in Entity _, ref Position pos) =>
        {
            pos.Value *= 1.01f;
        });
    }

    [Benchmark]
    public void QueryLargeComponent()
    {
        _world.Query()
              .ForEach((ref Transform transform) =>
        {
            transform.Matrix = Matrix4x4.Multiply(transform.Matrix, Matrix4x4.CreateScale(1.001f));
        });
    }
}
