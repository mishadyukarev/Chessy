using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetFarmExtractCells(in byte cell_0)
        {
            eMG.FarmExtractFertilizeC(cell_0).Resources = 0;

            if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Farm))
            {
                if (eMG.FertilizeC(cell_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.FARM_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                    //}

                    if (eMG.FertilizeC(cell_0).Resources < extract) extract = eMG.FertilizeC(cell_0).Resources;

                    eMG.FarmExtractFertilizeC(cell_0).Resources = extract;
                }
            }
        }
    }
}