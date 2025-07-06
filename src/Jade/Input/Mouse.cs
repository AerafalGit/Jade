// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;

namespace Jade.Input;

/// <summary>
/// Represents a mouse input manager for tracking position, movement, and wheel deltas.
/// </summary>
public sealed class Mouse
{
    /// <summary>
    /// Gets or sets the current position of the mouse.
    /// </summary>
    public Vector2 Position { get; internal set; }

    /// <summary>
    /// Gets or sets the change in mouse position since the last update.
    /// </summary>
    public Vector2 Delta { get; internal set; }

    /// <summary>
    /// Gets or sets the change in mouse wheel position since the last update.
    /// </summary>
    public Vector2 Wheel { get; internal set; }

    /// <summary>
    /// Clears the deltas for mouse movement and wheel input.
    /// </summary>
    internal void ClearDeltas()
    {
        Delta = Vector2.Zero;
        Wheel = Vector2.Zero;
    }
}
