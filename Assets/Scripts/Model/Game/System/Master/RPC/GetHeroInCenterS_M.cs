﻿using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class GetHeroInCenterS_M : SystemModel
    {
        internal GetHeroInCenterS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (eMG.LessonT == LessonTypes.PickingGod)
            {
                eMG.LessonTC.SetNextLesson();
            }

            eMG.PlayerInfoE(whoseMove).GodInfoE.UnitT = unitT;
            eMG.PlayerInfoE(whoseMove).GodInfoE.HaveHeroInInventor = true;
        }
    }
}