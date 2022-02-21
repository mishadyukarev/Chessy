namespace Game.Game
{
    sealed class WoodcutterExtractGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal WoodcutterExtractGetCellsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.WoodcutterExtractE(idx_0).ResourcesC.Resources = 0;

                if (E.BuildTC(idx_0).Is(BuildingTypes.Woodcutter))
                {
                    var extract = CellEnvironment_Values.WOODCUTTER_EXTRACT;


                    if (E.AdultForestC(idx_0).Resources < extract) extract = E.AdultForestC(idx_0).Resources;


                    E.WoodcutterExtractE(idx_0).ResourcesC.Resources = extract;
                }
            }
        }
    }
}