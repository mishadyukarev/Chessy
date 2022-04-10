﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetMainUnitS : SystemModel
    {
        internal SetMainUnitS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight, in byte cell_0)
        {
            eMG.UnitTC(cell_0).UnitT = unitT;
            eMG.UnitLevelTC(cell_0).LevelT = levelT;
            eMG.UnitPlayerTC(cell_0).PlayerT = playerT;
            eMG.UnitConditionTC(cell_0).Condition = conditionT;
            eMG.UnitIsRightArcherC(cell_0).IsRight = isRight;
        }

        internal void Set(in byte cell_from, in byte cell_to)
        {
            eMG.UnitTC(cell_to) = eMG.UnitTC(cell_from);
            eMG.UnitLevelTC(cell_to) = eMG.UnitLevelTC(cell_from);
            eMG.UnitPlayerTC(cell_to) = eMG.UnitPlayerTC(cell_from);
            eMG.UnitConditionTC(cell_to) = eMG.UnitConditionTC(cell_from);
            eMG.UnitIsRightArcherC(cell_to) = eMG.UnitIsRightArcherC(cell_from);
        }
    }
}