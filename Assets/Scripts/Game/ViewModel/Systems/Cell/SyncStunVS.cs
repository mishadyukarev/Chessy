namespace Chessy.Game
{
    static class SyncStunVS
    {
        public static void Sync(in byte idx_0, in EntitiesView eV, in EntitiesModel e)
        {
            eV.UnitEffectVEs(idx_0).StunSRC.Disable();

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                {
                    eV.UnitEffectVEs(idx_0).StunSRC.SetActive(e.UnitEffectStunC(idx_0).IsStunned);
                }
            }
        }
    }
}