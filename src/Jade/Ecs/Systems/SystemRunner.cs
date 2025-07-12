// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Jade.Ecs.Systems;

internal sealed class SystemRunner : IDisposable
{
    private readonly World _world;
    private readonly Dictionary<SystemStage, List<SystemMetadata>> _systemsByStage;
    private readonly ConcurrentDictionary<SystemStage, SystemMetadata[]> _executionOrderCache;
    private readonly ConcurrentDictionary<Type, bool> _enabledSystems;
    private readonly SystemStage[] _stages;
    private readonly Lock _buildLock;

    private volatile bool _isDirty;

    public SystemRunner(World world)
    {
        _world = world;
        _systemsByStage = [];
        _executionOrderCache = new ConcurrentDictionary<SystemStage, SystemMetadata[]>();
        _enabledSystems = new ConcurrentDictionary<Type, bool>();
        _stages = Enum.GetValues<SystemStage>();
        _isDirty = true;
        _buildLock = new Lock();

        foreach (var stage in _stages)
        {
            if (stage is SystemStage.None)
                continue;

            _systemsByStage[stage] = [];
            _executionOrderCache[stage] = [];
        }
    }

    ~SystemRunner()
    {
        ReleaseUnmanagedResources();
    }

    public void AddSystems(SystemStage stage, params IEnumerable<SystemBase> systems)
    {
        foreach (var system in systems)
            AddSystem(stage, system, null);
    }

    public void AddSystem<T>(SystemStage stage, T system)
        where T : SystemBase
    {
        AddSystem(stage, system, typeof(T));
    }

    public void AddSystem<T>(SystemStage stage)
        where T : SystemBase, new()
    {
        AddSystem(stage, new T());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return _enabledSystems.TryGetValue(typeof(T), out var enabled) && enabled;
    }

    public void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        _enabledSystems[typeof(T)] = enabled;
    }

    public T? GetSystem<T>()
        where T : SystemBase
    {
        return _systemsByStage.Values
            .SelectMany(list => list)
            .FirstOrDefault(meta => meta.Type == typeof(T))
            ?.Instance as T;
    }

    public T GetRequiredSystem<T>()
        where T : SystemBase
    {
        return GetSystem<T>() ?? throw new InvalidOperationException($"System of type {typeof(T).Name} not found.");
    }

    public bool HasSystem<T>()
        where T : SystemBase
    {
        return GetSystem<T>() is not null;
    }

    internal void RunStage(SystemStage stage)
    {
        if (_isDirty)
        {
            lock (_buildLock)
            {
                if (_isDirty)
                {
                    BuildAllExecutionOrders();
                    _isDirty = false;
                }
            }
        }

        var systems = _executionOrderCache[stage];

        foreach (var metadata in systems)
        {
            if (!_enabledSystems.TryGetValue(metadata.Type, out var enabled) || !enabled)
                continue;

            try
            {
                ExecuteSystemStage(metadata.Instance, stage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing system {metadata.Type.Name} in stage {stage}: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new InvalidOperationException($"Error executing system {metadata.Type.Name} in stage {stage}. See inner exception for details.", ex);
            }
        }
    }

    public SystemStatistics GetSystemStats()
    {
        var totalSystems = _systemsByStage.Values.Sum(static list => list.Count);
        var enabledSystems = _enabledSystems.Count(static kvp => kvp.Value);

        return new SystemStatistics(totalSystems, enabledSystems, totalSystems - enabledSystems, _stages.Length - 1);
    }

    private void AddSystem(SystemStage stage, SystemBase system, Type? type)
    {
        if (stage is SystemStage.None)
            throw new ArgumentException("Cannot add a system to the None stage.", nameof(stage));

        system.World = _world;
        var metadata = new SystemMetadata(system, type ?? system.GetType());

        var allExistingSystems = _systemsByStage.Values
            .SelectMany(list => list)
            .Select(meta => meta.Type);

        if (allExistingSystems.Contains(metadata.Type))
            throw new InvalidOperationException($"System of type {metadata.Type.Name} is already registered.");

        foreach (var currentStage in _stages)
        {
            if (currentStage is SystemStage.None)
                continue;

            if (stage.HasFlag(currentStage))
                _systemsByStage[currentStage].Add(metadata);
        }

        _enabledSystems[metadata.Type] = true;
        _isDirty = true;
    }

    private void BuildAllExecutionOrders()
    {
        foreach (var stage in _stages)
        {
            if (stage is SystemStage.None)
                continue;

            var systems = _systemsByStage[stage];

            if (systems.Count is 0)
            {
                _executionOrderCache[stage] = [];
                continue;
            }

            var systemsByType = systems.ToDictionary(static s => s.Type);
            var sorted = new List<SystemMetadata>();
            var visited = new HashSet<Type>();
            var visiting = new HashSet<Type>();

            var dependencyGraph = systems.ToDictionary(
                static s => s.Type,
                static s => new HashSet<Type>(s.RunAfter)
            );

            foreach (var systemMeta in systems)
            {
                foreach (var beforeType in systemMeta.RunBefore)
                {
                    if (dependencyGraph.TryGetValue(beforeType, out var dependencies))
                        dependencies.Add(systemMeta.Type);
                }
            }

            foreach (var systemMeta in systems)
            {
                if (!visited.Contains(systemMeta.Type))
                    Visit(systemMeta, dependencyGraph, systemsByType, sorted, visited, visiting);
            }

            _executionOrderCache[stage] = [.. sorted];
        }
    }

    private void ExecuteSystemStage(SystemBase system, SystemStage stage)
    {
        switch (stage)
        {
            case SystemStage.None:
                break;
            case SystemStage.Startup:
                system.Startup();
                break;
            case SystemStage.PreUpdate:
                system.PreUpdate();
                break;
            case SystemStage.Update:
                system.Update();
                break;
            case SystemStage.PostUpdate:
                system.PostUpdate();
                _world.SwapEvents();
                break;
            case SystemStage.FixedUpdate:
                system.FixedUpdate();
                break;
            case SystemStage.Render:
                system.Render();
                break;
            case SystemStage.Cleanup:
                system.Cleanup();
                break;
        }
    }

    private static void Visit(
        SystemMetadata metadata,
        Dictionary<Type, HashSet<Type>> dependencyGraph,
        Dictionary<Type, SystemMetadata> systemsByType,
        List<SystemMetadata> sorted,
        HashSet<Type> visited,
        HashSet<Type> visiting)
    {
        if (visited.Contains(metadata.Type))
            return;

        if (!visiting.Add(metadata.Type))
            throw new InvalidOperationException($"Circular dependency detected for system: {metadata.Type.Name}.");

        if (dependencyGraph.TryGetValue(metadata.Type, out var dependencies))
        {
            foreach (var dependencyType in dependencies)
            {
                if (systemsByType.TryGetValue(dependencyType, out var dependencyMeta))
                    Visit(dependencyMeta, dependencyGraph, systemsByType, sorted, visited, visiting);
            }
        }

        visiting.Remove(metadata.Type);
        visited.Add(metadata.Type);
        sorted.Add(metadata);
    }

    private void ReleaseUnmanagedResources()
    {
        foreach (var metadata in _systemsByStage.Values.SelectMany(static systems => systems))
        {
            if (metadata.Instance is IDisposable disposableSystem)
                disposableSystem.Dispose();
        }

        _systemsByStage.Clear();
        _executionOrderCache.Clear();
        _enabledSystems.Clear();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
