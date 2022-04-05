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
            eMG.HpUnitC(cell_0).Health = hp;
            eMG.StepUnitC(cell_0).Steps = steps;
            eMG.WaterUnitC(cell_0).Water = water;
        }

        internal void Set(in byte cell_from, in byte cell_to)
        {
            eMG.HpUnitC(cell_to) = eMG.HpUnitC(cell_from);
            eMG.StepUnitC(cell_to) = eMG.StepUnitC(cell_from);
            eMG.WaterUnitC(cell_to) = eMG.WaterUnitC(cell_from);
        }
    }
}