// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Systems;
using Jade.Ecs.Systems.Attributes;
using Jade.Input.Bindings;

namespace Jade.Input;

/// <summary>
/// Represents a system that processes input bindings and updates the state of actions.
/// The <see cref="ActionInputSystem{T}"/> class evaluates various input bindings (keyboard, mouse, gamepad)
/// and updates the pressed, value, and axis states for actions defined by an enumeration.
/// </summary>
/// <typeparam name="T">The enumeration type representing the actions.</typeparam>
[RunAfter<InputSystem>]
public sealed class ActionInputSystem<T> : SystemBase
    where T : struct, Enum
{
    /// <summary>
    /// Stores all possible actions defined by the enumeration <typeparamref name="T"/>.
    /// </summary>
    private static readonly T[] s_actions = Enum.GetValues<T>();

    /// <summary>
    /// Executes the pre-update logic for the system.
    /// This method evaluates input bindings and updates the action states.
    /// </summary>
    public override void PreUpdate()
    {
        var keyboard = GetResource<Input<KeyboardKey>>();
        var mouseButtons = GetResource<Input<MouseButton>>();
        var gamepadButtons = GetResource<Input<GamepadButton>>();
        var gamepadAxes = GetResource<Axis<GamepadAxis>>();
        var mouse = GetResource<Mouse>();
        var actionState = GetResource<ActionState<T>>();

        Query((ref InputMap<T> inputMap) =>
        {
            foreach (var action in s_actions)
            {
                var bindings = inputMap.GetBindings(action);

                var isPressed = false;
                var maxValue = 0f;
                var axisValue = Vector2.Zero;

                foreach (var binding in bindings)
                {
                    switch (binding)
                    {
                        case KeyboardBinding kb:
                            if (keyboard.Pressed(kb.Key))
                            {
                                isPressed = true;
                                maxValue = Math.Max(maxValue, 1f);
                            }
                            break;

                        case MouseButtonBinding mb:
                            if (mouseButtons.Pressed(mb.Button))
                            {
                                isPressed = true;
                                maxValue = Math.Max(maxValue, 1f);
                            }
                            break;

                        case GamepadButtonBinding gb:
                            if (gamepadButtons.Pressed(gb.Button))
                            {
                                isPressed = true;
                                maxValue = Math.Max(maxValue, 1f);
                            }
                            break;

                        case GamepadAxisBinding ga:
                            var axisVal = gamepadAxes.Get(ga.Axis);
                            if (Math.Abs(axisVal) > 0.1f)
                            {
                                isPressed = Math.Abs(axisVal) > 0.1f;
                                maxValue = Math.Max(maxValue, Math.Abs(axisVal));
                            }
                            break;

                        case MouseAxisBinding ma:
                            var mouseVal = ma.Axis switch
                            {
                                MouseAxis.DeltaX => mouse.Delta.X,
                                MouseAxis.DeltaY => mouse.Delta.Y,
                                MouseAxis.WheelX => mouse.Wheel.X,
                                MouseAxis.WheelY => mouse.Wheel.Y,
                                _ => 0f
                            };
                            if (Math.Abs(mouseVal) > 0.01f)
                            {
                                isPressed = true;
                                maxValue = Math.Max(maxValue, Math.Abs(mouseVal));
                            }
                            break;

                        case DualAxisBinding da:
                            var axis = EvaluateDualAxis(da, keyboard, mouseButtons, gamepadButtons, gamepadAxes, mouse);
                            if (axis.LengthSquared() > 0.01f)
                            {
                                isPressed = true;
                                axisValue += axis;
                                maxValue = Math.Max(maxValue, axis.Length());
                            }
                            break;

                        case GamepadStickBinding gs:
                            var stick = gamepadAxes.Get(gs.XAxis, gs.YAxis);
                            if (stick.LengthSquared() > 0.1f)
                            {
                                isPressed = true;
                                axisValue += stick;
                                maxValue = Math.Max(maxValue, stick.Length());
                            }
                            break;
                    }
                }

                actionState.SetPressed(action, isPressed);
                actionState.SetValue(action, maxValue);
                actionState.SetAxis(action, axisValue.LengthSquared() > 0.01f ? Vector2.Normalize(axisValue) : axisValue);
            }
        });
    }

    /// <summary>
    /// Executes the post-update logic for the system.
    /// This method clears the "just pressed" and "just released" states for all actions.
    /// </summary>
    public override void PostUpdate()
    {
        GetResource<ActionState<T>>().ClearJustChanged();
    }

    /// <summary>
    /// Evaluates the dual-axis binding and calculates the resulting axis values.
    /// </summary>
    /// <param name="binding">The dual-axis binding to evaluate.</param>
    /// <param name="keyboard">The keyboard input resource.</param>
    /// <param name="mouseButtons">The mouse button input resource.</param>
    /// <param name="gamepadButtons">The gamepad button input resource.</param>
    /// <param name="gamepadAxes">The gamepad axis input resource.</param>
    /// <param name="mouse">The mouse input resource.</param>
    /// <returns>The calculated axis values as a <see cref="Vector2"/>.</returns>
    private static Vector2 EvaluateDualAxis(
        DualAxisBinding binding,
        Input<KeyboardKey> keyboard,
        Input<MouseButton> mouseButtons,
        Input<GamepadButton> gamepadButtons,
        Axis<GamepadAxis> gamepadAxes,
        Mouse mouse)
    {
        var x = EvaluateBinding(binding.PositiveX, keyboard, mouseButtons, gamepadButtons, gamepadAxes, mouse) -
                EvaluateBinding(binding.NegativeX, keyboard, mouseButtons, gamepadButtons, gamepadAxes, mouse);

        var y = EvaluateBinding(binding.PositiveY, keyboard, mouseButtons, gamepadButtons, gamepadAxes, mouse) -
                EvaluateBinding(binding.NegativeY, keyboard, mouseButtons, gamepadButtons, gamepadAxes, mouse);

        return new Vector2(x, y);
    }

    /// <summary>
    /// Evaluates a single input binding and calculates its value.
    /// </summary>
    /// <param name="binding">The input binding to evaluate.</param>
    /// <param name="keyboard">The keyboard input resource.</param>
    /// <param name="mouseButtons">The mouse button input resource.</param>
    /// <param name="gamepadButtons">The gamepad button input resource.</param>
    /// <param name="gamepadAxes">The gamepad axis input resource.</param>
    /// <param name="mouse">The mouse input resource.</param>
    /// <returns>The calculated value of the binding.</returns>
    private static float EvaluateBinding(
        InputBinding binding,
        Input<KeyboardKey> keyboard,
        Input<MouseButton> mouseButtons,
        Input<GamepadButton> gamepadButtons,
        Axis<GamepadAxis> gamepadAxes,
        Mouse mouse)
    {
        return binding switch
        {
            KeyboardBinding kb => keyboard.Pressed(kb.Key) ? 1f : 0f,
            MouseButtonBinding mb => mouseButtons.Pressed(mb.Button) ? 1f : 0f,
            GamepadButtonBinding gb => gamepadButtons.Pressed(gb.Button) ? 1f : 0f,
            GamepadAxisBinding ga => gamepadAxes.Get(ga.Axis),
            MouseAxisBinding ma => ma.Axis switch
            {
                MouseAxis.DeltaX => mouse.Delta.X,
                MouseAxis.DeltaY => mouse.Delta.Y,
                MouseAxis.WheelX => mouse.Wheel.X,
                MouseAxis.WheelY => mouse.Wheel.Y,
                _ => 0f
            },
            _ => 0f
        };
    }
}
