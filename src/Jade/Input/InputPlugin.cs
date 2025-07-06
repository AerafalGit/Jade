// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs;
using Jade.Ecs.Plugins;
using Jade.Ecs.Systems;

namespace Jade.Input;

/// <summary>
/// Represents the input plugin responsible for setting up input-related resources and systems in the game world.
/// </summary>
public sealed class InputPlugin : PluginBase
{
    /// <summary>
    /// Configures the game world by adding input resources and systems.
    /// </summary>
    /// <param name="world">The game world to configure.</param>
    public override void Build(World world)
    {
        world.AddResource<Input<KeyboardKey>>();
        world.AddResource<Input<MouseButton>>();
        world.AddResource<Input<GamepadButton>>();
        world.AddResource<Axis<GamepadAxis>>();
        world.AddResource<Mouse>();
        world.AddResource<Gamepads>();

        world.AddSystem<InputSystem>(SystemStage.PreUpdate | SystemStage.PostUpdate);
        world.AddSystem<InputEventSystem>(SystemStage.Startup | SystemStage.Update);
    }
}
