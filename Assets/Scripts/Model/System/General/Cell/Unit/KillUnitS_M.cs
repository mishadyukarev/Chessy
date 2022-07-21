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


            if (_unitCs[cellIdxForKilling].PlayerT == PlayerTypes.Second)
            {
                if (_aboutGameC.LessonT == LessonTypes.Kill1Enemy) SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (_e.UnitT(cellIdxForKilling) == UnitTypes.King) _aboutGameC.WinnerPlayerT = whoKiller;
            }

            if (_e.UnitT(cellIdxForKilling).IsGod())
            {
                var cooldown = 0;

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

                _e.PlayerInfoE(_unitCs[cellIdxForKilling].PlayerT).GodInfoC.CooldownInSecondsForNextAppearance = cooldown;
                _e.PlayerInfoE(_unitCs[cellIdxForKilling].PlayerT).GodInfoC.HaveGodInInventor = true;
            }

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Tree)) _e.HaveTreeUnit = false;


            //_e.SetLastDiedUnitOnCell(cellIdxForKilling);

            if (_e.UnitT(cellIdxForKilling).Is(UnitTypes.Pawn))
            {
                _e.PawnPeopleInfoC(_unitCs[cellIdxForKilling].PlayerT).AmountInGame--;
            }

            _unitWhereViewDataCs[_unitWhereViewDataCs[cellIdxForKilling].ViewIdxCell].DataIdxCell = 0;


            var dataIdxCell = _unitWhereViewDataCs[cellIdxForKilling].DataIdxCell;

            _e.UnitE(cellIdxForKilling).Dispose();

            _unitWhereViewDataCs[cellIdxForKilling].DataIdxCell = dataIdxCell;
        }
    }
}