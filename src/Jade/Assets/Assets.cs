// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Assets;

public sealed class Assets<T>
    where T : class
{
    public Handle<T> GetOrAdd(T asset)
    {
        return new Handle<T>(0);
    }

    public T? Get(Handle<T> handle)
    {
        return null;
    }
}
