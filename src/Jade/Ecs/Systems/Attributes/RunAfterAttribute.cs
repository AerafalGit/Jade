// Copyright (c) Quantum 2025.
// Quantum licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Quantum/blob/main/LICENSE.

namespace Jade.Ecs.Systems.Attributes;

/// <summary>
/// Specifies that the annotated system should run after the specified target system.
/// This attribute can be applied to classes and allows multiple instances.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunAfterAttribute : Attribute
{
    /// <summary>
    /// Gets the type of the target system that the annotated system should run after.
    /// </summary>
    public Type TargetSystem { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RunAfterAttribute"/> class.
    /// </summary>
    /// <param name="targetSystem">The type of the target system.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the specified type does not inherit from <see cref="SystemBase"/>.
    /// </exception>
    public RunAfterAttribute(Type targetSystem)
    {
        if (targetSystem.BaseType != typeof(SystemBase))
            throw new ArgumentException($"Type {targetSystem.Name} must inherit from {nameof(SystemBase)}.");

        TargetSystem = targetSystem;
    }
}

/// <summary>
/// Specifies that the annotated system should run after the specified generic target system.
/// This sealed attribute can be applied to classes and allows multiple instances.
/// </summary>
/// <typeparam name="T">The type of the target system, which must inherit from <see cref="SystemBase"/>.</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RunAfterAttribute<T> : RunAfterAttribute
    where T : SystemBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RunAfterAttribute{T}"/> class.
    /// </summary>
    public RunAfterAttribute() : base(typeof(T))
    {
    }
}
