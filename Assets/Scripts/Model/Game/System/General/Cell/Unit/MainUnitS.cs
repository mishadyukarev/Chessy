using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct MainUnitS
    {
        readonly UnitMainE _mainUnitE;

        internal MainUnitS(in UnitMainE mainUnitE)
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
        internal void CopyFrom(in UnitMainE unitMainE)
        {
            _mainUnitE.UnitTC = unitMainE.UnitTC;
            _mainUnitE.LevelTC = unitMainE.LevelTC;
            _mainUnitE.PlayerTC = unitMainE.PlayerTC;
            _mainUnitE.ConditionTC = unitMainE.ConditionTC;
            _mainUnitE.IsRightArcherC = unitMainE.IsRightArcherC;
        }
    }
}