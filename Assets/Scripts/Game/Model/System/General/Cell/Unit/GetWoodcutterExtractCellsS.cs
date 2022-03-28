using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class GetWoodcutterExtractCellsS : SystemModelGameAbs
    {
        internal GetWoodcutterExtractCellsS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Get(in byte cell_0)
        {
            eMGame.WoodcutterExtractE(cell_0).Resources = 0;

            if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
            {
                var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (eMGame.AdultForestC(cell_0).Resources < extract) extract = eMGame.AdultForestC(cell_0).Resources;


                eMGame.WoodcutterExtractE(cell_0).Resources = extract;
            }
        }
    }
}