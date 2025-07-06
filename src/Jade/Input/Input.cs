// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input;

/// <summary>
/// Represents an input manager for handling button states and events.
/// </summary>
/// <typeparam name="T">
/// The type of input, which must be an enumeration.
/// </typeparam>
public sealed class Input<T>
    where T : struct, Enum
{
    private readonly Dictionary<T, ButtonState> _currentStates;
    private readonly HashSet<T> _justPressed;
    private readonly HashSet<T> _justReleased;

    /// <summary>
    /// Initializes a new instance of the <see cref="Input{T}"/> class.
    /// </summary>
    public Input()
    {
        _currentStates = [];
        _justPressed = [];
        _justReleased = [];
    }

    /// <summary>
    /// Determines whether the specified input is currently pressed.
    /// </summary>
    /// <param name="input">The input to check.</param>
    /// <returns><c>true</c> if the input is pressed; otherwise, <c>false</c>.</returns>
    public bool Pressed(T input)
    {
        return _currentStates.GetValueOrDefault(input) is ButtonState.Pressed;
    }

    /// <summary>
    /// Determines whether the specified input was just pressed.
    /// </summary>
    /// <param name="input">The input to check.</param>
    /// <returns><c>true</c> if the input was just pressed; otherwise, <c>false</c>.</returns>
    public bool JustPressed(T input)
    {
        return _justPressed.Contains(input);
    }

    /// <summary>
    /// Determines whether the specified input was just released.
    /// </summary>
    /// <param name="input">The input to check.</param>
    /// <returns><c>true</c> if the input was just released; otherwise, <c>false</c>.</returns>
    public bool JustReleased(T input)
    {
        return _justReleased.Contains(input);
    }

    /// <summary>
    /// Determines whether any of the specified inputs are currently pressed.
    /// </summary>
    /// <param name="inputs">The inputs to check.</param>
    /// <returns><c>true</c> if any input is pressed; otherwise, <c>false</c>.</returns>
    public bool Any(params T[] inputs)
    {
        return inputs.Any(Pressed);
    }

    /// <summary>
    /// Determines whether all of the specified inputs are currently pressed.
    /// </summary>
    /// <param name="inputs">The inputs to check.</param>
    /// <returns><c>true</c> if all inputs are pressed; otherwise, <c>false</c>.</returns>
    public bool All(params T[] inputs)
    {
        return inputs.All(Pressed);
    }

    /// <summary>
    /// Gets all inputs that are currently pressed.
    /// </summary>
    /// <returns>An enumerable of inputs that are pressed.</returns>
    public IEnumerable<T> GetPressed()
    {
        return _currentStates
            .Where(static kvp => kvp.Value is ButtonState.Pressed)
            .Select(static kvp => kvp.Key);
    }

    /// <summary>
    /// Gets all inputs that were just pressed.
    /// </summary>
    /// <returns>An enumerable of inputs that were just pressed.</returns>
    public IEnumerable<T> GetJustPressed()
    {
        return _justPressed;
    }

    /// <summary>
    /// Gets all inputs that were just released.
    /// </summary>
    /// <returns>An enumerable of inputs that were just released.</returns>
    public IEnumerable<T> GetJustReleased()
    {
        return _justReleased;
    }

    /// <summary>
    /// Marks the specified input as pressed.
    /// </summary>
    /// <param name="input">The input to mark as pressed.</param>
    internal void Press(T input)
    {
        if (!Pressed(input))
        {
            _currentStates[input] = ButtonState.Pressed;
            _justPressed.Add(input);
        }
    }

    /// <summary>
    /// Marks the specified input as released.
    /// </summary>
    /// <param name="input">The input to mark as released.</param>
    internal void Release(T input)
    {
        if (Pressed(input))
        {
            _currentStates[input] = ButtonState.Released;
            _justReleased.Add(input);
        }
    }

    /// <summary>
    /// Clears the inputs that were just pressed or released.
    /// </summary>
    internal void ClearJustChanged()
    {
        _justPressed.Clear();
        _justReleased.Clear();
    }
}
