namespace Game.Game
{
    sealed class WoodcutterExtractGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal WoodcutterExtractGetCellsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.WoodcutterExtractE(idx_0).Resources = 0;

                if (E.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter))
                {
                    var extract = Environment_Values.WOODCUTTER_EXTRACT;

                    if (E.BuildingsInfo(E.BuildingMainE(idx_0)).HaveCenterUpgrade)
                    {
                        extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                    }


                    if (E.AdultForestC(idx_0).Resources < extract) extract = E.AdultForestC(idx_0).Resources;


                    E.WoodcutterExtractE(idx_0).Resources = extract;
                }
            }
        }
    }
}