// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a gamepad button input binding.
/// A gamepad button binding maps a specific gamepad button to an input action.
/// </summary>
/// <param name="Button">The gamepad button associated with the binding.</param>
/// <param name="GamepadId">
/// The ID of the gamepad associated with the binding. Defaults to 0, which represents any gamepad.
/// </param>
public sealed record GamepadButtonBinding(GamepadButton Button, uint GamepadId = 0) : InputBinding;
