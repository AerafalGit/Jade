// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered when a gamepad is connected.
/// </summary>
/// <param name="GamepadId">The unique identifier of the connected gamepad.</param>
/// <param name="GamepadName">The name of the connected gamepad.</param>
public readonly record struct GamepadConnectedEvent(uint GamepadId, string GamepadName) : IEvent;
