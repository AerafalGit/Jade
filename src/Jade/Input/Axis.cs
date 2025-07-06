// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;

namespace Jade.Input;

/// <summary>
/// Represents an axis manager for handling axis values.
/// </summary>
/// <typeparam name="T">
/// The type of axis, which must be an enumeration.
/// </typeparam>
public sealed class Axis<T>
    where T : struct, Enum
{
    private readonly Dictionary<T, float> _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="Axis{T}"/> class.
    /// </summary>
    public Axis()
    {
        _values = [];
    }

    /// <summary>
    /// Gets the value of the specified axis.
    /// </summary>
    /// <param name="axis">The axis to retrieve the value for.</param>
    /// <returns>The value of the axis, or 0 if not set.</returns>
    public float Get(T axis)
    {
        return _values.GetValueOrDefault(axis, 0f);
    }

    /// <summary>
    /// Gets the values of two specified axes as a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="axisX">The horizontal axis.</param>
    /// <param name="axisY">The vertical axis.</param>
    /// <returns>A <see cref="Vector2"/> containing the values of the two axes.</returns>
    public Vector2 Get(T axisX, T axisY)
    {
        return new Vector2(_values.GetValueOrDefault(axisX, 0f), _values.GetValueOrDefault(axisY, 0f));
    }

    /// <summary>
    /// Sets the value of the specified axis.
    /// </summary>
    /// <param name="axis">The axis to set the value for.</param>
    /// <param name="value">The value to set for the axis.</param>
    internal void Set(T axis, float value)
    {
        _values[axis] = value;
    }

    /// <summary>
    /// Clears all axis values.
    /// </summary>
    internal void Clear()
    {
        _values.Clear();
    }
}
