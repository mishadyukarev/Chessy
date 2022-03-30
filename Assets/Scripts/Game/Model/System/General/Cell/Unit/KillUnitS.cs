using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using System;

namespace Chessy.Game.Model.System
{
    sealed class KillUnitS : SystemModelGameAbs
    {
        internal KillUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Kill(in PlayerTypes whoKiller, in byte cell_0)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
            }

            if (e.UnitTC(cell_0).IsGod)
            {
                var cooldown = 0f;

                switch (e.UnitTC(cell_0).Unit)
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

                e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).HeroCooldownC.Cooldown = cooldown;
                e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).HaveHeroInInventor = true;
            }

            if (e.UnitTC(cell_0).Is(UnitTypes.Tree)) e.HaveTreeUnit = false;


            s.SetLastDiedS.Set(cell_0);
            e.UnitInfo(e.UnitMainE(cell_0)).Take(e.UnitTC(cell_0).Unit, 1);


            s.ClearUnitS.Clear(cell_0);
        }
    }
}