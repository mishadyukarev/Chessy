//using Chessy.Model.Values.Environment;

//using Chessy.Model.Entity; namespace Chessy.Model.Master
//{
//    public static class ElfemaleSeedS_M
//    {
//        public static void TrySeed(in byte cell_0, in Chessy.Model.EntitiesModel e)
//        {
//            if (e.UnitTC(cell_0).Is(UnitTypes.Elfemale))
//            {
//                foreach (var idx_1 in e.CellEs(cell_0).IdxsAround)
//                {
//                    if (!e.AdultForestC(idx_1].HaveEnvironment(EnvironmentTypes.AdultForest) && !e.HillC(idx_1].HaveEnvironment(EnvironmentTypes.AdultForest) && !e.YoungForestC(idx_1].HaveEnvironment(EnvironmentTypes.AdultForest))
//                    {
//                        e.YoungForestC(idx_1).Resources = ValuesChessy.MAX_RESOURCES;
//                        return;
//                    }
//                }
//            }
//        }
//    }
//}