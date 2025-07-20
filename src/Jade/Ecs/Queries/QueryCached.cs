// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.CompilerServices;
using Jade.Ecs.Archetypes;

namespace Jade.Ecs.Queries;

/// <summary>
/// Represents a cached query in the ECS (Entity Component System).
/// Stores matching archetypes and provides methods for validation and updates.
/// </summary>
public sealed class QueryCached
{
    private readonly List<Archetype> _matchingArchetypes;
    private readonly Lock _updateLock;
    private long _version;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryCached"/> class.
    /// </summary>
    /// <param name="archetypes">The initial set of matching archetypes.</param>
    /// <param name="version">The initial version of the cached query.</param>
    public QueryCached(IEnumerable<Archetype> archetypes, long version)
    {
        _matchingArchetypes = [.. archetypes];
        _version = version;
        _updateLock = new Lock();
    }

    /// <summary>
    /// Determines whether the cached query is valid for the specified version.
    /// </summary>
    /// <param name="currentVersion">The current version to validate against.</param>
    /// <returns><c>true</c> if the cached query is valid; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(long currentVersion)
    {
        return Interlocked.Read(ref _version) == currentVersion;
    }

    /// <summary>
    /// Updates the cached query with new archetypes and a new version.
    /// </summary>
    /// <param name="newArchetypes">The new set of matching archetypes.</param>
    /// <param name="newVersion">The new version of the cached query.</param>
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

    /// <summary>
    /// Retrieves the list of matching archetypes for the cached query.
    /// </summary>
    /// <returns>An enumerable of matching archetypes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Archetype> GetMatchingArchetypes()
    {
        lock (_updateLock)
            return _matchingArchetypes.ToArray();
    }
}
