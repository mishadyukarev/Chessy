﻿//namespace Chessy.Game
//{
//    sealed class CitySetWoodcuttersAroundUpdateMS : SystemAbstract, IEcsRunSystem
//    {
//        internal CitySetWoodcuttersAroundUpdateMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
//            {
//                if (E.BuildingTC(idx_0).Is(BuildingTypes.City))
//                {
//                    if (E.PlayerInfoE(E.BuildingPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
//                    {
//                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
//                        {
//                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

//                            if (E.AdultForestC(idx_1).HaveAnyResources)
//                            {
//                                if (!E.BuildingTC(idx_1).HaveBuilding)
//                                {
//                                    E.BuildingMainE(idx_1).Set(BuildingTypes.Woodcutter, LevelTypes.First, BuildingValues.MAX_HP, E.BuildingPlayerTC(idx_0).Player);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}