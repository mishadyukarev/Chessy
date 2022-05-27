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
        Rpc _rpc;

        EntitiesModelGame _eMG;

        SystemsModelGame _sMG;
        SystemsModelMenu _sMM;
        SystemsViewUICommon _sUIC;



        public void StartMy(in Rpc rpc, in EntitiesModelGame eMG, in SystemsViewUICommon sUIC, in SystemsModelGame sMG, in SystemsModelMenu sMM)
        {
            _rpc = rpc;

            _eMG = eMG;

            _sMG = sMG;
            _sMM = sMM;
            _sUIC = sUIC;
        }


        public override sealed void OnLeftRoom()
        {
            _sMG.CommonSs.ToggleScene(SceneTypes.Menu);
            _sUIC.ToggleScene(SceneTypes.Menu);
        }

        //public override sealed void OnPhotonPlayerDisconnected(Player otherPlayer)
        //{
        //    base.OnPhotonPlayerDisconnected(otherPlayer);

        //    PhotonNetwork.LeaveRoom();
        //    SpawnInitComSys.ToggleScene(SceneTypes.Menu);
        //}
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }

        public override sealed void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }







        public override sealed void OnJoinedRoom()
        {
            _sMG.OnJoinedRoomS.OnJoinedRoom(_rpc);
            _sUIC.ToggleScene(SceneTypes.Game);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            _rpc.SyncAllMaster();
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