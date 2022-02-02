namespace Game.Game
{
    sealed class UpdIceWallGiveWaterMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdIceWallGiveWaterMS(in Entities ents) : base(ents)
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
                        if (UnitEs(idx_01).MainE.HaveUnit(UnitStatEs(idx_01)) && UnitEs(idx_01).MainE.OwnerC.Is(BuildEs(idx_0).BuildingE.OwnerC.Player))
                        {
                            UnitStatEs(idx_01).WaterE.SetMax(UnitEs(idx_01).MainE, Es.UnitStatUpgradesEs);
                        }
                    }
                }
            }
        }
    }
}