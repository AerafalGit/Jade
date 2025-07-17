// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Queries;

public ref partial struct Query
{
    public readonly void ForEachParallel<T1>(QueryAction<T1> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2>(QueryAction<T1, T2> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3>(QueryAction<T1, T2, T3> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4>(QueryAction<T1, T2, T3, T4> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5>(QueryAction<T1, T2, T3, T4, T5> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6>(QueryAction<T1, T2, T3, T4, T5, T6> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7>(QueryAction<T1, T2, T3, T4, T5, T6, T7> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();
                var span9 = archetypeChunk.GetSpan<T9>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i], ref span9[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

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

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i], ref span9[i], ref span10[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1>(QueryEntityAction<T1> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2>(QueryEntityAction<T1, T2> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3>(QueryEntityAction<T1, T2, T3> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4>(QueryEntityAction<T1, T2, T3, T4> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5>(QueryEntityAction<T1, T2, T3, T4, T5> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6>(QueryEntityAction<T1, T2, T3, T4, T5, T6> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

                var entities = archetypeChunk.Entities;
                var span1 = archetypeChunk.GetSpan<T1>();
                var span2 = archetypeChunk.GetSpan<T2>();
                var span3 = archetypeChunk.GetSpan<T3>();
                var span4 = archetypeChunk.GetSpan<T4>();
                var span5 = archetypeChunk.GetSpan<T5>();
                var span6 = archetypeChunk.GetSpan<T6>();
                var span7 = archetypeChunk.GetSpan<T7>();
                var span8 = archetypeChunk.GetSpan<T8>();

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

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

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i], ref span9[i]);
            });
        }
    }

    public readonly void ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(QueryEntityAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, ParallelOptions? parallelOptions = null)
    {
        parallelOptions ??= new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        foreach (var archetype in _world.GetMatchingArchetypes(in _all, in _any, in _none))
        {
            Parallel.ForEach(archetype.Chunks, parallelOptions, archetypeChunk =>
            {
                if (archetypeChunk.Count is 0)
                    return;

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

                for (var i = 0; i < archetypeChunk.Count; i++)
                    action(in entities[i], ref span1[i], ref span2[i], ref span3[i], ref span4[i], ref span5[i], ref span6[i], ref span7[i], ref span8[i], ref span9[i], ref span10[i]);
            });
        }
    }
}
