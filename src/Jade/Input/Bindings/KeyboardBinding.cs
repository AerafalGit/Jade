// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a keyboard input binding.
/// A keyboard binding maps a specific keyboard key to an input action.
/// </summary>
/// <param name="Key">The keyboard key associated with the binding.</param>
public sealed record KeyboardBinding(KeyboardKey Key) : InputBinding;
