﻿using Chessy.Game.Model.Entity;
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

        internal override void Sync()
        {
            if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= LessonTypes.SettingPawn)
            {
                _pawnE.ParenGOC.SetActive(true);

                var curPlayerI = e.CurPlayerITC.PlayerT;

                _pawnE.AmountTextC.TextUI.text = e.PlayerInfoE(curPlayerI).PawnInfoC.AmountInGame.ToString() + "/" + e.PlayerInfoE(curPlayerI).PawnInfoC.MaxAvailable;
                _pawnE.MaxPawnsTextC.TextUI.text = e.PlayerInfoE(curPlayerI).PawnInfoC.PeopleInCity.ToString();
            }
            else
            {
                _pawnE.ParenGOC.SetActive(false);
            }
        }
    }
}