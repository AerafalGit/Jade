// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Queries;

namespace Jade.Ecs;

public sealed partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query Query()
    {
        return new Query(this);
    }
}
