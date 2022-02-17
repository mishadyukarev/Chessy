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
                if (Es.BuildTC(idx_0).HaveBuilding && Es.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (!Es.BuildTC(idx_1).Is(BuildingTypes.City) && !Es.MountainC(idx_1).HaveAny)
                        {
                            if(!Es.HillC(idx_1).HaveAny)
                            {
                                Es.FertilizeC(idx_1).Resources = CellEnvironment_Values.AddingFromIceWall(EnvironmentTypes.Fertilizer);
                            }
                        }


                        if (Es.UnitTC(idx_1).HaveUnit && Es.UnitPlayerTC(idx_1).Is(Es.BuildPlayerTC(idx_0).Player))
                        {
                            //Es.UnitE(idx_1).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                    }
                }
            }
        }
    }
}