// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;

namespace Jade.Ecs.Relations;

internal static class RelationProperty
{
    public static readonly ComponentId ChildOf = new(-1);

    public static readonly ComponentId ParentOf = new(-2);

    public static readonly ComponentId InstanceOf = new(-3);

    public static readonly ComponentId DependsOn = new(-4);
}
