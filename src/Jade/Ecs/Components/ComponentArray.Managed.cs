// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

public sealed class ComponentArrayManaged<T> : ComponentArray<T>
{
    public T[] Array { get; }

    public override int Capacity { get; }

    public ComponentArrayManaged(int capacity)
    {
        Capacity = capacity;
        Array = ArrayPool<T>.Shared.Rent(capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Span<T> GetSpan(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return new Span<T>(Array, 0, Math.Min(count, Capacity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ref T GetRef(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        return ref Array[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SetRef(int index, in T value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        Array[index] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Move(int fromIndex, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, Capacity);

        if (fromIndex == toIndex)
            return;

        Array[toIndex] = Array[fromIndex];
        Array[fromIndex] = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Reset()
    {
        Array.AsSpan(0, Capacity).Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ReleaseUnmanagedResources()
    {
        ArrayPool<T>.Shared.Return(Array, clearArray: true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        toArray.SetRef(toIndex, Array[fromIndex]);
        Array[fromIndex] = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void CopyTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        switch (toArray)
        {
            case ComponentArrayManaged<T> managedArray:
                managedArray.Array[toIndex] = Array[fromIndex];
                break;

            case ComponentArrayUnmanaged<T> unmanagedArray:
                var sourceSpan = MemoryMarshal.CreateSpan(ref Array[fromIndex], 1);
                var destSpan = MemoryMarshal.CreateSpan(ref unmanagedArray.GetRef(toIndex), 1);
                sourceSpan.CopyTo(destSpan);
                break;

            default:
                toArray.SetRef(toIndex, Array[fromIndex]);
                break;
        }
    }
}
