// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Reflection;
using Jade.Ecs.Systems.Attributes;

namespace Jade.Ecs.Systems;

internal sealed class SystemMetadata
{
    public SystemBase Instance { get; }

    public Type Type { get; }

    public HashSet<Type> RunAfter { get; }

    public HashSet<Type> RunBefore { get; }

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
