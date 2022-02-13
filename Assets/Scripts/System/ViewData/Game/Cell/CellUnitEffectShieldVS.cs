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
                if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                {
                    VEs.UnitEffectVEs(idx_0).ShieldVE.SR.SetActive(Es.UnitE(idx_0).HaveShieldEffect);
                }
                else
                {
                    VEs.UnitEffectVEs(idx_0).ShieldVE.SR.Disable();
                }
            }
        }
    }
}