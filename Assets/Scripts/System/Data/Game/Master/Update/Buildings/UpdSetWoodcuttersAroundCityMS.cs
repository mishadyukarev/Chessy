namespace Game.Game
{
    sealed class UpdSetWoodcuttersAroundCityMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdSetWoodcuttersAroundCityMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildTC(idx_0).Is(BuildingTypes.City))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (Es.AdultForestC(idx_1).HaveAny)
                        {
                            if (!Es.BuildTC(idx_1).HaveBuilding)
                            {
                                //Es.BuildTC(idx_1).SetNew(BuildingTypes.Woodcutter, Es.BuildPlayerTC(idx_0).Owner);
                            }
                        }
                    }
                }
            }
        }
    }
}