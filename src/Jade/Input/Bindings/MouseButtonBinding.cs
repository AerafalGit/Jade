// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a mouse button input binding.
/// A mouse button binding maps a specific mouse button to an input action.
/// </summary>
/// <param name="Button">The mouse button associated with the binding.</param>
public sealed record MouseButtonBinding(MouseButton Button) : InputBinding;
