// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    public readonly void ForEachBatch<T1>(Action<Span<T1>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    action(batchSpan1);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2>(Action<Span<T1>, Span<T2>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3>(Action<Span<T1>, Span<T2>, Span<T3>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();
                var span9 = archetypeChunk.GetSpan<T9>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    var batchSpan9 = span9.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8, batchSpan9);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>, Span<T10>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();
                var span9 = archetypeChunk.GetSpan<T9>();
                var span10 = archetypeChunk.GetSpan<T10>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    var batchSpan9 = span9.Slice(i, minBatchCount);
                    var batchSpan10 = span10.Slice(i, minBatchCount);
                    action(batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8, batchSpan9, batchSpan10);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1>(Action<ReadOnlySpan<Entity>, Span<T1>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();
                var span9 = archetypeChunk.GetSpan<T9>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    var batchSpan9 = span9.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8, batchSpan9);
                }
            }
        }
    }

    public readonly void ForEachBatch<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<ReadOnlySpan<Entity>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>, Span<T10>> action, int batchCount = 64)
    {
        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            foreach (var archetypeChunk in archetype)
            {
                if (archetypeChunk.Count is 0)
                    continue;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();
                var span9 = archetypeChunk.GetSpan<T9>();
                var span10 = archetypeChunk.GetSpan<T10>();

                for (var i = 0; i < archetypeChunk.Count; i += batchCount)
                {
                    var minBatchCount = Math.Min(batchCount, archetypeChunk.Count - i);
                    var batchEntities = entities.Slice(i, minBatchCount);
                    var batchSpan1 = span1.Slice(i, minBatchCount);
                    var batchSpan2 = span2.Slice(i, minBatchCount);
                    var batchSpan3 = span3.Slice(i, minBatchCount);
                    var batchSpan4 = span4.Slice(i, minBatchCount);
                    var batchSpan5 = span5.Slice(i, minBatchCount);
                    var batchSpan6 = span6.Slice(i, minBatchCount);
                    var batchSpan7 = span7.Slice(i, minBatchCount);
                    var batchSpan8 = span8.Slice(i, minBatchCount);
                    var batchSpan9 = span9.Slice(i, minBatchCount);
                    var batchSpan10 = span10.Slice(i, minBatchCount);
                    action(batchEntities, batchSpan1, batchSpan2, batchSpan3, batchSpan4, batchSpan5, batchSpan6, batchSpan7, batchSpan8, batchSpan9, batchSpan10);
                }
            }
        }
    }
}
