using Chessy.Model.Enum;
using System;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitSystems
    {
        internal void KillUnit(in PlayerTypes whoKiller, in byte cellIdxForKilling)
        {
            if (!_e.UnitT(cellIdxForKilling).HaveUnit()) throw new Exception();


            if (_e.UnitPlayerT(cellIdxForKilling) == PlayerTypes.Second)
            {
                if (_e.LessonT == LessonTypes.Kill1Enemy) _e.LessonT.SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (_e.UnitT(cellIdxForKilling) == UnitTypes.King) _e.WinnerPlayerT = whoKiller;
            }

            if (_e.UnitT(cellIdxForKilling).IsGod())
            {
                var cooldown = 0f;

                switch (_e.UnitT(cellIdxForKilling))
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

                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxForKilling)).GodInfoE.Cooldown = cooldown;
                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxForKilling)).GodInfoE.HaveHeroInInventor = true;
            }

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Tree)) _e.HaveTreeUnit = false;


            SetLastDiedUnitOnCell(cellIdxForKilling);

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Pawn))
            {
                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxForKilling)).PawnInfoC.RemovePawn();
            }


            _e.UnitEs(cellIdxForKilling).ClearEverything();
        }
    }
}