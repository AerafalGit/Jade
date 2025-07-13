// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Numerics;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Benchmarks.Components;

public struct Transform : IComponent
{
    public Matrix4x4 Matrix;

    public Transform(Matrix4x4 matrix)
    {
        Matrix = matrix;
    }
}
