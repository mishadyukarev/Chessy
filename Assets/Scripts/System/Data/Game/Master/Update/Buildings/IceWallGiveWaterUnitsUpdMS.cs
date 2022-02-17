namespace Game.Game
{
    sealed class IceWallGiveWaterUnitsUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallGiveWaterUnitsUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildTC(idx_0).HaveBuilding && Es.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    var idxs_01 = CellWorker.GetIdxsAround(idx_0);
                    idxs_01.Add(idx_0);

                    foreach (var idx_01 in idxs_01)
                    {
                        if (Es.UnitTC(idx_01).HaveUnit && Es.UnitPlayerTC(idx_01).Is(Es.BuildPlayerTC(idx_0).Player))
                        {
                            //Es.UnitE(idx_01).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                    }
                }
            }
        }
    }
}