//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    sealed class MineExtractUpdateMS : SystemModelGameAbs, IEcsRunSystem
//    {
//        internal MineExtractUpdateMS(in Chessy.Model.EntitiesModelGame ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            //for (byte cell_0 = 0; cell_0 < Es.LengthCells; cell_0++)
//            //{
//            //    if (Es.BuildE(cell_0).Is(BuildingTypes.Mine))
//            //    {
//            //        if (Es.EnvHillE(cell_0).CanExtractMine(BuildEs(cell_0)))
//            //        {
//            //            Es.EnvHillE(cell_0).ExtractMine(CellEs(cell_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

//            //            if (!Es.EnvHillE(cell_0).HaveAny)
//            //            {
//            //                Es.BuildE(cell_0).Destroy(Es);
//            //            }
//            //        }
//            //        else
//            //        {
//            //            Es.BuildE(cell_0).Destroy(Es);
//            //        }
//            //    }
//            //}
//        }
//    }
//}