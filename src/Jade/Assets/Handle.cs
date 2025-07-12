// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jade.Assets;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct Handle<T>(uint Id) : IComparable<Handle<T>>
    where T : class
{
    public static readonly Handle<T> Invalid = new(0);

    public bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Id is not 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Handle<T> other)
    {
        return Id.CompareTo(other.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator bool(Handle<T> handle)
    {
        return handle.IsValid;
    }

    public override string ToString()
    {
        return $"Handle<{typeof(T).Name}>({Id})";
    }
}
