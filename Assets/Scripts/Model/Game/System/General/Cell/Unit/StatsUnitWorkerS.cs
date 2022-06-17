using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct StatsUnitWorkerS
    {
        readonly StatsE _statsE;

        internal StatsUnitWorkerS(in StatsE statsE)
        {
            _statsE = statsE;
        }

        internal void Set(in double hp, in double steps, in double water)
        {
            _statsE.HealthC.Health = hp;
            _statsE.StepC.Steps = steps;
            _statsE.WaterC.Water = water;
        }
        internal void Clear()
        {
            _statsE.HealthC.Health = default;
            _statsE.StepC.Steps = default;
            _statsE.WaterC.Water = default;
        }
    }
}