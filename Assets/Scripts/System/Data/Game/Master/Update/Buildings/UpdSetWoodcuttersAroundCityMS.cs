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
                if (Es.BuildE(idx_0).Is(BuildingTypes.City))
                {
                    foreach (var idx_1 in Es.CellWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.EnvAdultForestE(idx_1).HaveEnvironment)
                        {
                            if (!Es.BuildE(idx_1).HaveBuilding)
                            {
                                Es.BuildE(idx_1).SetNew(BuildingTypes.Woodcutter, Es.BuildE(idx_0).OwnerC.Player);
                            }
                        }
                    }
                }
            }
        }
    }
}