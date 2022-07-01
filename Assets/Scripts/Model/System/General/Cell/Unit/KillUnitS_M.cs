using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    static partial class SystemStatic
    {
        internal static void KillUnit(this EntitiesModel e, in PlayerTypes whoKiller, in byte cellIdxForKilling)
        {
            if (!e.UnitT(cellIdxForKilling).HaveUnit()) throw new Exception();


            if (e.UnitPlayerT(cellIdxForKilling) == PlayerTypes.Second)
            {
                if (e.LessonT == LessonTypes.Kill1Enemy) e.CommonInfoAboutGameC.SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (e.UnitT(cellIdxForKilling) == UnitTypes.King) e.WinnerPlayerT = whoKiller;
            }

            if (e.UnitT(cellIdxForKilling).IsGod())
            {
                var cooldown = 0f;

                switch (e.UnitT(cellIdxForKilling))
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

                e.PlayerInfoE(e.UnitPlayerT(cellIdxForKilling)).GodInfoC.Cooldown = cooldown;
                e.PlayerInfoE(e.UnitPlayerT(cellIdxForKilling)).GodInfoC.HaveGodInInventor = true;
            }

            if (e.UnitT(cellIdxForKilling).Is(UnitTypes.Tree)) e.HaveTreeUnit = false;


            e.SetLastDiedUnitOnCell(cellIdxForKilling);

            if (e.UnitT(cellIdxForKilling).Is(UnitTypes.Pawn))
            {
                e.PawnPeopleInfoC(e.UnitPlayerT(cellIdxForKilling)).AmountInGame--;
            }


            e.UnitE(cellIdxForKilling).ClearEverything();
        }
    }
}