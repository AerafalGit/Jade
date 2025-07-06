// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Systems;
using Jade.Input.Events;
using Silk.NET.Input;

namespace Jade.Input;

/// <summary>
/// Represents the system responsible for handling input events from various input devices.
/// </summary>
public sealed class InputEventSystem : SystemBase
{
    /// <summary>
    /// Initializes the input event system and sets up event publishers for input devices.
    /// </summary>
    public override void Startup()
    {
        var inputContext = GetResource<IInputContext>();
        var keyboardEvents = World.GetPublisher<KeyboardInputEvent>();
        var mouseButtonEvents = World.GetPublisher<MouseButtonInputEvent>();
        var mouseMotionEvents = World.GetPublisher<MouseMotionEvent>();
        var mouseWheelEvents = World.GetPublisher<MouseWheelEvent>();
        var gamepadButtonEvents = World.GetPublisher<GamepadButtonInputEvent>();
        var gamepadConnectedEvents = World.GetPublisher<GamepadConnectedEvent>();
        var gamepadDisconnectedEvents = World.GetPublisher<GamepadDisconnectedEvent>();

        foreach (var keyboard in inputContext.Keyboards)
        {
            keyboard.KeyDown += (_, key, _) => keyboardEvents.Write(new KeyboardInputEvent((KeyboardKey)key, ButtonState.Pressed));
            keyboard.KeyUp += (_, key, _) => keyboardEvents.Write(new KeyboardInputEvent((KeyboardKey)key, ButtonState.Released));
        }

        foreach (var mice in inputContext.Mice)
        {
            mice.MouseDown += (_, button) => mouseButtonEvents.Write(new MouseButtonInputEvent((MouseButton)button, ButtonState.Pressed));
            mice.MouseUp += (_, button) => mouseButtonEvents.Write(new MouseButtonInputEvent((MouseButton)button, ButtonState.Released));
            mice.MouseMove += (_, delta) => mouseMotionEvents.Write(new MouseMotionEvent(delta));
            mice.Scroll += (_, wheel) => mouseWheelEvents.Write(new MouseWheelEvent(new Vector2(wheel.X, wheel.Y)));
        }

        inputContext.ConnectionChanged += (device, connected) =>
        {
            if (device is not IGamepad gamepad)
                return;

            if (connected)
                gamepadConnectedEvents.Write(new GamepadConnectedEvent((uint)gamepad.Index, gamepad.Name));
            else
                gamepadDisconnectedEvents.Write(new GamepadDisconnectedEvent((uint)gamepad.Index));
        };

        foreach (var gamepad in inputContext.Gamepads)
        {
            gamepad.ButtonDown += (_, button) => gamepadButtonEvents.Write(new GamepadButtonInputEvent(new Gamepad((uint)gamepad.Index, gamepad.Name), (GamepadButton)button.Name, ButtonState.Pressed));
            gamepad.ButtonUp += (_, button) => gamepadButtonEvents.Write(new GamepadButtonInputEvent(new Gamepad((uint)gamepad.Index, gamepad.Name), (GamepadButton)button.Name, ButtonState.Released));
        }
    }

    /// <summary>
    /// Updates the input event system by processing gamepad axis input events.
    /// </summary>
    public override void Update()
    {
        var inputContext = GetResource<IInputContext>();
        var gamepadAxisEvents = World.GetPublisher<GamepadAxisEvent>();

        foreach (var gamepad in inputContext.Gamepads)
        {
            if (!gamepad.IsConnected)
                continue;

            var gamepadInfo = new Gamepad((uint)gamepad.Index, gamepad.Name);

            if (gamepad.Thumbsticks.Count > 0)
            {
                var leftStick = gamepad.Thumbsticks[0];
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.LeftStickX, leftStick.X));
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.LeftStickY, leftStick.Y));
            }

            if (gamepad.Thumbsticks.Count > 1)
            {
                var rightStick = gamepad.Thumbsticks[1];
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.RightStickX, rightStick.X));
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.RightStickY, rightStick.Y));
            }

            if (gamepad.Triggers.Count > 0)
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.LeftTrigger, gamepad.Triggers[0].Position));

            if (gamepad.Triggers.Count > 1)
                gamepadAxisEvents.Write(new GamepadAxisEvent(gamepadInfo, GamepadAxis.RightTrigger, gamepad.Triggers[1].Position));
        }
    }
}
