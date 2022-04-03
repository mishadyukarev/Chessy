using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetMainUnitS : SystemModelGameAbs
    {
        internal SetMainUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight, in byte cell_0)
        {
            eMG.UnitTC(cell_0).UnitT = unitT;
            eMG.UnitLevelTC(cell_0).LevelT = levelT;
            eMG.UnitPlayerTC(cell_0).PlayerT = playerT;
            eMG.UnitConditionTC(cell_0).Condition = conditionT;
            eMG.UnitIsRightArcherC(cell_0).IsRight = isRight;
        }

        internal void Set(in byte cell_from, in byte cell_to) => eMG.UnitMainE(cell_to) = eMG.UnitMainE(cell_from);
    }
}