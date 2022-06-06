using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct SetStatsUnitS
    {
        readonly StatsE _statsE;

        internal SetStatsUnitS(in StatsE statsE)
        {
            _statsE = statsE;
        }

        internal void Set(in double hp, in double steps, in double water)
        {
            _statsE.HealthC.Health = hp;
            _statsE.StepC.Steps = steps;
            _statsE.WaterC.Water = water;
        }
    }
}