namespace Chessy.Game
{
    static class ShieldVS
    {
        public static void Run(in byte idx_0, in EntitiesView eV, in EntitiesModel e)
        {
            eV.UnitEffectVEs(idx_0).ShieldVE.Disable();

            if (e.UnitEffectShield(idx_0).HaveAnyProtection)
            {
                if (e.UnitTC(idx_0).HaveUnit)
                {
                    if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                    {
                        eV.UnitEffectVEs(idx_0).ShieldVE.SetActive(true);
                    }
                }
            }
        }
    }
}