using Chessy.Model.Component;
namespace Chessy.Model
{
    static class MainUnitS
    {
        internal static void Set(this ref UnitOnCellC mainUnitE, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight)
        {
            mainUnitE.UnitT = unitT;
            mainUnitE.LevelT = levelT;
            mainUnitE.PlayerT = playerT;
            mainUnitE.ConditionT = conditionT;
            mainUnitE.IsArcherDirectedToRight = isRight;
        }
        internal static void CopyFrom(this ref UnitOnCellC mainUnitToE, in UnitOnCellC unitMainFromE)
        {
            mainUnitToE.UnitT = unitMainFromE.UnitT;
            mainUnitToE.LevelT = unitMainFromE.LevelT;
            mainUnitToE.PlayerT = unitMainFromE.PlayerT;
            mainUnitToE.ConditionT = unitMainFromE.ConditionT;
            mainUnitToE.IsArcherDirectedToRight = unitMainFromE.IsArcherDirectedToRight;
        }

    }
}