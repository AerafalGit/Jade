// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Components;

internal static unsafe class ComponentRegistry
{
    private static readonly Dictionary<Type, ComponentMetadata> s_metadataByType;
    private static readonly Dictionary<ComponentId, ComponentMetadata> s_metadataById;
    private static readonly Lock s_registrationLock;

    private static int s_nextId;

    static ComponentRegistry()
    {
        s_metadataByType = [];
        s_metadataById = [];
        s_registrationLock = new Lock();
        s_nextId = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMetadata GetMetadata<T>()
        where T : unmanaged, IComponent
    {
        var type = typeof(T);

        return s_metadataByType.TryGetValue(type, out var metadata)
            ? metadata
            : RegisterSlow<T>(type);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMetadata GetMetadata(ComponentId id)
    {
        return s_metadataById[id];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ComponentId> GetAllComponentIds()
    {
        return s_metadataById.Keys;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ComponentMetadata RegisterSlow<T>(Type type)
        where T : unmanaged, IComponent
    {
        using (s_registrationLock.EnterScope())
        {
            if (s_metadataByType.TryGetValue(type, out var metadata))
                return metadata;

            var id = new ComponentId(Interlocked.Increment(ref s_nextId));

            var size = sizeof(T);

            var alignment = sizeof(ComponentAlignment<T>) - size;

            metadata = new ComponentMetadata(id, size, alignment, capacity => new ComponentArray<T>(size, alignment, capacity));

            s_metadataByType[type] = metadata;
            s_metadataById[id] = metadata;

            return metadata;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct ComponentAlignment<T>
        where T : unmanaged, IComponent
    {
        public readonly byte Padding;

        public readonly T Component;
    }
}
