// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input;

/// <summary>
/// Represents a gamepad device.
/// </summary>
/// <param name="Id">The unique identifier of the gamepad.</param>
/// <param name="Name">The name of the gamepad.</param>
public readonly record struct Gamepad(uint Id, string Name);
