using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Components.View.Else.Game.General
{
    internal struct PhotonViewComp
    {
        internal static PhotonView PhotonView { get; private set; }

        internal PhotonViewComp(bool needNew)
        {
            PhotonView = ECSManager.RpcView_GO.AddComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.AllocateViewID(PhotonView);
            }
            else
            {
                PhotonView.ViewID = 1001;
            }
        }
    }
}
