namespace Game.Game
{
    sealed class CitySetWoodcuttersAroundUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal CitySetWoodcuttersAroundUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildTC(idx_0).Is(BuildingTypes.City))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (E.AdultForestC(idx_1).HaveAnyResources)
                        {
                            if (!E.BuildTC(idx_1).HaveBuilding)
                            {
                                E.BuildE(idx_1).Set(BuildingTypes.Woodcutter, LevelTypes.First, Building_Values.MaxHealth(BuildingTypes.Woodcutter), E.BuildPlayerTC(idx_0).Player);
                            }
                        }
                    }
                }
            }
        }
    }
}