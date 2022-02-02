namespace Game.Game
{
    sealed class CellUnitEffectShieldVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitEffectShieldVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                {
                    UnitEffectVEs(idx_0).ShieldVE.SR.SetActive(UnitEffectEs(idx_0).ShieldE.HaveShieldEffect);
                }
                else
                {
                    UnitEffectVEs(idx_0).ShieldVE.SR.Disable();
                }
            }
        }
    }
}