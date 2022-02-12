namespace Game.Game
{
    sealed class UpdAttackFromWaterHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdAttackFromWaterHellMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitEs(idx_0).UnitE.UnitTC.Is(UnitTypes.Hell))
                {
                    if (Es.RiverEs(idx_0).RiverE.HaveRiverNear)
                    {
                        Es.UnitE(idx_0).TakeHpHellWithNearWater(Es);
                    }

                    if (CellWorker.GetIdxsAround(Es.WindCloudE.CenterCloud.Idx).Contains(idx_0))
                    {
                        Es.UnitE(idx_0).TakeHpHellWithCloud(Es);
                        break;
                    }

                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.BuildEs(idx_1).BuildingE.BuildTC.Is(BuildingTypes.IceWall))
                        {
                            Es.UnitE(idx_0).TakeHpHellWithIceWall(Es);
                            break;
                        }
                    }
                }
            }
        }
    }
}