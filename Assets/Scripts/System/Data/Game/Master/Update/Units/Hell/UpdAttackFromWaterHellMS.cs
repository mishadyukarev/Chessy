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
                if (UnitEs(idx_0).TypeE.UnitTC.Is(UnitTypes.Hell))
                {
                    if (RiverEs(idx_0).RiverE.HaveRiverNear)
                    {
                        UnitStatEs(idx_0).Hp.TakeHpHellWithNearWater(Es);
                    }

                    if (CellWorker.GetIdxsAround(Es.WindCloudE.CenterCloud.Idx).Contains(idx_0))
                    {
                        UnitStatEs(idx_0).Hp.TakeHpHellWithCloud(Es);
                        break;
                    }

                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (BuildEs(idx_1).BuildingE.BuildTC.Is(BuildingTypes.IceWall))
                        {
                            UnitStatEs(idx_0).Hp.TakeHpHellWithIceWall(Es);
                            break;
                        }
                    }
                }
            }
        }
    }
}