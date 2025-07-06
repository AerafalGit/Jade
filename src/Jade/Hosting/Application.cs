// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs;

namespace Jade.Hosting;

/// <summary>
/// Represents the main application class for the ECS framework.
/// Provides functionality to manage the ECS world and application lifecycle.
/// </summary>
public sealed class Application : IDisposable
{
    /// <summary>
    /// Gets the ECS world associated with the application.
    /// </summary>
    public World World { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Application"/> class.
    /// </summary>
    /// <param name="world">The ECS world to associate with the application.</param>
    public Application(World world)
    {
        World = world;
    }

    /// <summary>
    /// Runs the application.
    /// </summary>
    public void Run()
    {
    }

    /// <summary>
    /// Disposes the application and releases resources.
    /// </summary>
    public void Dispose()
    {
        World.Dispose();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ApplicationBuilder"/> class.
    /// </summary>
    /// <returns>An <see cref="ApplicationBuilder"/> instance for configuring the application.</returns>
    public static ApplicationBuilder CreateBuilder()
    {
        return new ApplicationBuilder();
    }
}
