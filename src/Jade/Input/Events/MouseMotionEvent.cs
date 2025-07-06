// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by mouse motion.
/// </summary>
/// <param name="Delta">The change in mouse position as a 2D vector.</param>
public readonly record struct MouseMotionEvent(Vector2 Delta) : IEvent;
