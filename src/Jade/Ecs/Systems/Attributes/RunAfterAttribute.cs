// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunAfterAttribute : Attribute
{
    public Type TargetSystem { get; }

    public RunAfterAttribute(Type targetSystem)
    {
        if (targetSystem.BaseType != typeof(SystemBase))
            throw new ArgumentException($"Type {targetSystem.Name} must inherit from {nameof(SystemBase)}.");

        TargetSystem = targetSystem;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RunAfterAttribute<T> : RunAfterAttribute
    where T : SystemBase
{
    public RunAfterAttribute() : base(typeof(T))
    {
    }
}
