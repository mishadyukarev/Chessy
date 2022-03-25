using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using System;

namespace Chessy.Game.Model.System
{
    sealed class KillUnitS : SystemModelGameAbs
    {
        readonly SystemsModelGameUnit _unitSs;

        internal KillUnitS(in SystemsModelGameUnit unitSs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _unitSs = unitSs;
        }

        public void Kill(in byte cell_0, in PlayerTypes whoKiller)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.King)) eMGame.WinnerC.Player = whoKiller;
            }
            
            if (eMGame.UnitTC(cell_0).IsGod)
            {
                var cooldown = 0f;

                switch (eMGame.UnitTC(cell_0).Unit)
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

                eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).HeroCooldownC.Cooldown = cooldown;
                eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).HaveHeroInInventor = true;
            }

            if (eMGame.UnitTC(cell_0).Is(UnitTypes.Tree)) eMGame.HaveTreeUnit = false;


            _unitSs.SetLastDiedS.Set(cell_0);
            eMGame.UnitInfo(eMGame.UnitMainE(cell_0)).Take(eMGame.UnitTC(cell_0).Unit, 1);


            _unitSs.ClearUnitS.Clear(cell_0);
        }
    }
}