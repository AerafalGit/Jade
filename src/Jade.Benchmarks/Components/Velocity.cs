// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Benchmarks.Components;

public struct Velocity : IComponent
{
    public Vector3 Value;

    public Velocity(Vector3 value)
    {
        Value = value;
    }
}
