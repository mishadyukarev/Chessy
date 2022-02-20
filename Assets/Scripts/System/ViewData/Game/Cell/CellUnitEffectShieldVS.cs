namespace Game.Game
{
    sealed class CellUnitEffectShieldVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitEffectShieldVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerI.Player).IsVisible)
                {
                    VEs.UnitEffectVEs(idx_0).ShieldVE.SR.SetActive(E.UnitEffectShield(idx_0).HaveAnyProtection);
                }
                else
                {
                    VEs.UnitEffectVEs(idx_0).ShieldVE.SR.Disable();
                }
            }
        }
    }
}