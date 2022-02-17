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
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
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