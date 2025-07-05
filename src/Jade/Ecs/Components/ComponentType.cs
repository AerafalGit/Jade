// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Components;

/// <summary>
/// Represents the type of a component in the ECS (Entity Component System).
/// This type includes the component's unique identifier, its index within an archetype, and its size in bytes.
/// </summary>
/// <param name="Id">The unique identifier of the component.</param>
/// <param name="IndexInArchetype">The index of the component within an archetype.</param>
/// <param name="Size">The size of the component in bytes.</param>
public readonly record struct ComponentType(int Id, int IndexInArchetype, int Size)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentType"/> record struct using metadata and an archetype index.
    /// </summary>
    /// <param name="metadata">The metadata of the component, including its ID and size.</param>
    /// <param name="indexInArchetype">The index of the component within an archetype.</param>
    public ComponentType(ComponentMetadata metadata, int indexInArchetype)
        : this(metadata.Id, indexInArchetype, metadata.Size)
    {
    }
}
