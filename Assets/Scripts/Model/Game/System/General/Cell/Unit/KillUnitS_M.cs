using System;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void KillUnit(in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!_eMG.UnitTC(cellIdx).HaveUnit) throw new Exception();


            if (_eMG.UnitPlayerT(cellIdx) == PlayerTypes.Second)
            {
                if (_eMG.LessonT == Enum.LessonTypes.Kill1Enemy) _eMG.LessonTC.SetNextLesson();
            }


            if (whoKiller != PlayerTypes.None)
            {
                if (_eMG.UnitTC(cellIdx).Is(UnitTypes.King)) _eMG.WinnerPlayerT = whoKiller;
            }

            if (_eMG.UnitTC(cellIdx).IsGod)
            {
                var cooldown = 0f;

                switch (_eMG.UnitT(cellIdx))
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

                _eMG.PlayerInfoE(_eMG.UnitPlayerT(cellIdx)).GodInfoE.Cooldown = cooldown;
                _eMG.PlayerInfoE(_eMG.UnitPlayerT(cellIdx)).GodInfoE.HaveHeroInInventor = true;
            }

            if (_eMG.UnitTC(cellIdx).Is(UnitTypes.Tree)) _eMG.HaveTreeUnit = false;


            SetLastDiedUnitOnCell(cellIdx);

            if (_eMG.UnitTC(cellIdx).Is(UnitTypes.Pawn))
            {
                _eMG.PlayerInfoE(_eMG.UnitPlayerT(cellIdx)).PawnInfoC.RemovePawn();
            }






            ClearUnit(cellIdx);
        }
    }
}