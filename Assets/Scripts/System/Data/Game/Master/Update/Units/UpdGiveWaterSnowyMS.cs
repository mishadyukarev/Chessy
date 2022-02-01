namespace Game.Game
{
    sealed class UpdGiveWaterSnowyMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdGiveWaterSnowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && UnitEs(idx_0).MainE.UnitTC.Is(UnitTypes.Snowy))
                {
                    UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                }
            }
        }
    }
}