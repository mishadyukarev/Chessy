using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct SetMainUnitS
    {
        readonly UnitMainE _mainUnitE;

        internal SetMainUnitS(in UnitMainE mainUnitE)
        {
            _mainUnitE = mainUnitE;
        }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight)
        {
            _mainUnitE.UnitTC.UnitT = unitT;
            _mainUnitE.LevelTC.LevelT = levelT;
            _mainUnitE.PlayerTC.PlayerT = playerT;
            _mainUnitE.ConditionTC.Condition = conditionT;
            _mainUnitE.IsRightArcherC.IsRight = isRight;
        }
    }
}