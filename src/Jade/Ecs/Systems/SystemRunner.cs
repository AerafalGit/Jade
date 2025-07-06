// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

/// <summary>
/// Responsible for managing and executing ECS (Entity Component System) systems.
/// Handles system registration, execution order, and enabled state.
/// </summary>
internal sealed class SystemRunner
{
    private readonly World _world;
    private readonly Dictionary<SystemStage, List<SystemMetadata>> _systemsByStage;
    private readonly Dictionary<SystemStage, List<SystemMetadata>> _executionOrder;
    private readonly Dictionary<Type, bool> _enabledSystems;
    private readonly SystemStage[] _stages;

    private bool _isDirty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemRunner"/> class.
    /// Sets up the system runner with the ECS world and initializes storage for systems.
    /// </summary>
    /// <param name="world">The ECS world instance.</param>
    public SystemRunner(World world)
    {
        _world = world;
        _systemsByStage = [];
        _executionOrder = [];
        _enabledSystems = [];
        _stages = Enum.GetValues<SystemStage>();
        _isDirty = true;

        foreach (var stage in _stages)
        {
            if (stage is SystemStage.None)
                continue;

            _systemsByStage[stage] = [];
            _executionOrder[stage] = [];
        }
    }

    /// <summary>
    /// Adds multiple systems to the specified stage.
    /// </summary>
    /// <param name="stage">The stage to add the systems to.</param>
    /// <param name="systems">The systems to add.</param>
    public void AddSystems(SystemStage stage, params IEnumerable<SystemBase> systems)
    {
        foreach (var system in systems)
            AddSystem(stage, system, null);
    }

    /// <summary>
    /// Adds a single system to the specified stage.
    /// </summary>
    /// <typeparam name="T">The type of the system.</typeparam>
    /// <param name="stage">The stage to add the system to.</param>
    /// <param name="system">The system instance to add.</param>
    public void AddSystem<T>(SystemStage stage, T system)
        where T : SystemBase
    {
        AddSystem(stage, system, typeof(T));
    }

    /// <summary>
    /// Checks if a system of the specified type is enabled.
    /// </summary>
    /// <typeparam name="T">The type of the system.</typeparam>
    /// <returns><c>true</c> if the system is enabled; otherwise, <c>false</c>.</returns>
    public bool IsSystemEnabled<T>()
        where T : SystemBase
    {
        return _enabledSystems.TryGetValue(typeof(T), out var enabled) && enabled;
    }

    /// <summary>
    /// Enables or disables a system of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the system.</typeparam>
    /// <param name="enabled">The enabled state to set.</param>
    public void SetSystemEnabled<T>(bool enabled)
        where T : SystemBase
    {
        _enabledSystems[typeof(T)] = enabled;
        _isDirty = true;
    }

    /// <summary>
    /// Retrieves a system of the specified type, throwing an exception if it is not found.
    /// </summary>
    /// <typeparam name="T">The type of the system.</typeparam>
    /// <returns>The system instance.</returns>
    public T GetSystem<T>()
        where T : SystemBase
    {
        return (T)_systemsByStage.Values.SelectMany(x => x).First(static x => x.Type == typeof(T)).Instance;
    }

    /// <summary>
    /// Executes all systems in the specified stage.
    /// </summary>
    /// <param name="stage">The stage to execute.</param>
    internal void RunStage(SystemStage stage)
    {
        if (_isDirty)
        {
            BuildAllExecutionOrders();
            _isDirty = false;
        }

        foreach (var metadata in _executionOrder[stage].Where(x => _enabledSystems[x.Type]))
        {
            switch (stage)
            {
                case SystemStage.None:
                    break;

                case SystemStage.Startup:
                    metadata.Instance.Startup();
                    break;

                case SystemStage.PreUpdate:
                    metadata.Instance.PreUpdate();
                    break;

                case SystemStage.Update:
                    metadata.Instance.Update();
                    break;

                case SystemStage.PostUpdate:
                    metadata.Instance.PostUpdate();
                    _world.SwapEvents();
                    break;

                case SystemStage.FixedUpdate:
                    metadata.Instance.FixedUpdate();
                    break;

                case SystemStage.Render:
                    metadata.Instance.Render();
                    break;

                case SystemStage.Cleanup:
                    metadata.Instance.Cleanup();
                    break;
            }
        }
    }

    /// <summary>
    /// Adds a system to the specified stage and updates its metadata.
    /// </summary>
    /// <param name="stage">The stage to add the system to.</param>
    /// <param name="system">The system instance to add.</param>
    /// <param name="type">The type of the system.</param>
    private void AddSystem(SystemStage stage, SystemBase system, Type? type)
    {
        if (stage is SystemStage.None)
            throw new ArgumentException("Cannot add a system to the None stage.", nameof(stage));

        system.World = _world;

        var metadata = new SystemMetadata(system, type ?? system.GetType());

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

    /// <summary>
    /// Builds the execution order for all systems in all stages.
    /// </summary>
    private void BuildAllExecutionOrders()
    {
        foreach (var stage in Enum.GetValues<SystemStage>())
        {
            var systems = _systemsByStage[stage];

            if (systems.Count is 0)
                continue;

            var systemsByType = systems.ToDictionary(s => s.Type);

            var sorted = new List<SystemMetadata>();
            var visited = new HashSet<Type>();
            var visiting = new HashSet<Type>();

            var dependencyGraph = systems.ToDictionary(static s => s.Type, static s => new HashSet<Type>(s.RunAfter));

            foreach (var systemMeta in systems)
            {
                foreach(var beforeType in systemMeta.RunBefore)
                {
                    if(dependencyGraph.TryGetValue(beforeType, out var dependencies))
                        dependencies.Add(systemMeta.Type);
                }
            }

            foreach (var systemMeta in systems)
            {
                if (!visited.Contains(systemMeta.Type))
                    Visit(systemMeta, dependencyGraph, systemsByType, sorted, visited, visiting);
            }

            _executionOrder[stage] = sorted;
        }
    }

    /// <summary>
    /// Visits a system metadata node in the dependency graph to sort execution order.
    /// </summary>
    /// <param name="metadata">The system metadata to visit.</param>
    /// <param name="graph">The dependency graph.</param>
    /// <param name="dependencyGraph">The graph of system dependencies.</param>
    /// <param name="sorted">The sorted list of systems.</param>
    /// <param name="visited">The set of visited system types.</param>
    /// <param name="visiting">The set of currently visiting system types.</param>
    /// <exception cref="InvalidOperationException">Thrown if a circular dependency is detected.</exception>
    private static void Visit(
        SystemMetadata metadata,
        Dictionary<Type, HashSet<Type>> graph,
        Dictionary<Type, SystemMetadata> dependencyGraph,
        List<SystemMetadata> sorted,
        HashSet<Type> visited,
        HashSet<Type> visiting)
    {
        if (visited.Contains(metadata.Type))
            return;

        if (!visiting.Add(metadata.Type))
            throw new InvalidOperationException($"Circular dependency detected for system: {metadata.Type.Name}.");

        if (graph.TryGetValue(metadata.Type, out var dependencies))
        {
            foreach (var dependencyType in dependencies)
            {
                if (dependencyGraph.TryGetValue(dependencyType, out var dependencyMeta))
                    Visit(dependencyMeta, graph, dependencyGraph, sorted, visited, visiting);
            }
        }

        visiting.Remove(metadata.Type);
        visited.Add(metadata.Type);
        sorted.Add(metadata);
    }
}
