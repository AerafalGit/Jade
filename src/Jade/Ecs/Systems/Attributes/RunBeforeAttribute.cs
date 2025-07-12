// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunBeforeAttribute : Attribute
{
    public Type TargetSystem { get; }

    public RunBeforeAttribute(Type targetSystem)
    {
        if (targetSystem.BaseType != typeof(SystemBase))
            throw new ArgumentException($"Type {targetSystem.Name} must inherit from {nameof(SystemBase)}.");

        TargetSystem = targetSystem;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RunBeforeAttribute<T> : RunBeforeAttribute
    where T : SystemBase
{
    public RunBeforeAttribute() : base(typeof(T))
    {
    }
}
