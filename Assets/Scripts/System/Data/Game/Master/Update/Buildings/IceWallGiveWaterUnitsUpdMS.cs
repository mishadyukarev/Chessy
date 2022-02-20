namespace Game.Game
{
    sealed class IceWallGiveWaterUnitsUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallGiveWaterUnitsUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildTC(idx_0).HaveBuilding && E.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    //var idxs_01 = Es.CellEs(idx_0).Idxs;
                    //idxs_01.Add(idx_0);

                    //foreach (var idx_01 in Es.CellEs(idx_0).Idxs)
                    //{
                    //    if (Es.UnitTC(idx_01).HaveUnit && Es.UnitPlayerTC(idx_01).Is(Es.BuildPlayerTC(idx_0).Player))
                    //    {
                    //        //Es.UnitE(idx_01).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                    //    }
                    //}
                }
            }
        }
    }
}