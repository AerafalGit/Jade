// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by a gamepad axis input.
/// </summary>
/// <param name="Gamepad">The gamepad associated with the axis input event.</param>
/// <param name="Axis">The axis on the gamepad that triggered the event.</param>
/// <param name="Value">The value of the axis input, typically ranging from -1.0 to 1.0.</param>
public readonly record struct GamepadAxisEvent(Gamepad Gamepad, GamepadAxis Axis, float Value) : IEvent;
