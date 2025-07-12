// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

[Flags]
public enum SystemStage
{
    None = 0,
    Startup = 1,
    PreUpdate = 2,
    Update = 4,
    PostUpdate = 8,
    FixedUpdate = 16,
    Render = 32,
    Cleanup = 64
}
