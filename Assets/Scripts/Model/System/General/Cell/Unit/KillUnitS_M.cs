using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void KillUnit(in PlayerTypes whoKiller, in byte cellIdxForKilling)
        {
            if (!unitCs[cellIdxForKilling].HaveUnit) throw new Exception();


            if (unitCs[cellIdxForKilling].PlayerT == PlayerTypes.Second)
            {
                if (aboutGameC.LessonT == LessonTypes.Kill1Enemy) SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (unitCs[cellIdxForKilling].UnitT == UnitTypes.King) aboutGameC.WinnerPlayerT = whoKiller;
            }

            if (unitCs[cellIdxForKilling].UnitT.IsGod())
            {
                var cooldown = 0;

                switch (unitCs[cellIdxForKilling].UnitT)
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



                var playerT_byte = (byte)unitCs[cellIdxForKilling].PlayerT;

                godInfoCs[playerT_byte].CooldownInSecondsForNextAppearance = cooldown;
                godInfoCs[playerT_byte].HaveGodInInventor = true;
            }

            if (unitCs[cellIdxForKilling].UnitT.Is(UnitTypes.Tree)) aboutGameC.HaveTreeUnitInGame = false;


            //_e.SetLastDiedUnitOnCell(cellIdxForKilling);

            if (unitCs[cellIdxForKilling].UnitT.Is(UnitTypes.Pawn))
            {
                PawnPeopleInfoC(unitCs[cellIdxForKilling].PlayerT).AmountInGame--;
            }

            unitWhereViewDataCs[unitWhereViewDataCs[cellIdxForKilling].ViewIdxCell].DataIdxCell = 0;


            var dataIdxCell = unitWhereViewDataCs[cellIdxForKilling].DataIdxCell;

            _unitEs[cellIdxForKilling].Dispose();

            unitWhereViewDataCs[cellIdxForKilling].DataIdxCell = dataIdxCell;
        }
    }
}