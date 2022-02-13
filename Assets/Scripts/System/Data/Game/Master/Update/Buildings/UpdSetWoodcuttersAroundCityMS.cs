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
                if (Es.BuildingE(idx_0).Is(BuildingTypes.City))
                {
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.AdultForestE(idx_1).HaveEnvironment)
                        {
                            if (!Es.BuildingE(idx_1).HaveBuilding)
                            {
                                Es.BuildingE(idx_1).SetNew(BuildingTypes.Woodcutter, Es.BuildingE(idx_0).Owner);
                            }
                        }
                    }
                }
            }
        }
    }
}