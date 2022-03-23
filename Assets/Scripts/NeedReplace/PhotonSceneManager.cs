﻿using Chessy.Game;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Common
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {
        Action<SceneTypes> _toggleScene;

        public void StartMy(in Action<SceneTypes> toggleScene)
        {
            _toggleScene = toggleScene;
        }


        public override sealed void OnLeftRoom()
        {
            base.OnLeftRoom();

            _toggleScene(SceneTypes.Menu);
        }

        //public override sealed void OnPhotonPlayerDisconnected(Player otherPlayer)
        //{
        //    base.OnPhotonPlayerDisconnected(otherPlayer);

        //    PhotonNetwork.LeaveRoom();
        //    SpawnInitComSys.ToggleScene(SceneTypes.Menu);
        //}
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            PhotonNetwork.LeaveRoom();
            _toggleScene(SceneTypes.Menu);
        }

        public override sealed void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            PhotonNetwork.LeaveRoom();
        }







        public override sealed void OnJoinedRoom()
        {
            _toggleScene(SceneTypes.Game);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            Rpc.SyncAllMaster();
        }

        //public override sealed void OnDisconnectedFromPhoton()
        //{
        //    base.OnDisconnectedFromPhoton();

        //    PhotonNetwork.OfflineMode = true;
        //}
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            PhotonNetwork.OfflineMode = true;
        }
    }
}