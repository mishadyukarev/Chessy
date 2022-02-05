namespace Game.Game
{
    sealed class IceWallFertilizeAroundUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallFertilizeAroundUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildE(idx_0).HaveBuilding && Es.BuildE(idx_0).Is(BuildingTypes.IceWall))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (!Es.BuildE(idx_1).Is(BuildingTypes.City) && !Es.EnvMountainE(idx_1).HaveEnvironment)
                        {
                            if(!Es.EnvHillE(idx_1).HaveEnvironment)
                            {
                                Es.EnvFertilizerE(idx_1).AddFromIceWall();
                            }
                        }


                        if (UnitEs(idx_1).MainE.HaveUnit && Es.UnitOwnerE(idx_1).Is(Es.BuildE(idx_0).OwnerC.Player))
                        {
                            Es.UnitStatWaterE(idx_1).SetMax(UnitEs(idx_1), Es.UnitStatUpgradesEs);
                        }
                    }
                }
            }
        }
    }
}