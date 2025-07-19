// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents an unmanaged array for storing components of type <typeparamref name="T"/> in the ECS (Entity Component System).
/// Provides methods for accessing, moving, copying, and resetting components.
/// </summary>
/// <typeparam name="T">The type of the components stored in the array.</typeparam>
public sealed unsafe class ComponentArrayUnmanaged<T> : ComponentArray<T>
{
    private readonly T* _ptr;
    private readonly int _componentSize;

    /// <summary>
    /// Gets the capacity of the component array.
    /// </summary>
    public override int Capacity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentArrayUnmanaged{T}"/> class.
    /// Allocates unmanaged memory for storing components.
    /// </summary>
    /// <param name="size">The size of each component in bytes.</param>
    /// <param name="alignment">The memory alignment for the array.</param>
    /// <param name="capacity">The maximum number of components the array can hold.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if size or capacity is negative or zero.</exception>
    /// <exception cref="ArgumentException">Thrown if alignment is not a positive power of 2.</exception>
    public ComponentArrayUnmanaged(int size, int alignment, int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        if (alignment <= 0 || (alignment & (alignment - 1)) is not 0)
            throw new ArgumentException("Alignment must be a positive power of 2.", nameof(alignment));

        Capacity = capacity;

        _componentSize = size;
        _ptr = (T*)NativeMemory.AlignedAlloc((nuint)(_componentSize * Capacity), (nuint)alignment);

        NativeMemory.Clear(_ptr, (nuint)(_componentSize * Capacity));
    }

    /// <summary>
    /// Gets a span of components up to the specified count.
    /// </summary>
    /// <param name="count">The number of components to include in the span.</param>
    /// <returns>A span of components.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is negative.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Span<T> GetSpan(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return new Span<T>(_ptr, Math.Min(count, Capacity));
    }

    /// <summary>
    /// Gets a reference to the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <returns>A reference to the component at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative or exceeds the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ref T GetRef(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        return ref _ptr[index];
    }

    /// <summary>
    /// Sets the value of the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative or exceeds the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SetRef(int index, in T value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        _ptr[index] = value;
    }

    /// <summary>
    /// Moves a component from one index to another within the array.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toIndex">The target index to move the component to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Move(int fromIndex, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, Capacity);

        if (fromIndex == toIndex)
            return;

        _ptr[toIndex] = _ptr[fromIndex];
        _ptr[fromIndex] = default;
    }

    /// <summary>
    /// Resets the array, clearing all components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Reset()
    {
        NativeMemory.Clear(_ptr, (nuint)(_componentSize * Capacity));
    }

    /// <summary>
    /// Releases unmanaged resources used by the array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ReleaseUnmanagedResources()
    {
        NativeMemory.Free(_ptr);
    }

    /// <summary>
    /// Moves a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toArray">The target typed array to move the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        toArray.SetRef(toIndex, GetRef(fromIndex));
        _ptr[fromIndex] = default;
    }

    /// <summary>
    /// Copies a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to copy.</param>
    /// <param name="toArray">The target typed array to copy the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
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
                var source = (byte*)_ptr + fromIndex * _componentSize;
                var dest = (byte*)unmanagedArray._ptr + toIndex * _componentSize;
                NativeMemory.Copy(source, dest, (nuint)_componentSize);
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
