// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs;

/// <summary>
/// Represents the ECS (Entity Component System) world, providing methods for querying entities
/// based on their associated components.
/// </summary>
public sealed partial class World
{
    /// <summary>
    /// Creates a query for entities that have a single component of type <typeparamref name="T1"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1}"/> instance for querying entities.</returns>
    public Query<T1> Query<T1>()
        where T1 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        return new Query<T1>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have two components of types <typeparamref name="T1"/> and <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2}"/> instance for querying entities.</returns>
    public Query<T1, T2> Query<T1, T2>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        return new Query<T1, T2>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have three components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3> Query<T1, T2, T3>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        return new Query<T1, T2, T3>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have four components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, and <typeparamref name="T4"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4> Query<T1, T2, T3, T4>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        return new Query<T1, T2, T3, T4>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have five components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, and <typeparamref name="T5"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5> Query<T1, T2, T3, T4, T5>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        return new Query<T1, T2, T3, T4, T5>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have six components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, and <typeparamref name="T6"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T6">The type of the sixth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5, T6}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5, T6> Query<T1, T2, T3, T4, T5, T6>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        mask = mask.With(ComponentId<T6>.Id);
        return new Query<T1, T2, T3, T4, T5, T6>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have seven components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, and <typeparamref name="T7"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T6">The type of the sixth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T7">The type of the seventh component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5, T6, T7}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5, T6, T7> Query<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        mask = mask.With(ComponentId<T6>.Id);
        mask = mask.With(ComponentId<T7>.Id);
        return new Query<T1, T2, T3, T4, T5, T6, T7>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have eight components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, and <typeparamref name="T8"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T6">The type of the sixth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T7">The type of the seventh component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T8">The type of the eighth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5, T6, T7, T8> Query<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
        where T8 : struct, IComponent
    {
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        mask = mask.With(ComponentId<T6>.Id);
        mask = mask.With(ComponentId<T7>.Id);
        mask = mask.With(ComponentId<T8>.Id);
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have nine components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>,
    /// <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>,
    /// <typeparamref name="T7"/>, <typeparamref name="T8"/>, and <typeparamref name="T9"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T6">The type of the sixth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T7">The type of the seventh component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T8">The type of the eighth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T9">The type of the ninth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
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
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        mask = mask.With(ComponentId<T6>.Id);
        mask = mask.With(ComponentId<T7>.Id);
        mask = mask.With(ComponentId<T8>.Id);
        mask = mask.With(ComponentId<T9>.Id);
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this, mask, default);
    }

    /// <summary>
    /// Creates a query for entities that have ten components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>,
    /// <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>,
    /// <typeparamref name="T7"/>, <typeparamref name="T8"/>, <typeparamref name="T9"/>, and <typeparamref name="T10"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T2">The type of the second component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T3">The type of the third component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T4">The type of the fourth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T5">The type of the fifth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T6">The type of the sixth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T7">The type of the seventh component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T8">The type of the eighth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T9">The type of the ninth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <typeparam name="T10">The type of the tenth component, which must implement <see cref="IComponent"/>.</typeparam>
    /// <returns>A <see cref="Query{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> instance for querying entities.</returns>
    public Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
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
        var mask = new ComponentMask();
        mask = mask.With(ComponentId<T1>.Id);
        mask = mask.With(ComponentId<T2>.Id);
        mask = mask.With(ComponentId<T3>.Id);
        mask = mask.With(ComponentId<T4>.Id);
        mask = mask.With(ComponentId<T5>.Id);
        mask = mask.With(ComponentId<T6>.Id);
        mask = mask.With(ComponentId<T7>.Id);
        mask = mask.With(ComponentId<T8>.Id);
        mask = mask.With(ComponentId<T9>.Id);
        mask = mask.With(ComponentId<T10>.Id);
        return new Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this, mask, default);
    }
}
