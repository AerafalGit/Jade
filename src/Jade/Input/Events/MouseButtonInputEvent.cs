// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by mouse button input.
/// </summary>
/// <param name="Button">The mouse button associated with the input event.</param>
/// <param name="State">The state of the mouse button (pressed or released).</param>
public readonly record struct MouseButtonInputEvent(MouseButton Button, ButtonState State) : IEvent;
