// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;

namespace Jade.Input;

/// <summary>
/// Represents the state of actions in an input system.
/// The <see cref="ActionState{T}"/> class tracks the pressed, just pressed, just released states,
/// as well as values and axes for actions defined by an enumeration.
/// </summary>
/// <typeparam name="T">The enumeration type representing the actions.</typeparam>
public sealed class ActionState<T>
    where T : struct, Enum
{
    private readonly Dictionary<T, bool> _pressed;
    private readonly Dictionary<T, bool> _justPressed;
    private readonly Dictionary<T, bool> _justReleased;
    private readonly Dictionary<T, float> _values;
    private readonly Dictionary<T, Vector2> _axis;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionState{T}"/> class.
    /// </summary>
    public ActionState()
    {
        _pressed = [];
        _justPressed = [];
        _justReleased = [];
        _values = [];
        _axis = [];
    }

    /// <summary>
    /// Checks if the specified action is currently pressed.
    /// </summary>
    /// <param name="action">The action to check.</param>
    /// <returns><c>true</c> if the action is pressed; otherwise, <c>false</c>.</returns>
    public bool Pressed(T action)
    {
        return _pressed.GetValueOrDefault(action, false);
    }

    /// <summary>
    /// Checks if the specified action was just pressed.
    /// </summary>
    /// <param name="action">The action to check.</param>
    /// <returns><c>true</c> if the action was just pressed; otherwise, <c>false</c>.</returns>
    public bool JustPressed(T action)
    {
        return _justPressed.GetValueOrDefault(action, false);
    }

    /// <summary>
    /// Checks if the specified action was just released.
    /// </summary>
    /// <param name="action">The action to check.</param>
    /// <returns><c>true</c> if the action was just released; otherwise, <c>false</c>.</returns>
    public bool JustReleased(T action)
    {
        return _justReleased.GetValueOrDefault(action, false);
    }

    /// <summary>
    /// Retrieves the value associated with the specified action.
    /// </summary>
    /// <param name="action">The action to retrieve the value for.</param>
    /// <returns>The value associated with the action.</returns>
    public float Value(T action)
    {
        return _values.GetValueOrDefault(action, 0f);
    }

    /// <summary>
    /// Retrieves the axis values associated with the specified action.
    /// </summary>
    /// <param name="action">The action to retrieve the axis values for.</param>
    /// <returns>The axis values associated with the action.</returns>
    public Vector2 Axis(T action)
    {
        return _axis.GetValueOrDefault(action, Vector2.Zero);
    }

    /// <summary>
    /// Sets the pressed state for the specified action.
    /// </summary>
    /// <param name="action">The action to update.</param>
    /// <param name="pressed">The new pressed state.</param>
    internal void SetPressed(T action, bool pressed)
    {
        var wasPressed = _pressed.GetValueOrDefault(action, false);

        _pressed[action] = pressed;

        switch (pressed)
        {
            case true when !wasPressed:
                _justPressed[action] = true;
                break;
            case false when wasPressed:
                _justReleased[action] = true;
                break;
        }
    }

    /// <summary>
    /// Sets the value for the specified action.
    /// </summary>
    /// <param name="action">The action to update.</param>
    /// <param name="value">The new value.</param>
    internal void SetValue(T action, float value)
    {
        _values[action] = value;
    }

    /// <summary>
    /// Sets the axis values for the specified action.
    /// </summary>
    /// <param name="action">The action to update.</param>
    /// <param name="axis">The new axis values.</param>
    internal void SetAxis(T action, Vector2 axis)
    {
        _axis[action] = axis;
    }

    /// <summary>
    /// Clears the just pressed and just released states for all actions.
    /// </summary>
    internal void ClearJustChanged()
    {
        _justPressed.Clear();
        _justReleased.Clear();
    }
}
