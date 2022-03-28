using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetStatsUnitS
    {
        readonly StatsE _statsE;

        internal SetStatsUnitS(in StatsE statsE) { _statsE = statsE; }

        internal void Set(in float hp, in float steps, in float water)
        {
            _statsE.HealthC.Health = hp;
            _statsE.StepC.Steps = steps;
            _statsE.WaterC.Water = water;
        }
        internal void Set(in StatsE statsE)
        {
            _statsE.HealthC = statsE.HealthC;
            _statsE.StepC = statsE.StepC;
            _statsE.WaterC = statsE.WaterC;
        }
    }
}