//using Chessy.Model.Values;
//using Chessy.Model.Values;

//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    sealed class IceWallGiveWaterUnitsUpdMS : SystemModelGameAbs, IEcsRunSystem
//    {
//        internal IceWallGiveWaterUnitsUpdMS(in Chessy.Model.EntitiesModelGame ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
//            {
//                if (eMGame.BuildingTC(cell_0).HaveBuilding && eMGame.BuildingTC(cell_0).Is(BuildingTypes.IceWall))
//                {
//                    var idxs_01 = eMGame.CellEs(cell_0).IdxsAroundHashSet;
//                    idxs_01.Add(cell_0);

//                    foreach (var cell_01 in eMGame.CellEs(cell_0).IdxsAround)
//                    {
//                        if (eMGame.UnitTC(cell_01].HaveUnit && eMGame.UnitPlayerTC(cell_01).Is(eMGame.BuildingPlayerTC(cell_0).Player))
//                        {
//                            eMGame.UnitWaterC(cell_01).Water = WaterValues.MAX;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}