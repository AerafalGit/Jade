// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Provides functionality for registering and retrieving metadata about ECS components.
/// </summary>
internal static class ComponentRegistry
{
    private static readonly Dictionary<Type, ComponentMetadata> s_metadataByType;
    private static readonly Dictionary<int, ComponentMetadata> s_metadataById;
    private static readonly Lock s_lock;

    private static int s_nextId;

    /// <summary>
    /// Static constructor that initializes the component registry.
    /// </summary>
    static ComponentRegistry()
    {
        s_metadataByType = [];
        s_metadataById = [];
        s_lock = new Lock();
    }

    /// <summary>
    /// Registers a component type and returns its unique identifier.
    /// If the component type is already registered, its existing ID is returned.
    /// </summary>
    /// <typeparam name="T">The type of the component to register.</typeparam>
    /// <returns>The unique identifier of the component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Register<T>()
        where T : struct, IComponent
    {
        var type = typeof(T);

        return s_metadataByType.TryGetValue(type, out var existing)
            ? existing.Id
            : RegisterSlow(type, CreateMetadata<T>);
    }

    /// <summary>
    /// Registers a component type in a thread-safe manner and returns its unique identifier.
    /// </summary>
    /// <param name="type">The type of the component to register.</param>
    /// <param name="metadataFactory">A factory function to create the component metadata.</param>
    /// <returns>The unique identifier of the component type.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int RegisterSlow(Type type, Func<int, ComponentMetadata> metadataFactory)
    {
        lock (s_lock)
        {
            if (s_metadataByType.TryGetValue(type, out var metadata))
                return metadata.Id;

            var id = Interlocked.Increment(ref s_nextId) - 1;

            metadata = metadataFactory(id);

            s_metadataByType[type] = metadata;
            s_metadataById[id] = metadata;

            return id;
        }
    }

    /// <summary>
    /// Retrieves the metadata for a component type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The metadata of the component type.</returns>
    public static ComponentMetadata GetMetadata<T>()
        where T : struct, IComponent
    {
        return GetMetadata(typeof(T));
    }

    /// <summary>
    /// Retrieves the metadata for a component type by its type.
    /// </summary>
    /// <param name="type">The type of the component.</param>
    /// <returns>The metadata of the component type.</returns>
    public static ComponentMetadata GetMetadata(Type type)
    {
        return s_metadataByType[type];
    }

    /// <summary>
    /// Retrieves the metadata for a component type by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the component type.</param>
    /// <returns>The metadata of the component type.</returns>
    public static ComponentMetadata GetMetadata(int id)
    {
        return s_metadataById[id];
    }

    /// <summary>
    /// Creates metadata for a component type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <param name="id">The unique identifier of the component type.</param>
    /// <returns>The metadata of the component type.</returns>
    private static ComponentMetadata CreateMetadata<T>(int id)
        where T : struct, IComponent
    {
        var type = typeof(T);
        var size = Unsafe.SizeOf<T>();
        var isBlittable = !RuntimeHelpers.IsReferenceOrContainsReferences<T>();

        return new ComponentMetadata(id, type, size, isBlittable);
    }
}
