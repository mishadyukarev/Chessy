namespace Game.Game
{
    sealed class CityBuildingGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal CityBuildingGetCellsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                Es.CellEs(idx_0).Player(PlayerTypes.First).CanCityBuildHere = false;
                Es.CellEs(idx_0).Player(PlayerTypes.Second).CanCityBuildHere = false;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (Es.BuildTC(idx_0).Is(BuildingTypes.City))
                {
                    for (var dirT = DirectTypes.None + 1; dirT <= DirectTypes.Left; dirT++)
                    {
                        var idx_1 = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (!Es.UnitTC(idx_1).HaveUnit 
                            && !Es.MountainC(idx_1).HaveAny && !Es.HillC(idx_1).HaveAny && !Es.AdultForestC(idx_1).HaveAny)
                        {
                            Es.CellEs(idx_1).Player(Es.BuildPlayerTC(idx_0).Player).CanCityBuildHere = true;
                        }
                    }
                }
            }
        }
    }
}