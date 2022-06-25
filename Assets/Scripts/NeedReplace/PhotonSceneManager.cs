using Chessy.Model;
using Chessy.Model.System.View.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Common
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {
        SystemsModel _sMG;
        SystemsViewUI _sUI;



        public void StartMy(in SystemsViewUI sUIC, in SystemsModel sMG)
        {
            _sMG = sMG;
            _sUI = sUIC;
        }


        public override sealed void OnLeftRoom() => _sMG.OnLeftRoom();

        //public override sealed void OnPhotonPlayerDisconnected(Player otherPlayer)
        //{
        //    base.OnPhotonPlayerDisconnected(otherPlayer);

        //    PhotonNetwork.LeaveRoom();
        //    SpawnInitComSys.ToggleScene(SceneTypes.Menu);
        //}
        public override void OnPlayerLeftRoom(Player otherPlayer) => _sMG.OnPlayerLeftRoom(otherPlayer);

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