﻿using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class ReadyZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ReadyZoneUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            ref var readyBut = ref Ready<ButtonUIC>();

            readyBut.Color = Es.ReadyE(Es.WhoseMovePlayerTC.CurPlayerI).IsReadyC.IsReady ? Color.red : Color.white;

            if (Es.IsStartedGameC.Is || PhotonNetwork.OfflineMode)
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