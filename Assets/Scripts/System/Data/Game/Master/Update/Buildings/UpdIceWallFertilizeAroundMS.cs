namespace Game.Game
{
    sealed class UpdIceWallFertilizeAroundMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdIceWallFertilizeAroundMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (BuildEs(idx_0).BuildingE.HaveBuilding && BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.IceWall))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (!BuildEs(idx_1).BuildingE.BuildTC.Is(BuildingTypes.City) && !EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                        {
                            if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                            {
                                EnvironmentEs(idx_1).AdultForest.AddFromIceWall();
                            }
                            else if(!EnvironmentEs(idx_1).Hill.HaveEnvironment)
                            {
                                EnvironmentEs(idx_1).Fertilizer.AddFromIceWall();
                            }
                        }


                        if (UnitEs(idx_1).MainE.HaveUnit && UnitEs(idx_1).OwnerE.OwnerC.Is(BuildEs(idx_0).BuildingE.OwnerC.Player))
                        {
                            UnitStatEs(idx_1).WaterE.SetMax(UnitEs(idx_1), Es.UnitStatUpgradesEs);
                        }
                    }
                }
            }
        }
    }
}