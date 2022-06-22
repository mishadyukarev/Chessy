using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    static class StatsUnitWorkerS
    {
        internal static void Set(this UnitStatsE statsE, in double hp, in double steps, in double water)
        {
            statsE.HealthC.Health = hp;
            statsE.StepC.Steps = steps;
            statsE.WaterC.Water = water;
        }
        internal static void Clear(this UnitStatsE statsE)
        {
            statsE.HealthC.Health = default;
            statsE.StepC.Steps = default;
            statsE.WaterC.Water = default;
        }

        internal static void Set(this UnitStatsE unitStatsToE, in UnitStatsE unitStatsFromE)
        {
            unitStatsToE.HealthC = unitStatsFromE.HealthC;
            unitStatsToE.StepC = unitStatsFromE.StepC;
            unitStatsToE.WaterC = unitStatsFromE.WaterC;
        }
    }
}