﻿using Chessy.Model.Enum;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void GetHeroInCenterM(in UnitTypes unitT, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (_aboutGameC.LessonT == LessonTypes.PickingGod)
            {
                 SetNextLesson();
            }

            _e.PlayerInfoE(whoDoing).GodInfoC.UnitType = unitT;
            _e.PlayerInfoE(whoDoing).GodInfoC.HaveGodInInventor = true;
        }
    }
}