//using Chessy.Game.Values;
//using Chessy.Game.Values.Cell;
//using Chessy.Game.Values.Cell.Environment;

//namespace Chessy.Game
//{
//    sealed class IceWallFertilizeAroundUpdateMS : SystemModelGameAbs, IEcsRunSystem
//    {
//        internal IceWallFertilizeAroundUpdateMS(in Chessy.Game.Model.Entity.EntitiesModelGame ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
//            {
//                if (eMGame.BuildingTC(cell_0).HaveBuilding && eMGame.BuildingTC(cell_0).Is(BuildingTypes.IceWall))
//                {
//                    var aroundIdxs = eMGame.CellEs(cell_0).IdxsAroundHashSet;
//                    aroundIdxs.Add(cell_0);

//                    foreach (var cell_0_1 in aroundIdxs)
//                    {
//                        if (!eMGame.BuildingTC(cell_0_1).Is(BuildingTypes.City) && !eMGame.MountainC(cell_0_1).HaveAnyResources)
//                        {
//                            if (!eMGame.HillC(cell_0_1).HaveAnyResources)
//                            {
//                                eMGame.FertilizeC(cell_0_1).Resources = EnvironmentValues.ADDING_FROM_ICE_WALL;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}