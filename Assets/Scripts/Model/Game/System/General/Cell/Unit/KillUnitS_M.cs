using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class KillUnitS_M : SystemModel
    {
        internal KillUnitS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Kill(in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!eMG.UnitTC(cellIdx).HaveUnit) throw new Exception();


            if (eMG.UnitPlayerT(cellIdx) == PlayerTypes.Second)
            {
                if (eMG.LessonT == Enum.LessonTypes.Kill1Enemy) eMG.LessonTC.SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (eMG.UnitTC(cellIdx).Is(UnitTypes.King)) eMG.WinnerPlayerT = whoKiller;
            }

            if (eMG.UnitTC(cellIdx).IsGod)
            {
                var cooldown = 0f;

                switch (eMG.UnitT(cellIdx))
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

                eMG.PlayerInfoE(eMG.UnitPlayerT(cellIdx)).GodInfoE.Cooldown = cooldown;
                eMG.PlayerInfoE(eMG.UnitPlayerT(cellIdx)).GodInfoE.HaveHeroInInventor = true;
            }

            if (eMG.UnitTC(cellIdx).Is(UnitTypes.Tree)) eMG.HaveTreeUnit = false;


            sMG.UnitSs.SetLastDiedUnitOnCellS.Set(cellIdx);

            if (eMG.UnitTC(cellIdx).Is(UnitTypes.Pawn))
            {
                eMG.PlayerInfoE(eMG.UnitPlayerT(cellIdx)).PawnInfoC.RemovePawn();
            }






            sMG.UnitSs.ClearUnit(cellIdx);
        }
    }
}