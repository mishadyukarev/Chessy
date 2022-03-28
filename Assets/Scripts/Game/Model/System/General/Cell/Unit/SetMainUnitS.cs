using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetMainUnitS
    {
        readonly UnitMainE _unitMainE;

        internal SetMainUnitS(in UnitMainE unitMainE) { _unitMainE = unitMainE; }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight)
        {
            _unitMainE.UnitTC.Unit = unitT;
            _unitMainE.LevelTC.Level = levelT;
            _unitMainE.PlayerTC.Player = playerT;
            _unitMainE.ConditionTC.Condition = conditionT;
            _unitMainE.IsRightArcherC.IsRight = isRight;
        }
        internal void Set(in UnitMainE unitMainE)
        {
            _unitMainE.UnitTC = unitMainE.UnitTC;
            _unitMainE.LevelTC = unitMainE.LevelTC;
            _unitMainE.PlayerTC = unitMainE.PlayerTC;
            _unitMainE.ConditionTC = unitMainE.ConditionTC;
            _unitMainE.IsRightArcherC = unitMainE.IsRightArcherC;
        }
    }
}