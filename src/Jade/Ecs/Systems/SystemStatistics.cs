// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Jade.Ecs.Systems;

[StructLayout(LayoutKind.Sequential)]
public readonly struct SystemStatistics
{
    public readonly int TotalSystems;

    public readonly int EnabledSystems;

    public readonly int DisabledSystems;

    public readonly int StageCount;

    public SystemStatistics(int totalSystems, int enabledSystems, int disabledSystems, int stageCount)
    {
        TotalSystems = totalSystems;
        EnabledSystems = enabledSystems;
        DisabledSystems = disabledSystems;
        StageCount = stageCount;
    }

    public override string ToString()
    {
        return $"Systems({EnabledSystems}/{TotalSystems} enabled, {StageCount} stages)";
    }
}
