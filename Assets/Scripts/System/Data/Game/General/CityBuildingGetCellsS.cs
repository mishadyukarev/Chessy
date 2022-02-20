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
                E.CellEs(idx_0).Player(PlayerTypes.First).CanCityBuildHere = false;
                E.CellEs(idx_0).Player(PlayerTypes.Second).CanCityBuildHere = false;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.BuildTC(idx_0).Is(BuildingTypes.City))
                {
                    for (var dirT = DirectTypes.None + 1; dirT <= DirectTypes.Left; dirT++)
                    {
                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (!E.UnitTC(idx_1).HaveUnit 
                            && !E.MountainC(idx_1).HaveAny && !E.HillC(idx_1).HaveAny && !E.AdultForestC(idx_1).HaveAny)
                        {
                            E.CellEs(idx_1).Player(E.BuildPlayerTC(idx_0).Player).CanCityBuildHere = true;
                        }
                    }
                }
            }
        }
    }
}