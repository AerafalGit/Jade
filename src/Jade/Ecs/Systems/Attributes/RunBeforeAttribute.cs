// Copyright (c) Quantum 2025.
// Quantum licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Quantum/blob/main/LICENSE.

namespace Jade.Ecs.Systems.Attributes;

/// <summary>
/// Specifies that the annotated system should run before the specified target system.
/// This attribute can be applied to classes and allows multiple instances.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunBeforeAttribute : Attribute
{
    /// <summary>
    /// Gets the type of the target system that the annotated system should run before.
    /// </summary>
    public Type TargetSystem { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RunBeforeAttribute"/> class.
    /// </summary>
    /// <param name="targetSystem">The type of the target system.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the specified type does not inherit from <see cref="SystemBase"/>.
    /// </exception>
    public RunBeforeAttribute(Type targetSystem)
    {
        if (targetSystem.BaseType != typeof(SystemBase))
            throw new ArgumentException($"Type {targetSystem.Name} must inherit from {nameof(SystemBase)}.");

        TargetSystem = targetSystem;
    }
}

/// <summary>
/// Specifies that the annotated system should run before the specified generic target system.
/// This sealed attribute can be applied to classes and allows multiple instances.
/// </summary>
/// <typeparam name="T">The type of the target system, which must inherit from <see cref="SystemBase"/>.</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RunBeforeAttribute<T> : RunBeforeAttribute
    where T : SystemBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RunBeforeAttribute{T}"/> class.
    /// </summary>
    public RunBeforeAttribute() : base(typeof(T))
    {
    }
}
