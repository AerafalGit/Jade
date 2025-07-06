// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by mouse wheel movement.
/// </summary>
/// <param name="Delta">The change in the mouse wheel position as a 2D vector.</param>
public readonly record struct MouseWheelEvent(Vector2 Delta) : IEvent;
