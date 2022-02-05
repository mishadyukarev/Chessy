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
                if (UnitEs(idx_0).MainE.UnitTC.Is(UnitTypes.Snowy))
                {
                    if (CellWorker.GetIdxsAround(Es.WindCloudE.CenterCloud.Idx).Contains(idx_0))
                    {
                        UnitStatEs(idx_0).WaterE.SetMax(UnitEs(idx_0), Es.UnitStatUpgradesEs);
                    }
                    
                }
            }
        }
    }
}