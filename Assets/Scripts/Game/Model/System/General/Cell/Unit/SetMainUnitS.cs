using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetMainUnitS : SystemModelGameAbs
    {
        internal SetMainUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight, in byte cell_0)
        {
            e.UnitTC(cell_0).Unit = unitT;
            e.UnitLevelTC(cell_0).Level = levelT;
            e.UnitPlayerTC(cell_0).Player = playerT;
            e.UnitConditionTC(cell_0).Condition = conditionT;
            e.UnitIsRightArcherC(cell_0).IsRight = isRight;
        }

        internal void Set(in byte cell_from, in byte cell_to) => e.UnitMainE(cell_to) = e.UnitMainE(cell_from);
    }
}