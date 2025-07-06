// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Input.Bindings;

namespace Jade.Input;

/// <summary>
/// Represents a mapping between input bindings and actions.
/// The <see cref="InputMap{T}"/> allows associating various input bindings (e.g., keyboard keys, mouse buttons, gamepad buttons)
/// with specific actions defined by an enumeration.
/// </summary>
/// <typeparam name="T">The enumeration type representing the actions.</typeparam>
public readonly struct InputMap<T> : IComponent
    where T : struct, Enum
{
    private readonly Dictionary<T, List<InputBinding>> _bindings;

    /// <summary>
    /// Initializes a new instance of the <see cref="InputMap{T}"/> struct.
    /// </summary>
    public InputMap()
    {
        _bindings = [];
    }

    /// <summary>
    /// Inserts a new input binding for a specific action.
    /// </summary>
    /// <param name="binding">The input binding to associate with the action.</param>
    /// <param name="action">The action to associate the binding with.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> Insert(InputBinding binding, T action)
    {
        if (!_bindings.ContainsKey(action))
            _bindings[action] = [];

        _bindings[action].Add(binding);
        return this;
    }

    /// <summary>
    /// Inserts a keyboard key binding for a specific action.
    /// </summary>
    /// <param name="key">The keyboard key to associate with the action.</param>
    /// <param name="action">The action to associate the key with.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> Insert(KeyboardKey key, T action)
    {
        return Insert(new KeyboardBinding(key), action);
    }

    /// <summary>
    /// Inserts a mouse button binding for a specific action.
    /// </summary>
    /// <param name="button">The mouse button to associate with the action.</param>
    /// <param name="action">The action to associate the button with.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> Insert(MouseButton button, T action)
    {
        return Insert(new MouseButtonBinding(button), action);
    }

    /// <summary>
    /// Inserts a gamepad button binding for a specific action.
    /// </summary>
    /// <param name="button">The gamepad button to associate with the action.</param>
    /// <param name="action">The action to associate the button with.</param>
    /// <param name="gamepadId">The ID of the gamepad. Defaults to 0, representing any gamepad.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> Insert(GamepadButton button, T action, uint gamepadId = 0)
    {
        return Insert(new GamepadButtonBinding(button, gamepadId), action);
    }

    /// <summary>
    /// Inserts a dual-axis keyboard binding for a specific action.
    /// </summary>
    /// <param name="positiveX">The keyboard key for the positive X axis.</param>
    /// <param name="negativeX">The keyboard key for the negative X axis.</param>
    /// <param name="positiveY">The keyboard key for the positive Y axis.</param>
    /// <param name="negativeY">The keyboard key for the negative Y axis.</param>
    /// <param name="action">The action to associate the dual-axis binding with.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> InsertDualAxis(KeyboardKey positiveX, KeyboardKey negativeX, KeyboardKey positiveY, KeyboardKey negativeY, T action)
    {
        return Insert(new DualAxisBinding(new KeyboardBinding(positiveX), new KeyboardBinding(negativeX), new KeyboardBinding(positiveY), new KeyboardBinding(negativeY)), action);
    }

    /// <summary>
    /// Inserts a gamepad stick binding for a specific action.
    /// </summary>
    /// <param name="xAxis">The gamepad axis for the X-axis of the stick.</param>
    /// <param name="yAxis">The gamepad axis for the Y-axis of the stick.</param>
    /// <param name="action">The action to associate the gamepad stick binding with.</param>
    /// <param name="gamepadId">The ID of the gamepad. Defaults to 0, representing any gamepad.</param>
    /// <returns>The updated <see cref="InputMap{T}"/> instance.</returns>
    public InputMap<T> InsertGamepadStick(GamepadAxis xAxis, GamepadAxis yAxis, T action, uint gamepadId = 0)
    {
        return Insert(new GamepadStickBinding(xAxis, yAxis, gamepadId), action);
    }

    /// <summary>
    /// Retrieves the input bindings associated with a specific action.
    /// </summary>
    /// <param name="action">The action to retrieve bindings for.</param>
    /// <returns>An enumerable of input bindings associated with the action.</returns>
    internal IEnumerable<InputBinding> GetBindings(T action)
    {
        return _bindings.GetValueOrDefault(action, []);
    }
}
