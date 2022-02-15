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
                if (Es.BuildingE(idx_0).HaveBuilding && Es.BuildingE(idx_0).Is(BuildingTypes.IceWall))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (!Es.BuildingE(idx_1).Is(BuildingTypes.City) && !Es.MountainE(idx_1).HaveEnvironment)
                        {
                            if(!Es.HillE(idx_1).HaveEnvironment)
                            {
                                Es.FertilizeE(idx_1).AddFromIceWall();
                            }
                        }


                        if (Es.UnitTC(idx_1).HaveUnit && Es.UnitPlayerTC(idx_1).Is(Es.BuildingE(idx_0).Owner))
                        {
                            Es.UnitE(idx_1).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                    }
                }
            }
        }
    }
}