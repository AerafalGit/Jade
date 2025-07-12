// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Abstractions.Components;
using Jade.Ecs.Archives;
using Jade.Ecs.Components;

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    public void ForEach<T1>(QueryAction<T1> action)
        where T1 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entities[i]);
                    action(ref component1);
                }
            }
        }
    }

    public void ForEach<T1>(QueryEntityAction<T1> action)
        where T1 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    action(in entity, ref component1);
                }
            }
        }
    }

    public void ForEach<T1, T2>(QueryAction<T1, T2> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    action(ref component1, ref component2);
                }
            }
        }
    }

    public void ForEach<T1, T2>(QueryEntityAction<T1, T2> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    action(in entity, ref component1, ref component2);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3>(QueryAction<T1, T2, T3> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    action(ref component1, ref component2, ref component3);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3>(QueryEntityAction<T1, T2, T3> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    action(in entity, ref component1, ref component2, ref component3);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(QueryAction<T1, T2, T3, T4> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    action(ref component1, ref component2, ref component3, ref component4);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(QueryEntityAction<T1, T2, T3, T4> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(QueryAction<T1, T2, T3, T4, T5> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(QueryEntityAction<T1, T2, T3, T4, T5> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6>(QueryAction<T1, T2, T3, T4, T5, T6> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6>(QueryEntityAction<T1, T2, T3, T4, T5, T6> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7>(QueryAction<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var componentId9 = Component<T9>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);
        var strategy9 = storage.GetStrategy(componentId9);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var componentArray9 = strategy9 is ArchiveType.Archetype ? archetypeChunk.GetArray<T9>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;
                var sparseSet9 = strategy9 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId9) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    ref var component9 = ref strategy9 is ArchiveType.Archetype ? ref componentArray9.Get(i) : ref sparseSet9!.Get<T9>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var componentId9 = Component<T9>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);
        var strategy9 = storage.GetStrategy(componentId9);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var componentArray9 = strategy9 is ArchiveType.Archetype ? archetypeChunk.GetArray<T9>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;
                var sparseSet9 = strategy9 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId9) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    ref var component9 = ref strategy9 is ArchiveType.Archetype ? ref componentArray9.Get(i) : ref sparseSet9!.Get<T9>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
        where T10 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var componentId9 = Component<T9>.Metadata.Id;
        var componentId10 = Component<T10>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);
        var strategy9 = storage.GetStrategy(componentId9);
        var strategy10 = storage.GetStrategy(componentId10);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var componentArray9 = strategy9 is ArchiveType.Archetype ? archetypeChunk.GetArray<T9>() : null!;
                var componentArray10 = strategy10 is ArchiveType.Archetype ? archetypeChunk.GetArray<T10>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;
                var sparseSet9 = strategy9 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId9) : null!;
                var sparseSet10 = strategy10 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId10) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    ref var component9 = ref strategy9 is ArchiveType.Archetype ? ref componentArray9.Get(i) : ref sparseSet9!.Get<T9>(entity);
                    ref var component10 = ref strategy10 is ArchiveType.Archetype ? ref componentArray10.Get(i) : ref sparseSet10!.Get<T10>(entity);
                    action(ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9, ref component10);
                }
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
        where T5 : unmanaged, IComponent
        where T6 : unmanaged, IComponent
        where T7 : unmanaged, IComponent
        where T8 : unmanaged, IComponent
        where T9 : unmanaged, IComponent
        where T10 : unmanaged, IComponent
    {
        var storage = _world.Archive;
        var matchingArchetypes = storage.Graph.QueryCache.GetMatchingArchetypes(_all, _any, _none);

        var componentId1 = Component<T1>.Metadata.Id;
        var componentId2 = Component<T2>.Metadata.Id;
        var componentId3 = Component<T3>.Metadata.Id;
        var componentId4 = Component<T4>.Metadata.Id;
        var componentId5 = Component<T5>.Metadata.Id;
        var componentId6 = Component<T6>.Metadata.Id;
        var componentId7 = Component<T7>.Metadata.Id;
        var componentId8 = Component<T8>.Metadata.Id;
        var componentId9 = Component<T9>.Metadata.Id;
        var componentId10 = Component<T10>.Metadata.Id;
        var strategy1 = storage.GetStrategy(componentId1);
        var strategy2 = storage.GetStrategy(componentId2);
        var strategy3 = storage.GetStrategy(componentId3);
        var strategy4 = storage.GetStrategy(componentId4);
        var strategy5 = storage.GetStrategy(componentId5);
        var strategy6 = storage.GetStrategy(componentId6);
        var strategy7 = storage.GetStrategy(componentId7);
        var strategy8 = storage.GetStrategy(componentId8);
        var strategy9 = storage.GetStrategy(componentId9);
        var strategy10 = storage.GetStrategy(componentId10);

        foreach (var archetype in matchingArchetypes)
        {
            foreach (var archetypeChunk in archetype.Chunks)
            {
                var entities = GetMatchingEntities(archetypeChunk);
                var componentArray1 = strategy1 is ArchiveType.Archetype ? archetypeChunk.GetArray<T1>() : null!;
                var componentArray2 = strategy2 is ArchiveType.Archetype ? archetypeChunk.GetArray<T2>() : null!;
                var componentArray3 = strategy3 is ArchiveType.Archetype ? archetypeChunk.GetArray<T3>() : null!;
                var componentArray4 = strategy4 is ArchiveType.Archetype ? archetypeChunk.GetArray<T4>() : null!;
                var componentArray5 = strategy5 is ArchiveType.Archetype ? archetypeChunk.GetArray<T5>() : null!;
                var componentArray6 = strategy6 is ArchiveType.Archetype ? archetypeChunk.GetArray<T6>() : null!;
                var componentArray7 = strategy7 is ArchiveType.Archetype ? archetypeChunk.GetArray<T7>() : null!;
                var componentArray8 = strategy8 is ArchiveType.Archetype ? archetypeChunk.GetArray<T8>() : null!;
                var componentArray9 = strategy9 is ArchiveType.Archetype ? archetypeChunk.GetArray<T9>() : null!;
                var componentArray10 = strategy10 is ArchiveType.Archetype ? archetypeChunk.GetArray<T10>() : null!;
                var sparseSet1 = strategy1 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId1) : null!;
                var sparseSet2 = strategy2 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId2) : null!;
                var sparseSet3 = strategy3 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId3) : null!;
                var sparseSet4 = strategy4 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId4) : null!;
                var sparseSet5 = strategy5 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId5) : null!;
                var sparseSet6 = strategy6 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId6) : null!;
                var sparseSet7 = strategy7 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId7) : null!;
                var sparseSet8 = strategy8 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId8) : null!;
                var sparseSet9 = strategy9 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId9) : null!;
                var sparseSet10 = strategy10 is ArchiveType.SparseSet ? storage.GetSparseSet(componentId10) : null!;

                var count = archetypeChunk.Count;

                for (var i = 0; i < count; i++)
                {
                    var entity = entities[i];
                    ref var component1 = ref strategy1 is ArchiveType.Archetype ? ref componentArray1.Get(i) : ref sparseSet1!.Get<T1>(entity);
                    ref var component2 = ref strategy2 is ArchiveType.Archetype ? ref componentArray2.Get(i) : ref sparseSet2!.Get<T2>(entity);
                    ref var component3 = ref strategy3 is ArchiveType.Archetype ? ref componentArray3.Get(i) : ref sparseSet3!.Get<T3>(entity);
                    ref var component4 = ref strategy4 is ArchiveType.Archetype ? ref componentArray4.Get(i) : ref sparseSet4!.Get<T4>(entity);
                    ref var component5 = ref strategy5 is ArchiveType.Archetype ? ref componentArray5.Get(i) : ref sparseSet5!.Get<T5>(entity);
                    ref var component6 = ref strategy6 is ArchiveType.Archetype ? ref componentArray6.Get(i) : ref sparseSet6!.Get<T6>(entity);
                    ref var component7 = ref strategy7 is ArchiveType.Archetype ? ref componentArray7.Get(i) : ref sparseSet7!.Get<T7>(entity);
                    ref var component8 = ref strategy8 is ArchiveType.Archetype ? ref componentArray8.Get(i) : ref sparseSet8!.Get<T8>(entity);
                    ref var component9 = ref strategy9 is ArchiveType.Archetype ? ref componentArray9.Get(i) : ref sparseSet9!.Get<T9>(entity);
                    ref var component10 = ref strategy10 is ArchiveType.Archetype ? ref componentArray10.Get(i) : ref sparseSet10!.Get<T10>(entity);
                    action(in entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9, ref component10);
                }
            }
        }
    }
}
