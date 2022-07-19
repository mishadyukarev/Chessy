using Chessy.Model.Component;
namespace Chessy.Model
{
    static class MainUnitS
    {
        internal static void Set(this ref UnitOnCellC mainUnitE, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight)
        {
            mainUnitE.UnitType = unitT;
            mainUnitE.LevelType = levelT;
            mainUnitE.PlayerType = playerT;
            mainUnitE.ConditionType = conditionT;
            mainUnitE.IsArcherDirectedToRight = isRight;
        }
        internal static void CopyFrom(this ref UnitOnCellC mainUnitToE, in UnitOnCellC unitMainFromE)
        {
            mainUnitToE.UnitType = unitMainFromE.UnitType;
            mainUnitToE.LevelType = unitMainFromE.LevelType;
            mainUnitToE.PlayerType = unitMainFromE.PlayerType;
            mainUnitToE.ConditionType = unitMainFromE.ConditionType;
            mainUnitToE.IsArcherDirectedToRight = unitMainFromE.IsArcherDirectedToRight;
        }
    }
}