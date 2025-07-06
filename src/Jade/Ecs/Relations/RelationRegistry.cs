// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;

namespace Jade.Ecs.Relations;

/// <summary>
/// Provides a registry for managing unique identifiers for relation types in the ECS (Entity Component System).
/// This class ensures that each relation type is assigned a unique integer identifier.
/// </summary>
public static class RelationRegistry
{
    private static readonly Dictionary<Type, int> s_typeToId;
    private static readonly Lock s_lock;

    private static int s_nextId;

    /// <summary>
    /// Initializes the static members of the <see cref="RelationRegistry"/> class.
    /// </summary>
    static RelationRegistry()
    {
        s_typeToId = [];
        s_lock = new Lock();
    }

    /// <summary>
    /// Retrieves the unique identifier for the specified relation type.
    /// If the relation type does not already have an identifier, a new one is generated.
    /// </summary>
    /// <typeparam name="T">The type of the relation, which must implement <see cref="IRelation"/> and be a value type.</typeparam>
    /// <returns>The unique identifier for the relation type <typeparamref name="T"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetId<T>()
        where T : struct, IRelation
    {
        var type = typeof(T);

        return s_typeToId.TryGetValue(type, out var id)
            ? id
            : GetIdSlow(type);
    }

    /// <summary>
    /// Retrieves the unique identifier for a relation type in a thread-safe manner.
    /// This method is called when the identifier is not already cached.
    /// </summary>
    /// <param name="type">The type of the relation.</param>
    /// <returns>The unique identifier for the specified relation type.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int GetIdSlow(Type type)
    {
        lock (s_lock)
        {
            if (s_typeToId.TryGetValue(type, out var id))
                return id;

            id = Interlocked.Increment(ref s_nextId) - 1;
            s_typeToId[type] = id;
            return id;
        }
    }
}
