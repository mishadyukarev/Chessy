﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Photon.Realtime;
using Photon.Pun;

namespace Chessy.Game.Model.System.Master
{
    sealed class GetHeroS_M : SystemModel
    {
        internal GetHeroS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (eMG.LessonTC.LessonT == LessonTypes.PickingGod)
            {
                eMG.LessonTC.SetNextLesson();
            }

            eMG.PlayerInfoE(whoseMove).GodInfoE.UnitT = unitT;
            eMG.PlayerInfoE(whoseMove).GodInfoE.HaveHeroInInventor = true;
        }
    }
}