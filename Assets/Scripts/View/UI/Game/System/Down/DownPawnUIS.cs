﻿using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using System;
using Chessy.Game.System;

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
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.SettingPawn)
            {
                _pawnE.ParenGOC.SetActive(true);

                var curPlayerI = _e.CurPlayerIT;

                _pawnE.AmountTextC.TextUI.text = _e.PlayerInfoE(curPlayerI).PawnInfoC.AmountInGame.ToString() + "/" + _e.PlayerInfoE(curPlayerI).PawnInfoC.MaxAvailable;
                _pawnE.MaxPawnsTextC.TextUI.text = _e.PlayerInfoE(curPlayerI).PawnInfoC.PeopleInCity.ToString();
            }
            else
            {
                _pawnE.ParenGOC.SetActive(false);
            }
        }
    }
}