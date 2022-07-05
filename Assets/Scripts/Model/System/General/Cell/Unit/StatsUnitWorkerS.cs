namespace Chessy.Model
{
    static class StatsUnitWorkerS
    {
        internal static void SetStats(ref this UnitE statsE, in double hp, in double energy, in double water)
        {
            statsE.HealthC.Health = hp;
            statsE.EnergyC.Energy = energy;
            statsE.WaterC.Water = water;
        }
        internal static void Clear(ref this UnitE statsE)
        {
            statsE.HealthC = default;
            statsE.EnergyC = default;
            statsE.WaterC = default;
        }

        internal static void Set(ref this UnitE unitStatsToE, in UnitE unitStatsFromE)
        {
            unitStatsToE.HealthC = unitStatsFromE.HealthC;
            unitStatsToE.EnergyC = unitStatsFromE.EnergyC;
            unitStatsToE.WaterC = unitStatsFromE.WaterC;
        }
    }
}