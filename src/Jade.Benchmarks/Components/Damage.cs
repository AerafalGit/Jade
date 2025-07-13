// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions.Components;

namespace Jade.Benchmarks.Components;

public struct Damage : IComponent
{
    public float Value;

    public Damage(float value)
    {
        Value = value;
    }
}
