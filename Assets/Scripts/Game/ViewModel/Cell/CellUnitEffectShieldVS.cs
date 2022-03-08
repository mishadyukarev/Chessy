namespace Chessy.Game
{
    sealed class CellUnitEffectShieldVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitEffectShieldVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                VEs.UnitEffectVEs(idx_0).ShieldVE.Disable();

                if (E.UnitEffectShield(idx_0).HaveAnyProtection)
                {
                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                        {
                            VEs.UnitEffectVEs(idx_0).ShieldVE.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}