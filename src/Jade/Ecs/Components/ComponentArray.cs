// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Components;

/// <summary>
/// Represents an abstract base class for managing arrays of components in the ECS (Entity Component System).
/// Provides methods for moving, copying, and resetting components.
/// </summary>
public abstract class ComponentArray : IDisposable
{
    /// <summary>
    /// Gets the capacity of the component array.
    /// </summary>
    public abstract int Capacity { get; }

    /// <summary>
    /// Finalizer to release unmanaged resources when the object is garbage collected.
    /// </summary>
    ~ComponentArray()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>
    /// Moves a component from one index to another within the array.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toIndex">The target index to move the component to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Move(int fromIndex, int toIndex);

    /// <summary>
    /// Moves a component from this array to another array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toArray">The target array to move the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void MoveTo(int fromIndex, ComponentArray toArray, int toIndex);

    /// <summary>
    /// Copies a component from this array to another array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to copy.</param>
    /// <param name="toArray">The target array to copy the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void CopyTo(int fromIndex, ComponentArray toArray, int toIndex);

    /// <summary>
    /// Resets the array, clearing all components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Reset();

    /// <summary>
    /// Releases unmanaged resources used by the array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void ReleaseUnmanagedResources();

    /// <summary>
    /// Disposes the array, releasing unmanaged resources and suppressing finalization.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// Represents a generic component array for managing components of type <typeparamref name="T"/>.
/// Provides methods for accessing, moving, and copying components.
/// </summary>
/// <typeparam name="T">The type of the components stored in the array.</typeparam>
public abstract class ComponentArray<T> : ComponentArray
{
    /// <summary>
    /// Gets or sets the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <returns>A reference to the component at the specified index.</returns>
    public ref T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref GetRef(index);
    }

    /// <summary>
    /// Gets a span of components starting from the specified count.
    /// </summary>
    /// <param name="count">The number of components to include in the span.</param>
    /// <returns>A span of components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Span<T> GetSpan(int count);

    /// <summary>
    /// Gets a reference to the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <returns>A reference to the component at the specified index.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract ref T GetRef(int index);

    /// <summary>
    /// Sets the value of the component at the specified index.
    /// </summary>
    /// <param name="index">The index of the component.</param>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void SetRef(int index, in T value);

    /// <summary>
    /// Moves a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toArray">The target typed array to move the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void MoveTo(int fromIndex, ComponentArray<T> toArray, int toIndex);

    /// <summary>
    /// Copies a component from this array to another typed array at the specified index.
    /// </summary>
    /// <param name="fromIndex">The index of the component to copy.</param>
    /// <param name="toArray">The target typed array to copy the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void CopyTo(int fromIndex, ComponentArray<T> toArray, int toIndex);

    /// <summary>
    /// Moves a component from this array to another array at the specified index.
    /// Throws an exception if the target array is not of the same type.
    /// </summary>
    /// <param name="fromIndex">The index of the component to move.</param>
    /// <param name="toArray">The target array to move the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="InvalidCastException">
    /// Thrown if the target array is not of type <see cref="ComponentArray{T}"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override void MoveTo(int fromIndex, ComponentArray toArray, int toIndex)
    {
        if (toArray is not ComponentArray<T> typedArray)
            throw new InvalidCastException($"Cannot cast {toArray.GetType().Name} to {typeof(ComponentArray<T>).Name}.");

        MoveTo(fromIndex, typedArray, toIndex);
    }

    /// <summary>
    /// Copies a component from this array to another array at the specified index.
    /// Throws an exception if the target array is not of the same type.
    /// </summary>
    /// <param name="fromIndex">The index of the component to copy.</param>
    /// <param name="toArray">The target array to copy the component to.</param>
    /// <param name="toIndex">The target index in the target array.</param>
    /// <exception cref="InvalidCastException">
    /// Thrown if the target array is not of type <see cref="ComponentArray{T}"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override void CopyTo(int fromIndex, ComponentArray toArray, int toIndex)
    {
        if (toArray is not ComponentArray<T> typedArray)
            throw new InvalidCastException($"Cannot cast {toArray.GetType().Name} to {typeof(ComponentArray<T>).Name}.");

        CopyTo(fromIndex, typedArray, toIndex);
    }
}
