using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class GetFarmExtractCellsS : SystemModelGameAbs
    {
        internal GetFarmExtractCellsS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            eMGame.FarmExtractFertilizeE(cell_0).Resources = 0;

            if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Farm))
            {
                if (eMGame.FertilizeC(cell_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.FARM_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                    //}

                    if (eMGame.FertilizeC(cell_0).Resources < extract) extract = eMGame.FertilizeC(cell_0).Resources;

                    eMGame.FarmExtractFertilizeE(cell_0).Resources = extract;
                }
            }
        }
    }
}