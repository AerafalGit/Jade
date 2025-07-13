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
public sealed class ComponentBenchmarks
{
    private World _world = null!;
    private Entity[] _entities = null!;

    [Params(1000, 10000)]
    public int EntityCount;

    [GlobalSetup]
    public void Setup()
    {
        _world = new World();
        _entities = new Entity[EntityCount];

        for (int i = 0; i < EntityCount; i++)
        {
            _entities[i] = _world.CreateEntity();
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _world.Dispose();
    }

    [Benchmark(Baseline = true)]
    public void AddComponents()
    {
        for (int i = 0; i < EntityCount; i++)
        {
            _world.AddComponent(_entities[i], new Position(Vector3.Zero));
        }
    }

    [Benchmark]
    public void AddMultipleComponents()
    {
        for (int i = 0; i < EntityCount; i++)
        {
            _world.AddComponent(_entities[i], new Position(Vector3.Zero));
            _world.AddComponent(_entities[i], new Velocity(Vector3.One));
            _world.AddComponent(_entities[i], new Health(100f));
        }
    }

    [Benchmark]
    public void RemoveComponents()
    {
        // Setup - add components first
        for (int i = 0; i < EntityCount; i++)
        {
            _world.AddComponent(_entities[i], new Position(Vector3.Zero));
        }

        // Actual benchmark
        for (int i = 0; i < EntityCount; i++)
        {
            _world.RemoveComponent<Position>(_entities[i]);
        }
    }
}
