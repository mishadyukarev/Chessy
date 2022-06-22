using Chessy.Common.View.UI.System;
using Chessy.Game;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Menu;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Common
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {
        SystemsModelGame _sMG;
        SystemsViewUICommon _sUIC;



        public void StartMy(in SystemsViewUICommon sUIC, in SystemsModelGame sMG)
        {
            _sMG = sMG;
            _sUIC = sUIC;
        }


        public override sealed void OnLeftRoom() => _sMG.CommonSs.OnLeftRoom();

        //public override sealed void OnPhotonPlayerDisconnected(Player otherPlayer)
        //{
        //    base.OnPhotonPlayerDisconnected(otherPlayer);

        //    PhotonNetwork.LeaveRoom();
        //    SpawnInitComSys.ToggleScene(SceneTypes.Menu);
        //}
        public override void OnPlayerLeftRoom(Player otherPlayer) => _sMG.CommonSs.OnPlayerLeftRoom(otherPlayer);

        public override sealed void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }







        public override sealed void OnJoinedRoom() => _sMG.OnJoinedRoom();
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            _sMG.SyncDataM();
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