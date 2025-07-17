// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;

namespace Jade.Ecs.Queries;

internal sealed class QueryCached
{
    private readonly List<Archetype> _matchingArchetypes;
    private readonly Lock _updateLock;

    private long _version;

    public QueryCached(IEnumerable<Archetype> archetypes, long version)
    {
        _matchingArchetypes = [.. archetypes];
        _version = version;
        _updateLock = new Lock();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(long currentVersion)
    {
        return Interlocked.Read(ref _version) == currentVersion;
    }

    public void Update(IEnumerable<Archetype> newArchetypes, long newVersion)
    {
        lock (_updateLock)
        {
            if (Interlocked.Read(ref _version) == newVersion)
                return;

            _matchingArchetypes.Clear();
            _matchingArchetypes.AddRange(newArchetypes);

            Interlocked.Exchange(ref _version, newVersion);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Archetype> GetMatchingArchetypes()
    {
        lock (_updateLock)
            return _matchingArchetypes.ToArray();
    }
}
