using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetStatsUnitS : SystemModelGameAbs
    {
        internal SetStatsUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in float hp, in float steps, in float water, in byte cell_0)
        {
            e.UnitHpC(cell_0).Health = hp;
            e.UnitStepC(cell_0).Steps = steps;
            e.UnitWaterC(cell_0).Water = water;
        }

        internal void Set(in byte cell_from, in byte cell_to) => e.UnitStatsE(cell_to) = e.UnitStatsE(cell_from);
    }
}