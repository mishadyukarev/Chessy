namespace Game.Game
{
    sealed class IceWallFertilizeAroundUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallFertilizeAroundUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildTC(idx_0).HaveBuilding && E.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (!E.BuildTC(idx_1).Is(BuildingTypes.City) && !E.MountainC(idx_1).HaveAny)
                        {
                            if(!E.HillC(idx_1).HaveAny)
                            {
                                E.FertilizeC(idx_1).Resources = CellEnvironment_Values.AddingFromIceWall(EnvironmentTypes.Fertilizer);
                            }
                        }


                        if (E.UnitTC(idx_1).HaveUnit && E.UnitPlayerTC(idx_1).Is(E.BuildPlayerTC(idx_0).Player))
                        {
                            //Es.UnitE(idx_1).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                    }
                }
            }
        }
    }
}