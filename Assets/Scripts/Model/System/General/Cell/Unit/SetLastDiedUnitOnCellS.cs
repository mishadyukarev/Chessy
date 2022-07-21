//using Chessy.Model.Entity;

//namespace Chessy.Model.System
//{
//    static partial class SystemStatic
//    {
//        internal static void SetLastDiedUnitOnCell(this EntitiesModel e, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
//        {
//            e.SetLastDiedUnitT(cell_0, unitT);
//            e.SetLastDiedLevelT(cell_0, levelT);
//            e.SetLastDiedPlayerT(cell_0, playerT);
//        }

//        internal static void SetLastDiedUnitOnCell(this EntitiesModel e, in byte cell_from, in byte cell_to)
//        {
//            e.SetLastDiedUnitT(cell_to, e.LastDiedUnitT(cell_from));
//            e.SetLastDiedPlayerT(cell_to, e.LastDiedPlayerT(cell_from));
//            e.SetLastDiedLevelT(cell_to, e.LastDiedLevelT(cell_from));
//        }
//        internal static void SetLastDiedUnitOnCell(this EntitiesModel e, in byte cell_0)
//        {
//            e.SetLastDiedUnitT(cell_0, e.UnitT(cell_0));
//            e.SetLastDiedPlayerT(cell_0, e.UnitPlayerT(cell_0));
//            e.SetLastDiedLevelT(cell_0, e.UnitLevelT(cell_0));
//        }
//    }
//}