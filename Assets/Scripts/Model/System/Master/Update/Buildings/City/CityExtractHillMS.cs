//using Chessy.Model.Values;
//using Chessy.Model.Values.Environment;

//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    sealed class CityExtractHillMS : SystemAbstract, IEcsRunSystem
//    {
//        internal CityExtractHillMS(in Chessy.Model.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte cell_0 = 0; cell_0 < E.LengthCells; cell_0++)
//            {
//                if (E.BuildingTC(cell_0).Is(BuildingTypes.City))
//                {
//                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
//                    {
//                        var idx_1 = E.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

//                        if (E.HillC(idx_1).HaveAnyResources)
//                        {
//                            var extract = ValuesChessy.CITY_EXTRACT_HILL;

//                            if (E.HillC(idx_1).Resources < extract) extract = E.HillC(idx_1).Resources;

//                            E.ResourcesC(E.BuildingPlayerTC(cell_0).Player, ResourceTypes.Ore).Resources += extract;
//                            E.HillC(idx_1).Resources -= extract;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}