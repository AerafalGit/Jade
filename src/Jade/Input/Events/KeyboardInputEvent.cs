// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered by keyboard input.
/// </summary>
/// <param name="Key">The key associated with the keyboard input event.</param>
/// <param name="State">The state of the key (pressed or released).</param>
public readonly record struct KeyboardInputEvent(KeyboardKey Key, ButtonState State) : IEvent;
