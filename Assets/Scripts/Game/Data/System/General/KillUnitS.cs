using System;

namespace Chessy.Game.System.Model
{
    public struct KillUnitS
    {
        public void Kill(in byte idx_0, in PlayerTypes whoKiller, in EntitiesModel e)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (e.UnitTC(idx_0).Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
            }
            
            else if (e.UnitTC(idx_0).IsHero)
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



            e.LastDiedE(idx_0).Set(e.UnitMainE(idx_0));
            e.UnitInfo(e.UnitMainE(idx_0)).Take(e.UnitTC(idx_0).Unit, 1);



            e.UnitTC(idx_0).Unit = UnitTypes.None;
        }
    }
}