using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void KillUnit(in PlayerTypes whoKiller, in byte cellIdxForKilling)
        {
            if (!_e.UnitT(cellIdxForKilling).HaveUnit()) throw new Exception();


            if (_e.UnitPlayerT(cellIdxForKilling) == PlayerTypes.Second)
            {
                if (_e.LessonT == LessonTypes.Kill1Enemy) SetNextLesson();
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

                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxForKilling)).GodInfoC.Cooldown = cooldown;
                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxForKilling)).GodInfoC.HaveGodInInventor = true;
            }

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Tree)) _e.HaveTreeUnit = false;


            _e.SetLastDiedUnitOnCell(cellIdxForKilling);

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Pawn))
            {
                _e.PawnPeopleInfoC(_e.UnitPlayerT(cellIdxForKilling)).AmountInGame--;
            }


            _e.UnitE(cellIdxForKilling).ClearEverything();
        }
    }
}