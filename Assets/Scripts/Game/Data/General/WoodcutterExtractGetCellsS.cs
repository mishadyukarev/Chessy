using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class WoodcutterExtractGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal WoodcutterExtractGetCellsS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                E.WoodcutterExtractE(idx_0).Resources = 0;

                if (E.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter))
                {
                    var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                    //}


                    if (E.AdultForestC(idx_0).Resources < extract) extract = E.AdultForestC(idx_0).Resources;


                    E.WoodcutterExtractE(idx_0).Resources = extract;
                }
            }
        }
    }
}