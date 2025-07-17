// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

internal static unsafe class ComponentRegistry
{
    private static readonly Dictionary<Type, ComponentMetadata> s_metadataByType;
    private static readonly Dictionary<ComponentId, ComponentMetadata> s_metadataById;
    private static readonly Lock s_lock;

    private static int s_nextId;

    static ComponentRegistry()
    {
        s_metadataByType = [];
        s_metadataById = [];
        s_lock = new Lock();
        s_nextId = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMetadata GetMetadata<T>()
    {
        var type = typeof(T);

        return s_metadataByType.GetValueOrDefault(type, RegisterSlow<T>(type));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentMetadata GetMetadata(ComponentId id)
    {
        lock (s_lock)
            return s_metadataById[id];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ComponentId> GetAllComponentIds()
    {
        lock (s_lock)
        {
            return s_metadataById.Keys;
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ComponentMetadata RegisterSlow<T>(Type type)
    {
        lock (s_lock)
        {
            if (s_metadataByType.TryGetValue(type, out var metadata))
                return metadata;

            var id = new ComponentId(Interlocked.Increment(ref s_nextId));

            var isBlittable = !RuntimeHelpers.IsReferenceOrContainsReferences<T>();

            var size = isBlittable
                ? sizeof(T)
                : IntPtr.Size;

            var alignment = isBlittable
                ? CalculateAlignment<T>()
                : size;

            Func<int, ComponentArray<T>> factory = isBlittable
                ? capacity => new ComponentArrayUnmanaged<T>(size, alignment, capacity)
                : capacity => new ComponentArrayManaged<T>(capacity);

            metadata = new ComponentMetadata(id, size, alignment, isBlittable, factory);

            s_metadataByType[type] = metadata;
            s_metadataById[id] = metadata;

            return metadata;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CalculateAlignment<T>()
    {
        var naturalAlignment = sizeof(ComponentAlignment<T>) - sizeof(T);

        if (naturalAlignment <= 0)
        {
            var size = sizeof(T);

            return size switch
            {
                1 => 1,
                2 => 2,
                <= 4 => 4,
                <= 8 => 8,
                _ => 16
            };
        }

        if ((naturalAlignment & (naturalAlignment - 1)) is not 0)
        {
            var powerOf2 = 1;

            while (powerOf2 < naturalAlignment)
                powerOf2 <<= 1;

            return Math.Min(powerOf2, 16);
        }

        return Math.Min(naturalAlignment, 16);
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct ComponentAlignment<T>
    {
        public readonly byte Padding;

        public readonly T Component;
    }
}
