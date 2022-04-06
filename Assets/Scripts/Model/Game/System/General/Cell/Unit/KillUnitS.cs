using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class KillUnitS : SystemModel
    {
        internal KillUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Kill(in PlayerTypes whoKiller, in byte cell_0)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.King)) eMG.WinnerPlayerTC.PlayerT = whoKiller;
            }

            if (eMG.UnitTC(cell_0).IsGod)
            {
                var cooldown = 0f;

                switch (eMG.UnitTC(cell_0).UnitT)
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

                eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.Cooldown = cooldown;
                eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.HaveHeroInInventor = true;
            }

            if (eMG.UnitTC(cell_0).Is(UnitTypes.Tree)) eMG.HaveTreeUnit = false;


            sMG.UnitSs.SetLastDiedS.Set(cell_0);

            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
            {
                eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).PawnInfoE.PawnsInGame--;
            }

            


            sMG.UnitSs.ClearUnitS.Clear(cell_0);
        }
    }
}