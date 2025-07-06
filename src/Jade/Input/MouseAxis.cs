// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input;

/// <summary>
/// Represents the axes of a mouse input device.
/// Mouse axes are used to track movement and scrolling.
/// </summary>
public enum MouseAxis
{
    /// <summary>
    /// Represents the horizontal movement of the mouse.
    /// </summary>
    DeltaX,

    /// <summary>
    /// Represents the vertical movement of the mouse.
    /// </summary>
    DeltaY,

    /// <summary>
    /// Represents the horizontal scrolling of the mouse wheel.
    /// </summary>
    WheelX,

    /// <summary>
    /// Represents the vertical scrolling of the mouse wheel.
    /// </summary>
    WheelY
}
