using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class WoodcutterExtractGetCellsS : CellSystem, IEcsRunSystem
    {
        internal WoodcutterExtractGetCellsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.WoodcutterExtractE(Idx).Resources = 0;

            if (E.BuildingTC(Idx).Is(BuildingTypes.Woodcutter))
            {
                var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (E.AdultForestC(Idx).Resources < extract) extract = E.AdultForestC(Idx).Resources;


                E.WoodcutterExtractE(Idx).Resources = extract;
            }
        }
    }
}