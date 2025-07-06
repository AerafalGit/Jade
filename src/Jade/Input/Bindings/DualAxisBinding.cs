// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Input.Bindings;

/// <summary>
/// Represents a dual-axis input binding.
/// A dual-axis binding maps positive and negative inputs for both X and Y axes to input actions.
/// </summary>
/// <param name="PositiveX">The input binding for the positive X axis.</param>
/// <param name="NegativeX">The input binding for the negative X axis.</param>
/// <param name="PositiveY">The input binding for the positive Y axis.</param>
/// <param name="NegativeY">The input binding for the negative Y axis.</param>
public sealed record DualAxisBinding(InputBinding PositiveX, InputBinding NegativeX, InputBinding PositiveY, InputBinding NegativeY) : InputBinding;
