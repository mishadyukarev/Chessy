//using Chessy.Game.Values.Cell;
//using Chessy.Game.Values.Cell.Environment;

//namespace Chessy.Game
//{
//    sealed class CityExtractHillMS : SystemAbstract, IEcsRunSystem
//    {
//        internal CityExtractHillMS(in EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
//            {
//                if (E.BuildingTC(idx_0).Is(BuildingTypes.City))
//                {
//                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
//                    {
//                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

//                        if (E.HillC(idx_1).HaveAnyResources)
//                        {
//                            var extract = EnvironmentValues.CITY_EXTRACT_HILL;

//                            if (E.HillC(idx_1).Resources < extract) extract = E.HillC(idx_1).Resources;

//                            E.ResourcesC(E.BuildingPlayerTC(idx_0).Player, ResourceTypes.Ore).Resources += extract;
//                            E.HillC(idx_1).Resources -= extract;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}