using Chessy.Model.Cell.Unit;

namespace Chessy.Model
{
    static class StatsUnitWorkerS
    {
        internal static void SetStats(this UnitE statsE, in double hp, in double steps, in double water)
        {
            statsE.HealthC.Health = hp;
            statsE.EnergyC.Energy = steps;
            statsE.WaterC.Water = water;
        }
        internal static void Clear(this UnitE statsE)
        {
            statsE.HealthC = default;
            statsE.EnergyC = default;
            statsE.WaterC = default;
        }

        internal static void Set(this UnitE unitStatsToE, in UnitE unitStatsFromE)
        {
            unitStatsToE.HealthC = unitStatsFromE.HealthC;
            unitStatsToE.EnergyC = unitStatsFromE.EnergyC;
            unitStatsToE.WaterC = unitStatsFromE.WaterC;
        }
    }
}