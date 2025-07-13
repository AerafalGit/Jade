// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;

namespace Jade.Benchmarks.Configuration;

[Config(typeof(EcsConfiguration))]
public sealed class EcsConfiguration : ManualConfig
{
    public EcsConfiguration()
    {
        AddJob(Job.Default
            .WithWarmupCount(3)
            .WithIterationCount(10)
            .WithInvocationCount(1)
            .WithUnrollFactor(1));

        AddColumn(StatisticColumn.Mean);
        AddColumn(StatisticColumn.StdDev);
        AddColumn(StatisticColumn.Median);
        AddColumn(BaselineColumn.Default);
        AddColumn(StatisticColumn.AllStatistics);

        AddExporter(MarkdownExporter.GitHub);
        AddExporter(HtmlExporter.Default);

        AddLogger(ConsoleLogger.Default);
        AddLogger(ConsoleLogger.Unicode);

        WithOption(ConfigOptions.DisableOptimizationsValidator, true);
    }
}
