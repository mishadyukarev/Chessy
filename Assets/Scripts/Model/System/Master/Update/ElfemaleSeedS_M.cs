//using Chessy.Model.Values.Cell.Environment;

//namespace Chessy.Model.Model.System.Master
//{
//    public static class ElfemaleSeedS_M
//    {
//        public static void TrySeed(in byte cell_0, in Chessy.Model.Model.Entity.EntitiesModel e)
//        {
//            if (e.UnitTC(cell_0).Is(UnitTypes.Elfemale))
//            {
//                foreach (var idx_1 in e.CellEs(cell_0).IdxsAround)
//                {
//                    if (!e.AdultForestC(idx_1).HaveAnyResources && !e.HillC(idx_1).HaveAnyResources && !e.YoungForestC(idx_1).HaveAnyResources)
//                    {
//                        e.YoungForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
//                        return;
//                    }
//                }
//            }
//        }
//    }
//}