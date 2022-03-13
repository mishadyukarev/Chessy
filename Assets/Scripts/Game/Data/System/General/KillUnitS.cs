using System;

namespace Chessy.Game.System.Model
{
    public struct KillUnitS
    {
        public KillUnitS(in byte idx_0, in PlayerTypes whoKiller, in EntitiesModel e)
        {
            if (e.UnitTC(idx_0).Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
            else if (e.IsHero(e.UnitTC(idx_0).Unit))
            {
                var cooldown = 0f;

                switch (e.UnitTC(idx_0).Unit)
                {
                    case UnitTypes.Elfemale:
                        cooldown = HeroCooldown_VALUES.Elfemale;
                        break;

                    case UnitTypes.Snowy:
                        cooldown = HeroCooldown_VALUES.Snowy;
                        break;

                    case UnitTypes.Undead:
                        cooldown = HeroCooldown_VALUES.Undead;
                        break;

                    case UnitTypes.Hell:
                        cooldown = HeroCooldown_VALUES.Hell;
                        break;

                    default: throw new Exception();
                }

                e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).HeroCooldownC.Cooldown = cooldown;
                e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).HaveHeroInInventor = true;
            }


            e.LastDiedE(idx_0).Set(e.UnitMainE(idx_0));
            e.UnitInfo(e.UnitMainE(idx_0)).Take(e.UnitTC(idx_0).Unit, 1);



            e.UnitTC(idx_0).Unit = UnitTypes.None;
        }
    }
}