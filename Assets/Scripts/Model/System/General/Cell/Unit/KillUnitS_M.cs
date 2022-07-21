using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void KillUnit(in PlayerTypes whoKiller, in byte cellIdxForKilling)
        {
            if (!_unitCs[cellIdxForKilling].HaveUnit) throw new Exception();


            if (_unitCs[cellIdxForKilling].PlayerT == PlayerTypes.Second)
            {
                if (_aboutGameC.LessonT == LessonTypes.Kill1Enemy) SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (_unitCs[cellIdxForKilling].UnitT == UnitTypes.King) _aboutGameC.WinnerPlayerT = whoKiller;
            }

            if (_unitCs[cellIdxForKilling].UnitT.IsGod())
            {
                var cooldown = 0;

                switch (_unitCs[cellIdxForKilling].UnitT)
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

                PlayerInfoE(_unitCs[cellIdxForKilling].PlayerT).GodInfoC.CooldownInSecondsForNextAppearance = cooldown;
                PlayerInfoE(_unitCs[cellIdxForKilling].PlayerT).GodInfoC.HaveGodInInventor = true;
            }

            if (_unitCs[cellIdxForKilling].UnitT.Is(UnitTypes.Tree)) _aboutGameC.HaveTreeUnit = false;


            //_e.SetLastDiedUnitOnCell(cellIdxForKilling);

            if (_unitCs[cellIdxForKilling].UnitT.Is(UnitTypes.Pawn))
            {
                PawnPeopleInfoC(_unitCs[cellIdxForKilling].PlayerT).AmountInGame--;
            }

            _unitWhereViewDataCs[_unitWhereViewDataCs[cellIdxForKilling].ViewIdxCell].DataIdxCell = 0;


            var dataIdxCell = _unitWhereViewDataCs[cellIdxForKilling].DataIdxCell;

            _unitEs[cellIdxForKilling].Dispose();

            _unitWhereViewDataCs[cellIdxForKilling].DataIdxCell = dataIdxCell;
        }
    }
}