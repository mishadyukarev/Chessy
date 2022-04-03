using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetStatsUnitS : SystemModelGameAbs
    {
        internal SetStatsUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in float hp, in float steps, in float water, in byte cell_0)
        {
            eMG.UnitHpC(cell_0).Health = hp;
            eMG.UnitStepC(cell_0).Steps = steps;
            eMG.UnitWaterC(cell_0).Water = water;
        }

        internal void Set(in byte cell_from, in byte cell_to) => eMG.UnitStatsE(cell_to) = eMG.UnitStatsE(cell_from);
    }
}