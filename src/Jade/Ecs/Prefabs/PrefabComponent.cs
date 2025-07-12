// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Assets;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Prefabs;

public struct PrefabComponent : IComponent
{
    public Handle<string> NameId;

    public bool IsTemplate;

    public PrefabComponent(Handle<string> nameId, bool isTemplate)
    {
        NameId = nameId;
        IsTemplate = isTemplate;
    }
}
