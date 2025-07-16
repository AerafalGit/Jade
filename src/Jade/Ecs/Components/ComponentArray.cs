// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Components;

public abstract class ComponentArray : IDisposable
{
    public abstract int Capacity { get; }

    ~ComponentArray()
    {
        ReleaseUnmanagedResources();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Move(int fromIndex, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void MoveTo(int fromIndex, ComponentArray toArray, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void CopyTo(int fromIndex, ComponentArray toArray, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Reset();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void ReleaseUnmanagedResources();

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}

public abstract class ComponentArray<T> : ComponentArray
{
    public ref T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref GetRef(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Span<T> GetSpan(int count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract ref T GetRef(int index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void SetRef(int index, in T value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override void MoveTo(int fromIndex, ComponentArray toArray, int toIndex)
    {
        if (toArray is not ComponentArray<T> typedArray)
            throw new InvalidCastException($"Cannot cast {toArray.GetType().Name} to {typeof(ComponentArray<T>).Name}.");

        MoveTo(fromIndex, typedArray, toIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override void CopyTo(int fromIndex, ComponentArray toArray, int toIndex)
    {
        if (toArray is not ComponentArray<T> typedArray)
            throw new InvalidCastException($"Cannot cast {toArray.GetType().Name} to {typeof(ComponentArray<T>).Name}.");

        CopyTo(fromIndex, typedArray, toIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void CopyTo(int fromIndex, ComponentArray<T> toArray, int toIndex);
}
