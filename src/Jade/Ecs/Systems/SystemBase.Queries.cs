// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;
using Jade.Ecs.Queries;

namespace Jade.Ecs.Systems;

/// <summary>
/// Represents the base class for ECS (Entity Component System) systems.
/// Provides methods for querying components in the ECS world.
/// </summary>
public abstract partial class SystemBase
{
    /// <summary>
    /// Queries components of type <typeparamref name="T"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T">The type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T>(QueryCallback<T> callback)
        where T : struct, IComponent
    {
        World.Query<T>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/> and <typeparamref name="T2"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2>(QueryCallback<T1, T2> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        World.Query<T1, T2>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3>(QueryCallback<T1, T2, T3> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        World.Query<T1, T2, T3>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, and <typeparamref name="T4"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4>(QueryCallback<T1, T2, T3, T4> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, and <typeparamref name="T5"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5>(QueryCallback<T1, T2, T3, T4, T5> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, and <typeparamref name="T6"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5, T6>(QueryCallback<T1, T2, T3, T4, T5, T6> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, and <typeparamref name="T7"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5, T6, T7>(QueryCallback<T1, T2, T3, T4, T5, T6, T7> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6, T7>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, and <typeparamref name="T8"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5, T6, T7, T8>(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
        where T8 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, <typeparamref name="T8"/>, and <typeparamref name="T9"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <typeparam name="T9">The ninth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
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
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, <typeparamref name="T8"/>, <typeparamref name="T9"/>, and <typeparamref name="T10"/> and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <typeparam name="T9">The ninth type of component to query.</typeparam>
    /// <typeparam name="T10">The tenth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity.</param>
    protected void Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
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
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of type <typeparamref name="T"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T">The type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and component.</param>
    protected void QueryWithEntity<T>(QueryCallbackWithEntity<T> callback)
        where T : struct, IComponent
    {
        World.Query<T>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/> and <typeparamref name="T2"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2>(QueryCallbackWithEntity<T1, T2> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        World.Query<T1, T2>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3>(QueryCallbackWithEntity<T1, T2, T3> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        World.Query<T1, T2, T3>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, and <typeparamref name="T4"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4>(QueryCallbackWithEntity<T1, T2, T3, T4> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, and <typeparamref name="T5"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5>(QueryCallbackWithEntity<T1, T2, T3, T4, T5> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, and <typeparamref name="T6"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5, T6>(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, and <typeparamref name="T7"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5, T6, T7>(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6, T7>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, and <typeparamref name="T8"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5, T6, T7, T8>(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8> callback)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
        where T7 : struct, IComponent
        where T8 : struct, IComponent
    {
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, <typeparamref name="T8"/>, and <typeparamref name="T9"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <typeparam name="T9">The ninth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
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
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>().ForEach(callback);
    }

    /// <summary>
    /// Queries components of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>, <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>, <typeparamref name="T8"/>, <typeparamref name="T9"/>, and <typeparamref name="T10"/> along with their associated entity IDs
    /// and executes the provided callback for each matching entity.
    /// </summary>
    /// <typeparam name="T1">The first type of component to query.</typeparam>
    /// <typeparam name="T2">The second type of component to query.</typeparam>
    /// <typeparam name="T3">The third type of component to query.</typeparam>
    /// <typeparam name="T4">The fourth type of component to query.</typeparam>
    /// <typeparam name="T5">The fifth type of component to query.</typeparam>
    /// <typeparam name="T6">The sixth type of component to query.</typeparam>
    /// <typeparam name="T7">The seventh type of component to query.</typeparam>
    /// <typeparam name="T8">The eighth type of component to query.</typeparam>
    /// <typeparam name="T9">The ninth type of component to query.</typeparam>
    /// <typeparam name="T10">The tenth type of component to query.</typeparam>
    /// <param name="callback">The callback to execute for each matching entity and components.</param>
    protected void QueryWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryCallbackWithEntity<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
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
        World.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>().ForEach(callback);
    }
}
