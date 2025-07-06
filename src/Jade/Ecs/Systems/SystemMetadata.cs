// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Reflection;
using Jade.Ecs.Systems.Attributes;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents metadata for a system in the ECS (Entity Component System) world.
/// Stores the system instance, its type, and dependencies for execution order.
/// </summary>
internal sealed class SystemMetadata
{
    /// <summary>
    /// Gets the instance of the system.
    /// </summary>
    public SystemBase Instance { get; }

    /// <summary>
    /// Gets the type of the system.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Gets the set of systems that this system should run after.
    /// </summary>
    public HashSet<Type> RunAfter { get; }

    /// <summary>
    /// Gets the set of systems that this system should run before.
    /// </summary>
    public HashSet<Type> RunBefore { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemMetadata"/> class.
    /// Extracts execution order attributes from the system type.
    /// </summary>
    /// <param name="instance">The instance of the system.</param>
    /// <param name="type">The type of the system.</param>
    public SystemMetadata(SystemBase instance, Type type)
    {
        Instance = instance;
        Type = type;
        RunAfter = [];
        RunBefore = [];

        foreach (var attribute in Type.GetCustomAttributes<RunAfterAttribute>(true))
            RunAfter.Add(attribute.TargetSystem);

        foreach (var attribute in Type.GetCustomAttributes<RunBeforeAttribute>(true))
            RunBefore.Add(attribute.TargetSystem);
    }
}
