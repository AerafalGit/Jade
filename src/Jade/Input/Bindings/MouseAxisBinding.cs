// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a mouse axis input binding.
/// A mouse axis binding maps a specific mouse axis to an input action.
/// </summary>
/// <param name="Axis">The mouse axis associated with the binding.</param>
public sealed record MouseAxisBinding(MouseAxis Axis) : InputBinding;
