namespace Chessy.Game
{
    static class SyncStunVS
    {
        public static void Sync(in byte idx_0, in EntitiesView eV, in EntitiesModel e)
        {
            if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
            {
                eV.UnitEffectVEs(idx_0).StunVE.SetActive(e.UnitEffectStunC(idx_0).IsStunned);
            }
            else
            {
                eV.UnitEffectVEs(idx_0).StunVE.Disable();
            }
        }
    }
}