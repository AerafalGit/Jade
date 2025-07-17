// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

public sealed unsafe class ComponentArrayUnmanaged<T> : ComponentArray<T>
{
    public T* Ptr { get; }

    public int ComponentSize { get; }

    public override int Capacity { get; }

    public ComponentArrayUnmanaged(int size, int alignment, int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        if (alignment <= 0 || (alignment & (alignment - 1)) is not 0)
            throw new ArgumentException("Alignment must be a positive power of 2", nameof(alignment));

        Capacity = capacity;
        ComponentSize = size;
        Ptr = (T*)NativeMemory.AlignedAlloc((nuint)(ComponentSize * Capacity), (nuint)alignment);

        NativeMemory.Clear(Ptr, (nuint)(ComponentSize * Capacity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Span<T> GetSpan(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return new Span<T>(Ptr, Math.Min(count, Capacity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ref T GetRef(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        return ref Ptr[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SetRef(int index, in T value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        Ptr[index] = value;
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

        Ptr[toIndex] = Ptr[fromIndex];
        Ptr[fromIndex] = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Reset()
    {
        NativeMemory.Clear(Ptr, (nuint)(ComponentSize * Capacity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ReleaseUnmanagedResources()
    {
        NativeMemory.Free(Ptr);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        toArray.SetRef(toIndex, GetRef(fromIndex));
        Ptr[fromIndex] = default;
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
            case ComponentArrayUnmanaged<T> unmanagedArray:
                var source = (byte*)Ptr + fromIndex * ComponentSize;
                var dest = (byte*)unmanagedArray.Ptr + toIndex * ComponentSize;
                NativeMemory.Copy(source, dest, (nuint)ComponentSize);
                break;

            case ComponentArrayManaged<T> managedArray:
                var sourceSpan = MemoryMarshal.CreateSpan(ref GetRef(fromIndex), 1);
                var destSpan = MemoryMarshal.CreateSpan(ref managedArray.GetRef(toIndex), 1);
                sourceSpan.CopyTo(destSpan);
                break;

            default:
                toArray.SetRef(toIndex, GetRef(fromIndex));
                break;
        }
    }
}
