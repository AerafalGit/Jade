// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Archetypes;

public sealed unsafe class ArchetypeChunk : IDisposable
{
    private const nuint Alignment = 64;

    private const int Capacity = 16 * 1024;

    private readonly Entity* _entities;
    private readonly Dictionary<ComponentId, ComponentArray> _componentArrays;

    public int Count { get; private set; }

    public bool IsFull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Count >= Capacity;
    }

    public ReadOnlySpan<Entity> Entities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(_entities, Count);
    }

    public ArchetypeChunk(IEnumerable<ComponentId> ids)
    {
        _entities = (Entity*)NativeMemory.AlignedAlloc(Capacity * (nuint)sizeof(Entity), Alignment);
        NativeMemory.Clear(_entities, Capacity * (nuint)sizeof(Entity));

        _componentArrays = [];

        foreach (var id in ids)
        {
            var metadata = ComponentRegistry.GetMetadata(id);

            if (metadata.Size > 0)
                _componentArrays[id] = metadata.ComponentArrayFactory(Capacity);
        }
    }

    ~ArchetypeChunk()
    {
        ReleaseUnmanagedResources();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<Entity>.Enumerator GetEnumerator()
    {
        return Entities.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        Count = 0;

        NativeMemory.Clear(_entities, Capacity * (nuint)sizeof(Entity));

        foreach (var array in _componentArrays.Values)
            array.Reset();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add(Entity entity)
    {
        var index = Count++;
        _entities[index] = entity;
        return index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Remove(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

        var lastIndex = --Count;

        if (index == lastIndex)
            return Entity.Null;

        var movedEntity = _entities[lastIndex];
        _entities[index] = movedEntity;

        foreach (var array in _componentArrays.Values)
            array.Move(lastIndex, index);

        return movedEntity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>()
    {
        return GetArray<T>().GetSpan(Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>(int count)
    {
        return GetArray<T>().GetSpan(Math.Min(count, Count));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray<T> GetArray<T>()
    {
        return (ComponentArray<T>)_componentArrays[Component<T>.Metadata.Id];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray GetArray(ComponentId id)
    {
        return _componentArrays[id];
    }

    private void ReleaseUnmanagedResources()
    {
        NativeMemory.AlignedFree(_entities);

        foreach (var array in _componentArrays.Values)
            array.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}

