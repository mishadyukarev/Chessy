﻿//using Chessy.Model.Values;
//using Chessy.Model.Values;
//using Chessy.Model.Values.Environment;

//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    sealed class IceWallFertilizeAroundUpdateMS : SystemModelGameAbs, IEcsRunSystem
//    {
//        internal IceWallFertilizeAroundUpdateMS(in Chessy.Model.EntitiesModelGame ents) : base(ents)
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
//                        if (!eMGame.BuildingTC(cell_0_1).Is(BuildingTypes.City) && !eMGame.MountainC(cell_0_1].HaveEnvironment(EnvironmentTypes.AdultForest))
//                        {
//                            if (!eMGame.HillC(cell_0_1].HaveEnvironment(EnvironmentTypes.AdultForest))
//                            {
//                                eMGame.FertilizeC(cell_0_1).Resources = ValuesChessy.ADDING_FROM_ICE_WALL;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}