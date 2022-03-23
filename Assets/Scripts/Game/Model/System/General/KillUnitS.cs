using System;

namespace Chessy.Game.System.Model
{
    public struct KillUnitS
    {
        public void Kill(in byte idx_0, in PlayerTypes whoKiller, in SetLastDiedS setLastDiedS, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (e.UnitTC(idx_0).Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
            }
            
            if (e.UnitTC(idx_0).IsHero)
            {
                var cooldown = 0f;

                switch (e.UnitTC(idx_0).Unit)
                {
                    case UnitTypes.Elfemale:
                        cooldown = HeroCooldownValues.Elfemale;
                        break;

                    case UnitTypes.Snowy:
                        cooldown = HeroCooldownValues.Snowy;
                        break;

                    case UnitTypes.Undead:
                        cooldown = HeroCooldownValues.Undead;
                        break;

                    case UnitTypes.Hell:
                        cooldown = HeroCooldownValues.Hell;
                        break;

                    default: throw new Exception();
                }

                e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).HeroCooldownC.Cooldown = cooldown;
                e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).HaveHeroInInventor = true;
            }

            if (e.UnitTC(idx_0).Is(UnitTypes.Tree)) e.HaveTreeUnit = false;


            setLastDiedS.Set(e.UnitMainE(idx_0), ref e.UnitEs(idx_0).WhoLastDiedHereE);
            e.UnitInfo(e.UnitMainE(idx_0)).Take(e.UnitTC(idx_0).Unit, 1);



            e.UnitTC(idx_0).Unit = UnitTypes.None;
        }
    }
}