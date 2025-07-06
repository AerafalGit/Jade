// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a gamepad stick input binding.
/// A gamepad stick binding maps the X and Y axes of a gamepad stick to input actions.
/// </summary>
/// <param name="XAxis">The gamepad axis associated with the X-axis of the stick.</param>
/// <param name="YAxis">The gamepad axis associated with the Y-axis of the stick.</param>
/// <param name="GamepadId">
/// The ID of the gamepad associated with the binding. Defaults to 0, which represents any gamepad.
/// </param>
public sealed record GamepadStickBinding(GamepadAxis XAxis, GamepadAxis YAxis, uint GamepadId = 0) : InputBinding;
