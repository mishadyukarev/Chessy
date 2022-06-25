//namespace Chessy.Model
//{
//    sealed class SmelterSmeltUpdateMS : SystemAbstract, IEcsRunSystem
//    {
//        internal SmelterSmeltUpdateMS(in Chessy.Model.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte cell_0 = 0; cell_0 < E.LengthCells; cell_0++)
//            {
//                if (E.BuildingTC(cell_0).HaveBuilding)
//                {
//                    if (E.IsActiveSmelter(cell_0))
//                    {
//                        //Es.InventorResourcesEs.Melt_Master(Es.BuildPlayerTC(cell_0).Player);
//                    }
//                }
//            }
//        }
//    }
//}