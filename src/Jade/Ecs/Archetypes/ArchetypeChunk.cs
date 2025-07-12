// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Components;

namespace Jade.Ecs.Archetypes;

[StructLayout(LayoutKind.Sequential)]
internal sealed unsafe class ArchetypeChunk : IDisposable
{
    private const int Capacity = ComponentMask.MaxComponents * 2;

    private const nuint Alignment = 64;

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
        _entities = (Entity*)NativeMemory.AlignedAlloc((nuint)(Capacity * sizeof(Entity)), Alignment);
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
    public int Add(Entity entity)
    {
        var index = Count++;
        _entities[index] = entity;
        return index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Remove(int index)
    {
        var lastIndex = --Count;
        var removedEntity = _entities[index];

        if (index == lastIndex)
            return removedEntity;

        var movedEntity = _entities[lastIndex];

        _entities[index] = movedEntity;

        foreach (var array in _componentArrays.Values)
            array.Move(lastIndex, index);

        return movedEntity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T* GetUnsafePtr<T>()
        where T : unmanaged, IComponent
    {
        return GetArray<T>().GetUnsafePtr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>()
        where T : unmanaged, IComponent
    {
        return GetArray<T>().GetSpan(Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray<T> GetArray<T>()
        where T : unmanaged, IComponent
    {
        return (ComponentArray<T>)_componentArrays[Component<T>.Metadata.Id];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentArray GetArray(ComponentId id)
    {
        return _componentArrays[id];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SwapAllComponents(int index, void** ptr)
    {
        foreach (var (id, array) in _componentArrays)
            array.Swap(index, ptr[id]);
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
