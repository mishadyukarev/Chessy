using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using System;

namespace Chessy.Game
{
    sealed class DownPawnUIS : SystemUIAbstract
    {
        readonly DownPawnUIE _pawnE;

        internal DownPawnUIS(in DownPawnUIE pawnE, in EntitiesModelGame ents) : base(ents)
        {
            _pawnE = pawnE;
        }

        internal void Run()
        {
            if (e.LessonTC.LessonT == LessonTypes.None || e.LessonTC.LessonT >= LessonTypes.SettingPawn)
            {
                _pawnE.ParenGOC.SetActive(true);

                var curPlayerI = e.CurPlayerITC.PlayerT;

                var amountPawnsInGame = e.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                    + e.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

                _pawnE.AmountTextC.TextUI.text = amountPawnsInGame.ToString() + "/" + e.PlayerInfoE(curPlayerI).MaxAvailablePawns;
                _pawnE.MaxPawnsTextC.TextUI.text = Math.Truncate(e.PlayerInfoE(curPlayerI).PeopleInCity).ToString();
            }
            else
            {
                _pawnE.ParenGOC.SetActive(false);
            }
        }
    }
}