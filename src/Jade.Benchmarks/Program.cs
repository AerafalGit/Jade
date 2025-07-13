using BenchmarkDotNet.Running;
using Jade.Benchmarks.Benchmarks;
using Jade.Benchmarks.Configuration;

var config = new EcsConfiguration();

Console.WriteLine("=== ECS Performance Benchmarks ===\n");

BenchmarkRunner.Run<EntityLifecycleBenchmarks>(config);
BenchmarkRunner.Run<ComponentBenchmarks>(config);
BenchmarkRunner.Run<QueryBenchmarks>(config);
BenchmarkRunner.Run<SystemBenchmarks>(config);
BenchmarkRunner.Run<RelationBenchmarks>(config);
BenchmarkRunner.Run<StressTestBenchmarks>(config);

Console.WriteLine("\nBenchmarks completed! Check the generated reports for detailed results.");
