namespace Game.Game
{
    sealed class UpdGiveWaterCloudScowyMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdGiveWaterCloudScowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitEs(idx_0).UnitE.UnitTC.Is(UnitTypes.Snowy))
                {
                    if (CellWorker.GetIdxsAround(Es.WindCloudE.CenterCloud.Idx).Contains(idx_0))
                    {
                        Es.UnitE(idx_0).SetMaxWater(Es.UnitStatUpgradesEs);
                    }
                    
                }
            }
        }
    }
}