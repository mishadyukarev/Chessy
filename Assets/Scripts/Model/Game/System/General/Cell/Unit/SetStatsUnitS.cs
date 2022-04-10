using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetStatsUnitS : SystemModel
    {
        internal SetStatsUnitS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in double hp, in double steps, in double water, in byte cell_0)
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