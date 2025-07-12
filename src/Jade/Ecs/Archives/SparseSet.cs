// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Archives;

internal sealed unsafe class SparseSet : IDisposable
{
    private const int DefaultCapacity = 128;

    private const nuint Alignment = 64;

    private readonly int _componentSize;
    private readonly nuint _componentAlignment;

    private uint* _sparse;
    private Entity* _dense;
    private byte* _components;

    private int _denseCapacity;
    private int _sparseCapacity;

    public int Count { get; private set; }

    public ReadOnlySpan<Entity> Entities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(_dense, Count);
    }

    public SparseSet(int componentSize, int componentAlignment, int capacity = DefaultCapacity)
    {
        _componentSize = componentSize;
        _componentAlignment = (nuint)componentAlignment;
        _denseCapacity = capacity;
        _sparseCapacity = capacity;

        _sparse = (uint*)NativeMemory.AlignedAlloc((nuint)(_sparseCapacity * sizeof(uint)), Alignment);
        NativeMemory.Clear(_sparse, (nuint)(_sparseCapacity * sizeof(uint)));

        _dense = (Entity*)NativeMemory.AlignedAlloc((nuint)(_denseCapacity * sizeof(Entity)), Alignment);
        NativeMemory.Clear(_dense, (nuint)(_denseCapacity * sizeof(Entity)));

        if (_componentSize > 0)
            _components = (byte*)NativeMemory.AlignedAlloc((nuint)(_denseCapacity * _componentSize), _componentAlignment);
    }

    ~SparseSet()
    {
        ReleaseUnmanagedResources();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan<T>()
        where T : unmanaged, IComponent
    {
        return _componentSize is 0
            ? []
            : new Span<T>((T*)_components, Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity)
    {
        if (entity.Id >= _sparseCapacity)
            return false;

        var denseIndex = _sparse[entity.Id];

        return denseIndex < Count && _dense[denseIndex].Equals(entity);
    }

    public void AddFromBytes(Entity entity, ReadOnlySpan<byte> data)
    {
        EnsureSparseCapacity(entity.Id + 1);

        if (Contains(entity))
        {
            data.CopyTo(new Span<byte>(&_components[_sparse[entity.Id] * _componentSize], _componentSize));
            return;
        }

        EnsureDenseCapacity(Count + 1);

        var newDenseIndex = Count++;
        _sparse[entity.Id] = (uint)newDenseIndex;
        _dense[newDenseIndex] = entity;

        if (_componentSize > 0)
            data.CopyTo(new Span<byte>(&_components[newDenseIndex * _componentSize], _componentSize));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add<T>(Entity entity, in T component) where T : unmanaged
    {
        EnsureSparseCapacity(entity.Id + 1);

        if (Contains(entity))
        {
            ref var current = ref Get<T>(entity);
            current = component;
            return ref current;
        }

        EnsureDenseCapacity(Count + 1);

        var denseIndex = Count++;

        _sparse[entity.Id] = (uint)denseIndex;
        _dense[denseIndex] = entity;

        if (_componentSize > 0)
        {
            ref var componentData = ref Unsafe.As<byte, T>(ref _components[denseIndex * _componentSize]);
            componentData = component;
            return ref componentData;
        }

        return ref Unsafe.NullRef<T>();
    }

    public void Remove(Entity entity)
    {
        if (!Contains(entity))
            return;

        var denseIndex = (int)_sparse[entity.Id];
        var lastDenseIndex = --Count;

        if (denseIndex != lastDenseIndex)
        {
            var lastEntity = _dense[lastDenseIndex];
            _dense[denseIndex] = lastEntity;

            if (_componentSize > 0)
            {
                var src = &_components[lastDenseIndex * _componentSize];
                var dst = &_components[denseIndex * _componentSize];
                Unsafe.CopyBlock(dst, src, (uint)_componentSize);
            }

            _sparse[lastEntity.Id] = (uint)denseIndex;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(Entity entity) where T : unmanaged
    {
        return ref Unsafe.As<byte, T>(ref _components[_sparse[entity.Id] * _componentSize]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> GetAsBytes(Entity entity)
    {
        return new ReadOnlySpan<byte>(&_components[_sparse[entity.Id] * _componentSize], _componentSize);
    }

    private void EnsureSparseCapacity(uint required)
    {
        if (required <= _sparseCapacity)
            return;

        var newCapacity = Math.Max(_sparseCapacity * 2, (int)required);
        var newSparse = (uint*)NativeMemory.AlignedRealloc(_sparse, (nuint)(newCapacity * sizeof(uint)), Alignment);

        NativeMemory.Clear(newSparse + _sparseCapacity, (nuint)((newCapacity - _sparseCapacity) * sizeof(uint)));

        _sparse = newSparse;
        _sparseCapacity = newCapacity;
    }

    private void EnsureDenseCapacity(int required)
    {
        if (required <= _denseCapacity)
            return;

        var newCapacity = Math.Max(_denseCapacity * 2, required);
        var newDense = (Entity*)NativeMemory.AlignedRealloc(_dense, (nuint)(newCapacity * sizeof(Entity)), Alignment);

        NativeMemory.Clear(newDense + _denseCapacity, (nuint)((newCapacity - _denseCapacity) * sizeof(Entity)));

        _dense = newDense;

        if (_componentSize > 0)
        {
            var newComponents = (byte*)NativeMemory.AlignedRealloc(_components, (nuint)(newCapacity * _componentSize), _componentAlignment);
            NativeMemory.Clear(_components + _denseCapacity * _componentSize, (nuint)((newCapacity - _denseCapacity) * _componentSize));
            _components = newComponents;
        }

        _denseCapacity = newCapacity;
    }

    private void ReleaseUnmanagedResources()
    {
        NativeMemory.AlignedFree(_sparse);
        NativeMemory.AlignedFree(_dense);

        if (_components is not null)
            NativeMemory.AlignedFree(_components);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}
