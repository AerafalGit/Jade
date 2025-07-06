// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by a gamepad button input.
/// </summary>
/// <param name="Gamepad">The gamepad associated with the input event.</param>
/// <param name="Button">The button on the gamepad that triggered the event.</param>
/// <param name="State">The state of the button (pressed or released).</param>
public readonly record struct GamepadButtonInputEvent(Gamepad Gamepad, GamepadButton Button, ButtonState State) : IEvent;
