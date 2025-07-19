// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents a managed array for storing components of type <typeparamref name="T"/> in the ECS (Entity Component System).
/// Provides methods for accessing, moving, copying, and resetting components.
/// </summary>
/// <typeparam name="T">The type of the components stored in the array.</typeparam>
public sealed class ComponentArrayManaged<T> : ComponentArray<T>
{
    private readonly T[] _array;

    private bool _disposed;

    /// <summary>
    /// Gets the capacity of the component array.
    /// </summary>
    public override int Capacity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentArrayManaged{T}"/> class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of components the array can hold.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the capacity is negative or zero.</exception>
    public ComponentArrayManaged(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        Capacity = capacity;
        _array = ArrayPool<T>.Shared.Rent(capacity);
        _array.AsSpan(0, capacity).Clear();
    }

    /// <summary>
    /// Gets a span of components up to the specified count.
    /// </summary>
    /// <param name="count">The number of components to include in the span.</param>
    /// <returns>A span of components.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is negative.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Span<T> GetSpan(int count)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return new Span<T>(_array, 0, Math.Min(count, Capacity));
    }

    /// <summary>
    /// Gets a reference to the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <returns>A reference to the component at the specified index.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative or exceeds the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ref T GetRef(int index)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        return ref _array[index];
    }

    /// <summary>
    /// Sets the value of the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative or exceeds the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SetRef(int index, in T value)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Capacity);
        _array[index] = value;
    }

    /// <summary>
    /// Moves a component from one index to another within the array.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toIndex">The target index to move the component to.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Move(int fromIndex, int toIndex)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, Capacity);

        if (fromIndex == toIndex)
            return;

        _array[toIndex] = _array[fromIndex];
        _array[fromIndex] = default!;
    }

    /// <summary>
    /// Resets the array, clearing all components.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Reset()
    {
        ThrowIfDisposed();
        _array.AsSpan(0, Capacity).Clear();
    }

    /// <summary>
    /// Releases unmanaged resources used by the array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ReleaseUnmanagedResources()
    {
        if (_disposed)
            return;

        _disposed = true;

        ArrayPool<T>.Shared.Return(_array, clearArray: true);
    }

    /// <summary>
    /// Moves a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toArray">The target typed array to move the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        toArray.SetRef(toIndex, _array[fromIndex]);
        _array[fromIndex] = default!;
    }

    /// <summary>
    /// Copies a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to copy.</param>
    /// <param name="toArray">The target typed array to copy the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the indices are negative or exceed the capacity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void CopyTo(int fromIndex, ComponentArray<T> toArray, int toIndex)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(fromIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fromIndex, Capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(toIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(toIndex, toArray.Capacity);

        switch (toArray)
        {
            case ComponentArrayManaged<T> managedArray:
                managedArray._array[toIndex] = _array[fromIndex];
                break;

            case ComponentArrayUnmanaged<T> unmanagedArray:
                var sourceSpan = MemoryMarshal.CreateSpan(ref _array[fromIndex], 1);
                var destSpan = MemoryMarshal.CreateSpan(ref unmanagedArray.GetRef(toIndex), 1);
                sourceSpan.CopyTo(destSpan);
                break;

            default:
                toArray.SetRef(toIndex, _array[fromIndex]);
                break;
        }
    }

    /// <summary>
    /// Throws an exception if the array has been disposed.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the array has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(ComponentArrayManaged<T>));
    }
}
