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
                if (BuildEs(idx_0).BuildingE.HaveBuilding && BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.IceWall))
                {
                    var idxs_01 = CellWorker.GetIdxsAround(idx_0);
                    idxs_01.Add(idx_0);

                    foreach (var idx_01 in idxs_01)
                    {
                        if (UnitEs(idx_01).TypeE.HaveUnit && UnitEs(idx_01).OwnerE.OwnerC.Is(BuildEs(idx_0).BuildingE.OwnerC.Player))
                        {
                            Es.UnitStatWaterE(idx_01).SetMax(UnitEs(idx_01), Es.UnitStatUpgradesEs);
                        }
                    }
                }
            }
        }
    }
}