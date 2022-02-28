namespace Chessy.Game
{
    sealed class FarmExtractGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal FarmExtractGetCellsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.FarmExtractFertilizeE(idx_0).Resources = 0;

                if (E.BuildingTC(idx_0).Is(BuildingTypes.Farm))
                {
                    if (E.FertilizeC(idx_0).HaveAnyResources)
                    {
                        var extract = Environment_Values.FARM_EXTRACT;

                        if (E.BuildingsInfo(E.BuildingMainE(idx_0)).HaveCenterUpgrade)
                        {
                            extract += Environment_Values.FARM_CENTER_UPGRADE;
                        }

                        if (E.FertilizeC(idx_0).Resources < extract) extract = E.FertilizeC(idx_0).Resources;

                        E.FarmExtractFertilizeE(idx_0).Resources = extract;
                    }
                }
            }
        }
    }
}