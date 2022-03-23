using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    public struct GetWoodcutterExtractCellsS
    {
        public GetWoodcutterExtractCellsS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.WoodcutterExtractE(idx_0).Resources = 0;

            if (e.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter))
            {
                var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (e.AdultForestC(idx_0).Resources < extract) extract = e.AdultForestC(idx_0).Resources;


                e.WoodcutterExtractE(idx_0).Resources = extract;
            }
        }
    }
}