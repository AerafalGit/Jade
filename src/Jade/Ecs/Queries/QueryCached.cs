// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;

namespace Jade.Ecs.Queries;

internal sealed class QueryCached
{
    public List<Archetype> MatchingArchetypes { get; }

    public int EntityCount { get; private set; }

    public ulong Version { get; private set; }

    public QueryCached(IEnumerable<Archetype> archetypes, ulong version)
    {
        MatchingArchetypes = [.. archetypes];
        EntityCount = MatchingArchetypes.Sum(static a => a.EntityCount);
        Version = version;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(ulong currentVersion)
    {
        return Version == currentVersion;
    }

    public void Update(IEnumerable<Archetype> newArchetypes, ulong newVersion)
    {
        MatchingArchetypes.Clear();
        MatchingArchetypes.AddRange(newArchetypes);
        EntityCount = MatchingArchetypes.Sum(static a => a.EntityCount);
        Version = newVersion;
    }
}
