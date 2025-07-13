// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Jade.Ecs;
using Jade.Ecs.Abstractions;

namespace Jade.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class RelationBenchmarks
{
    private World _world = null!;
    private Entity[] _parents = null!;
    private Entity[] _children = null!;

    [Params(1000)]
    public int EntityCount;

    [GlobalSetup]
    public void Setup()
    {
        _world = new World();
        _parents = new Entity[EntityCount];
        _children = new Entity[EntityCount * 5];

        for (var i = 0; i < EntityCount; i++)
        {
            _parents[i] = _world.CreateEntity();
        }

        for (var i = 0; i < _children.Length; i++)
        {
            _children[i] = _world.CreateEntity();
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _world.Dispose();
    }

    [Benchmark(Baseline = true)]
    public void SetParentChildRelations()
    {
        for (var i = 0; i < EntityCount; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                var childIndex = i * 5 + j;
                _world.SetParent(_children[childIndex], _parents[i]);
            }
        }
    }

    [Benchmark]
    public void QueryChildrenOfParents()
    {
        for (var i = 0; i < EntityCount; i++)
        {
            _ = _world.GetChildren(_parents[i]);
        }
    }
}
