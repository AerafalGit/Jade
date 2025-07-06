// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a query for entities in the ECS (Entity Component System) that match specific component criteria.
/// Provides methods for filtering entities and iterating over their components.
/// </summary>
/// <typeparam name="T1">The type of the first component to query, which must implement <see cref="IComponent"/> and be a value type.</typeparam>
public readonly ref struct Query<T1>
    where T1 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1> With<T>()
        where T : struct, IComponent
    {
        return new Query<T1>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
        where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex1)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's component.
    /// </summary>
    /// <param name="callback">The callback function to process each component.</param>
    public void ForEach(QueryCallback<T1> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its component.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its component.</param>
    public void ForEach(QueryCallbackWithEntity<T1> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS (Entity Component System) that match specific component criteria.
/// Provides methods for filtering entities and iterating over their components.
/// </summary>
/// <typeparam name="T1">The type of the first component to query, which must implement <see cref="IComponent"/> and be a value type.</typeparam>
/// <typeparam name="T2">The type of the second component to query, which must implement <see cref="IComponent"/> and be a value type.</typeparam>
public readonly ref struct Query<T1, T2>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2> With<T>()
        where T : struct, IComponent
    {
        return new Query<T1, T2>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
        where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex1)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each component.</param>
    public void ForEach(QueryCallback<T1, T2> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
/// <typeparam name="T6">The type of the sixth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5, T6>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5, T6}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5, T6> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
/// <typeparam name="T6">The type of the sixth component to query.</typeparam>
/// <typeparam name="T7">The type of the seventh component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5, T6, T7>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5, T6, T7}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
/// <typeparam name="T6">The type of the sixth component to query.</typeparam>
/// <typeparam name="T7">The type of the seventh component to query.</typeparam>
/// <typeparam name="T8">The type of the eighth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5, T6, T7, T8>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
/// <typeparam name="T6">The type of the sixth component to query.</typeparam>
/// <typeparam name="T7">The type of the seventh component to query.</typeparam>
/// <typeparam name="T8">The type of the eighth component to query.</typeparam>
/// <typeparam name="T9">The type of the ninth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent
    where T9 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            var componentIndex9 = archetype.GetComponentIndex<T9>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                var components9 = chunk.GetComponents<T9>(componentIndex9);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n], ref components9[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            var componentIndex9 = archetype.GetComponentIndex<T9>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                var components9 = chunk.GetComponents<T9>(componentIndex9);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n], ref components9[n]);
            }
        }
    }
}

/// <summary>
/// Represents a query for entities in the ECS that match specific component criteria.
/// </summary>
/// <typeparam name="T1">The type of the first component to query.</typeparam>
/// <typeparam name="T2">The type of the second component to query.</typeparam>
/// <typeparam name="T3">The type of the third component to query.</typeparam>
/// <typeparam name="T4">The type of the fourth component to query.</typeparam>
/// <typeparam name="T5">The type of the fifth component to query.</typeparam>
/// <typeparam name="T6">The type of the sixth component to query.</typeparam>
/// <typeparam name="T7">The type of the seventh component to query.</typeparam>
/// <typeparam name="T8">The type of the eighth component to query.</typeparam>
/// <typeparam name="T9">The type of the ninth component to query.</typeparam>
/// <typeparam name="T10">The type of the tenth component to query.</typeparam>
public readonly ref struct Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent
    where T9 : struct, IComponent
    where T10 : struct, IComponent
{
    private readonly World _world;
    private readonly ComponentMask _withMask;
    private readonly ComponentMask _withoutMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> struct.
    /// </summary>
    /// <param name="world">The world containing the entities and components.</param>
    /// <param name="with">The mask specifying required components.</param>
    /// <param name="without">The mask specifying excluded components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Query(World world, ComponentMask with, ComponentMask without)
    {
        _world = world;
        _withMask = with;
        _withoutMask = without;
    }

    /// <summary>
    /// Adds a required component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> with the added component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> With<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_world, _withMask.With(ComponentId<T>.Id), _withoutMask);
    }

    /// <summary>
    /// Adds an excluded component type to the query.
    /// </summary>
    /// <typeparam name="T">The type of the component to exclude.</typeparam>
    /// <returns>A new <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> with the excluded component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Without<T>()
		where T : struct, IComponent
    {
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_world, _withMask, _withoutMask.With(ComponentId<T>.Id));
    }

    /// <summary>
    /// Retrieves a single entity's component matching the query.
    /// Throws an exception if no matching entity is found or if multiple entities match.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A reference to the single component of type <typeparamref name="T"/>.</returns>
    public ref T Single<T>()
		where T : struct, IComponent
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex = archetype.GetComponentIndex<T>();
            foreach (var chunk in archetype.GetChunks())
            {
                if (chunk.Count is 1)
                    return ref chunk.GetComponents<T>(componentIndex)[0];
            }
        }
        throw new InvalidOperationException("No single entity found matching the query.");
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity's components.
    /// </summary>
    /// <param name="callback">The callback function to process each set of components.</param>
    public void ForEach(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            var componentIndex9 = archetype.GetComponentIndex<T9>();
            var componentIndex10 = archetype.GetComponentIndex<T10>();
            foreach (var chunk in archetype.GetChunks())
            {
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                var components9 = chunk.GetComponents<T9>(componentIndex9);
                var components10 = chunk.GetComponents<T10>(componentIndex10);
                for (var n = 0; n < chunk.Count; n++)
                    callback(ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n], ref components9[n], ref components10[n]);
            }
        }
    }

    /// <summary>
    /// Iterates over all entities matching the query and invokes the specified callback for each entity and its components.
    /// </summary>
    /// <param name="callback">The callback function to process each entity and its components.</param>
    public void ForEach(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        var archetypes = _world.GetMatchingArchetypes(in _withMask, in _withoutMask);
        foreach (var archetype in archetypes)
        {
            var componentIndex1 = archetype.GetComponentIndex<T1>();
            var componentIndex2 = archetype.GetComponentIndex<T2>();
            var componentIndex3 = archetype.GetComponentIndex<T3>();
            var componentIndex4 = archetype.GetComponentIndex<T4>();
            var componentIndex5 = archetype.GetComponentIndex<T5>();
            var componentIndex6 = archetype.GetComponentIndex<T6>();
            var componentIndex7 = archetype.GetComponentIndex<T7>();
            var componentIndex8 = archetype.GetComponentIndex<T8>();
            var componentIndex9 = archetype.GetComponentIndex<T9>();
            var componentIndex10 = archetype.GetComponentIndex<T10>();
            foreach (var chunk in archetype.GetChunks())
            {
                var entities = chunk.GetEntities();
                var components1 = chunk.GetComponents<T1>(componentIndex1);
                var components2 = chunk.GetComponents<T2>(componentIndex2);
                var components3 = chunk.GetComponents<T3>(componentIndex3);
                var components4 = chunk.GetComponents<T4>(componentIndex4);
                var components5 = chunk.GetComponents<T5>(componentIndex5);
                var components6 = chunk.GetComponents<T6>(componentIndex6);
                var components7 = chunk.GetComponents<T7>(componentIndex7);
                var components8 = chunk.GetComponents<T8>(componentIndex8);
                var components9 = chunk.GetComponents<T9>(componentIndex9);
                var components10 = chunk.GetComponents<T10>(componentIndex10);
                for (var n = 0; n < chunk.Count; n++)
                    callback(entities[n], ref components1[n], ref components2[n], ref components3[n], ref components4[n], ref components5[n], ref components6[n], ref components7[n], ref components8[n], ref components9[n], ref components10[n]);
            }
        }
    }
}
