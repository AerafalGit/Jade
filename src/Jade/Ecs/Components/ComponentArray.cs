// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Jade.Ecs.Abstractions.Components;

namespace Jade.Ecs.Components;

[StructLayout(LayoutKind.Sequential)]
internal abstract unsafe class ComponentArray : IDisposable
{
    protected readonly int ComponentSize;
    protected readonly void* Ptr;

    public readonly int Capacity;

    protected ComponentArray(int size, int alignment, int capacity)
    {
        Capacity = capacity;
        ComponentSize = size;

        if (size > 0)
            Ptr = NativeMemory.AlignedAlloc((nuint)(capacity * size), (nuint)alignment);
    }

    ~ComponentArray()
    {
        ReleaseUnmanagedResources();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Move(int fromIndex, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void MoveTo(int fromIndex, ComponentArray toArray, int toIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract ReadOnlySpan<byte> GetAsBytes(int index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void SetFromBytes(int index, ReadOnlySpan<byte> data);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Swap(int index, void* ptr);

    private void ReleaseUnmanagedResources()
    {
        if (Ptr is not null)
            NativeMemory.AlignedFree(Ptr);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}

[StructLayout(LayoutKind.Sequential)]
internal sealed unsafe class ComponentArray<T> : ComponentArray
    where T : unmanaged, IComponent
{
    public ComponentArray(int size, int alignment, int capacity) : base(size, alignment, capacity)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T* GetUnsafePtr()
    {
        return (T*)Ptr;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetSpan(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, Capacity);

        return new Span<T>(GetUnsafePtr(), Math.Min(count, Capacity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);

        return ref GetUnsafePtr()[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, in T value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);

        GetUnsafePtr()[index] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Move(int fromIndex, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, Capacity);

        if(fromIndex != toIndex)
            Ptr[toIndex] = Ptr[fromIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void MoveTo(int fromIndex, ComponentArray toArray, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        if (toArray is ComponentArray<T> target)
            target.Set(toIndex, Get(fromIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ReadOnlySpan<byte> GetAsBytes(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);

        return new ReadOnlySpan<byte>(Unsafe.Add<T>(Ptr, index), ComponentSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SetFromBytes(int index, ReadOnlySpan<byte> data)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);

        data.CopyTo(new Span<byte>(Unsafe.Add<T>(Ptr, index), ComponentSize));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Swap(int index, void* ptr)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);

        Unsafe.CopyBlock(ptr, Unsafe.Add<T>(Ptr, index), (uint)ComponentSize);
    }
}
