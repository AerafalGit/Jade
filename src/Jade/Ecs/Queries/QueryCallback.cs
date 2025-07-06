// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a callback function for processing a single component in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the component, which must implement <see cref="IComponent"/> and be a value type.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
public delegate void QueryCallback<T1>(ref T1 component1)
    where T1 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing two components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
public delegate void QueryCallback<T1, T2>(ref T1 component1, ref T2 component2)
    where T1 : struct, IComponent
    where T2 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing three components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
public delegate void QueryCallback<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing four components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing five components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing six components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <typeparam name="T6">The type of the sixth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
/// <param name="component6">A reference to the sixth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5, T6>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing seven components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <typeparam name="T6">The type of the sixth component.</typeparam>
/// <typeparam name="T7">The type of the seventh component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
/// <param name="component6">A reference to the sixth component to process.</param>
/// <param name="component7">A reference to the seventh component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5, T6, T7>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing eight components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <typeparam name="T6">The type of the sixth component.</typeparam>
/// <typeparam name="T7">The type of the seventh component.</typeparam>
/// <typeparam name="T8">The type of the eighth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
/// <param name="component6">A reference to the sixth component to process.</param>
/// <param name="component7">A reference to the seventh component to process.</param>
/// <param name="component8">A reference to the eighth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing nine components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <typeparam name="T6">The type of the sixth component.</typeparam>
/// <typeparam name="T7">The type of the seventh component.</typeparam>
/// <typeparam name="T8">The type of the eighth component.</typeparam>
/// <typeparam name="T9">The type of the ninth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
/// <param name="component6">A reference to the sixth component to process.</param>
/// <param name="component7">A reference to the seventh component to process.</param>
/// <param name="component8">A reference to the eighth component to process.</param>
/// <param name="component9">A reference to the ninth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent
    where T9 : struct, IComponent;

/// <summary>
/// Represents a callback function for processing ten components in an ECS query.
/// </summary>
/// <typeparam name="T1">The type of the first component.</typeparam>
/// <typeparam name="T2">The type of the second component.</typeparam>
/// <typeparam name="T3">The type of the third component.</typeparam>
/// <typeparam name="T4">The type of the fourth component.</typeparam>
/// <typeparam name="T5">The type of the fifth component.</typeparam>
/// <typeparam name="T6">The type of the sixth component.</typeparam>
/// <typeparam name="T7">The type of the seventh component.</typeparam>
/// <typeparam name="T8">The type of the eighth component.</typeparam>
/// <typeparam name="T9">The type of the ninth component.</typeparam>
/// <typeparam name="T10">The type of the tenth component.</typeparam>
/// <param name="component1">A reference to the first component to process.</param>
/// <param name="component2">A reference to the second component to process.</param>
/// <param name="component3">A reference to the third component to process.</param>
/// <param name="component4">A reference to the fourth component to process.</param>
/// <param name="component5">A reference to the fifth component to process.</param>
/// <param name="component6">A reference to the sixth component to process.</param>
/// <param name="component7">A reference to the seventh component to process.</param>
/// <param name="component8">A reference to the eighth component to process.</param>
/// <param name="component9">A reference to the ninth component to process.</param>
/// <param name="component10">A reference to the tenth component to process.</param>
public delegate void QueryCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9, ref T10 component10)
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
    where T5 : struct, IComponent
    where T6 : struct, IComponent
    where T7 : struct, IComponent
    where T8 : struct, IComponent
    where T9 : struct, IComponent
    where T10 : struct, IComponent;
