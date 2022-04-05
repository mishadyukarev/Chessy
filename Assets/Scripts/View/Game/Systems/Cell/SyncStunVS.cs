using Chessy.Game.Entity;

namespace Chessy.Game
{
    static class SyncStunVS
    {
        public static void Sync(in byte idx_0, in EntitiesViewGame eV, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            eV.UnitEffectVEs(idx_0).StunSRC.Disable();

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitVisibleC(idx_0).IsVisible(e.CurPlayerITC.PlayerT))
                {
                    eV.UnitEffectVEs(idx_0).StunSRC.SetActive(e.StunUnitC(idx_0).IsStunned);
                }
            }
        }
    }
}