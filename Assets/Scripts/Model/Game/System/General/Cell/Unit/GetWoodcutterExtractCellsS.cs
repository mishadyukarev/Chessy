using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        public void GetWoodcutterExtractCells(in byte cell_0)
        {
            eMG.WoodcutterExtractC(cell_0).Resources = 0;

            if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
            {
                var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (eMG.AdultForestC(cell_0).Resources < extract) extract = eMG.AdultForestC(cell_0).Resources;


                eMG.WoodcutterExtractC(cell_0).Resources = extract;
            }
        }
    }
}