using Chessy.Game.Entity;

namespace Chessy.Game
{
    static class ShieldVS
    {
        public static void Run(in byte idx_0, in EntitiesViewGame eV, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            eV.UnitEffectVEs(idx_0).ShieldSRC.Disable();

            if (e.UnitEffectShield(idx_0).HaveAnyProtection)
            {
                if (e.UnitTC(idx_0).HaveUnit)
                {
                    if (e.UnitVisibleC(idx_0).IsVisible(e.CurPlayerITC.PlayerT))
                    {
                        eV.UnitEffectVEs(idx_0).ShieldSRC.SetActive(true);
                    }
                }
            }
        }
    }
}