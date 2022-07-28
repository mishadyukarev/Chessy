﻿using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
using Photon.Pun;
using UnityEngine;
namespace Chessy.Model
{
    sealed class CenterReadyUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal CenterReadyUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var readyBut = eUI.CenterEs.ReadyButtonC;

            readyBut.Image.color = PlayerInfoE(AboutGameC.CurrentPlayerIType).PlayerInfoC.IsReadyForStartOnlineGameP ? Color.red : Color.white;

            if (AboutGameC.IsStartedGameP || PhotonNetwork.OfflineMode)
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