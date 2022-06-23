using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
{
    static class MainUnitS
    {
        internal static void Set(this UnitMainE mainUnitE, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight)
        {
            mainUnitE.UnitT = unitT;
            mainUnitE.LevelT = levelT;
            mainUnitE.PlayerT = playerT;
            mainUnitE.ConditionT = conditionT;
            mainUnitE.IsRightArcherC.IsRight = isRight;
        }
        internal static void CopyFrom(this UnitMainE mainUnitToE, in UnitMainE unitMainFromE)
        {
            mainUnitToE.UnitT = unitMainFromE.UnitT;
            mainUnitToE.LevelT = unitMainFromE.LevelT;
            mainUnitToE.PlayerT = unitMainFromE.PlayerT;
            mainUnitToE.ConditionT = unitMainFromE.ConditionT;
            mainUnitToE.IsRightArcherC = unitMainFromE.IsRightArcherC;
        }

    }
}