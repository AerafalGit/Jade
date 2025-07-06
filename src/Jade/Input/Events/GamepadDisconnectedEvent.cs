// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;

namespace Jade.Input.Events;

/// <summary>
/// Represents an event triggered when a gamepad is disconnected.
/// </summary>
/// <param name="GamepadId">The unique identifier of the gamepad that was disconnected.</param>
public readonly record struct GamepadDisconnectedEvent(uint GamepadId) : IEvent;
