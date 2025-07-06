// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input;

/// <summary>
/// Represents a manager for handling connected gamepads and their states.
/// </summary>
public sealed class Gamepads
{
    private readonly Dictionary<uint, Gamepad> _gamepads;
    private readonly HashSet<Gamepad> _justConnected;
    private readonly HashSet<Gamepad> _justDisconnected;

    /// <summary>
    /// Gets an enumerable of all currently connected gamepads.
    /// </summary>
    public IEnumerable<Gamepad> Connected =>
        _gamepads.Values;

    /// <summary>
    /// Gets an enumerable of gamepads that were just connected.
    /// </summary>
    public IEnumerable<Gamepad> JustConnected =>
        _justConnected;

    /// <summary>
    /// Gets an enumerable of gamepads that were just disconnected.
    /// </summary>
    public IEnumerable<Gamepad> JustDisconnected =>
        _justDisconnected;

    /// <summary>
    /// Initializes a new instance of the <see cref="Gamepads"/> class.
    /// </summary>
    public Gamepads()
    {
        _gamepads = [];
        _justConnected = [];
        _justDisconnected = [];
    }

    /// <summary>
    /// Determines whether a gamepad with the specified ID is currently connected.
    /// </summary>
    /// <param name="gamepadId">The ID of the gamepad to check.</param>
    /// <returns><c>true</c> if the gamepad is connected; otherwise, <c>false</c>.</returns>
    public bool IsConnected(uint gamepadId)
    {
        return _gamepads.ContainsKey(gamepadId);
    }

    /// <summary>
    /// Gets the gamepad with the specified ID, if it is connected.
    /// </summary>
    /// <param name="gamepadId">The ID of the gamepad to retrieve.</param>
    /// <returns>The <see cref="Gamepad"/> instance if connected; otherwise, <c>null</c>.</returns>
    public Gamepad? Get(uint gamepadId)
    {
        return _gamepads.GetValueOrDefault(gamepadId);
    }

    /// <summary>
    /// Marks a gamepad as connected and adds it to the list of just connected gamepads.
    /// </summary>
    /// <param name="id">The ID of the gamepad.</param>
    /// <param name="name">The name of the gamepad.</param>
    internal void Connect(uint id, string name)
    {
        var gamepad = new Gamepad(id, name);
        _gamepads[id] = gamepad;
        _justConnected.Add(gamepad);
    }

    /// <summary>
    /// Marks a gamepad as disconnected and adds it to the list of just disconnected gamepads.
    /// </summary>
    /// <param name="id">The ID of the gamepad to disconnect.</param>
    internal void Disconnect(uint id)
    {
        if (_gamepads.Remove(id, out var gamepad))
            _justDisconnected.Add(gamepad);
    }

    /// <summary>
    /// Clears the lists of gamepads that were just connected or disconnected.
    /// </summary>
    internal void ClearJustChanged()
    {
        _justConnected.Clear();
        _justDisconnected.Clear();
    }
}
