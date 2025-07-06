// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Events;
using Jade.Ecs.Systems;
using Jade.Ecs.Systems.Attributes;
using Jade.Input.Events;

namespace Jade.Input;

/// <summary>
/// Represents the input system responsible for processing input events and updating input states.
/// </summary>
[RunAfter<InputEventSystem>]
public sealed class InputSystem : SystemBase
{
    /// <summary>
    /// Processes input events before the main update loop.
    /// </summary>
    public override void PreUpdate()
    {
        var keyboard = GetResource<Input<KeyboardKey>>();
        var mouseButtons = GetResource<Input<MouseButton>>();
        var gamepadButtons = GetResource<Input<GamepadButton>>();
        var gamepadAxes = GetResource<Axis<GamepadAxis>>();
        var mouse = GetResource<Mouse>();
        var gamepads = GetResource<Gamepads>();

        var keyboardEvents = GetResource<EventReader<KeyboardInputEvent>>();
        var mouseButtonEvents = GetResource<EventReader<MouseButtonInputEvent>>();
        var mouseMotionEvents = GetResource<EventReader<MouseMotionEvent>>();
        var mouseWheelEvents = GetResource<EventReader<MouseWheelEvent>>();
        var gamepadButtonEvents = GetResource<EventReader<GamepadButtonInputEvent>>();
        var gamepadAxisEvents = GetResource<EventReader<GamepadAxisEvent>>();
        var gamepadConnectedEvents = GetResource<EventReader<GamepadConnectedEvent>>();
        var gamepadDisconnectedEvents = GetResource<EventReader<GamepadDisconnectedEvent>>();

        foreach (var evt in keyboardEvents)
        {
            switch (evt.State)
            {
                case ButtonState.Pressed:
                    keyboard.Press(evt.Key);
                    break;
                case ButtonState.Released:
                    keyboard.Release(evt.Key);
                    break;
            }
        }

        foreach (var evt in mouseButtonEvents)
        {
            switch (evt.State)
            {
                case ButtonState.Pressed:
                    mouseButtons.Press(evt.Button);
                    break;
                case ButtonState.Released:
                    mouseButtons.Release(evt.Button);
                    break;
            }
        }

        foreach (var evt in mouseMotionEvents)
            mouse.Delta += evt.Delta;

        foreach (var evt in mouseWheelEvents)
            mouse.Wheel += evt.Delta;

        foreach (var evt in gamepadConnectedEvents)
            gamepads.Connect(evt.GamepadId, evt.GamepadName);

        foreach (var evt in gamepadDisconnectedEvents)
            gamepads.Disconnect(evt.GamepadId);

        foreach (var evt in gamepadButtonEvents)
        {
            switch (evt.State)
            {
                case ButtonState.Pressed:
                    gamepadButtons.Press(evt.Button);
                    break;
                case ButtonState.Released:
                    gamepadButtons.Release(evt.Button);
                    break;
            }
        }

        foreach (var evt in gamepadAxisEvents)
            gamepadAxes.Set(evt.Axis, evt.Value);
    }

    /// <summary>
    /// Cleans up input states after the main update loop.
    /// </summary>
    public override void PostUpdate()
    {
        var keyboard = GetResource<Input<KeyboardKey>>();
        var mouseButtons = GetResource<Input<MouseButton>>();
        var gamepadButtons = GetResource<Input<GamepadButton>>();
        var mouse = GetResource<Mouse>();
        var gamepads = GetResource<Gamepads>();

        keyboard.ClearJustChanged();
        mouseButtons.ClearJustChanged();
        gamepadButtons.ClearJustChanged();
        mouse.ClearDeltas();
        gamepads.ClearJustChanged();
    }
}
