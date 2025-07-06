// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a gamepad axis input binding.
/// A gamepad axis binding maps a specific gamepad axis to an input action.
/// </summary>
/// <param name="Axis">The gamepad axis associated with the binding.</param>
/// <param name="GamepadId">
/// The ID of the gamepad associated with the binding. Defaults to 0, which represents any gamepad.
/// </param>
public sealed record GamepadAxisBinding(GamepadAxis Axis, uint GamepadId = 0) : InputBinding;
