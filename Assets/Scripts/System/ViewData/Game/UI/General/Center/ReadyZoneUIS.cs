﻿using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ReadyZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var readyBut = ref Ready<ButtonUIC>();

            readyBut.Color = Ready<IsReadyC>(Entities.WhoseMoveE.CurPlayerI).IsReady ? Color.red : Color.white;

            if (GameInfo<IsStartedGameC>().IsStartedGame || PhotonNetwork.OfflineMode)
            {
                readyBut.SetActiveParent(false);
            }
            else
            {
                readyBut.SetActiveParent(true);
            }
        }
    }
}