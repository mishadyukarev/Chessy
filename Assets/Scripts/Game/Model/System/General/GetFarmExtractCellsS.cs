using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    public struct GetFarmExtractCellsS
    {
        public GetFarmExtractCellsS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.FarmExtractFertilizeE(idx_0).Resources = 0;

            if (e.BuildingTC(idx_0).Is(BuildingTypes.Farm))
            {
                if (e.FertilizeC(idx_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.FARM_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                    //}

                    if (e.FertilizeC(idx_0).Resources < extract) extract = e.FertilizeC(idx_0).Resources;

                    e.FarmExtractFertilizeE(idx_0).Resources = extract;
                }
            }
        }
    }
}