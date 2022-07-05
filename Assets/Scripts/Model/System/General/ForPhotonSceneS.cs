using Chessy.Model.Entity;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.System
{
    sealed partial class ForPhotonSceneS : SystemModelAbstract
    {
        internal ForPhotonSceneS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
        }

        public void OnLeftRoom()
        {
            _e.SceneT = SceneTypes.Menu;
            _e.NeedUpdateView = true;
        }
        public void OnPlayerLeftRoom(in Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
        public void OnMasterClientSwitched(in Player newMasterClient)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
        public void OnPlayerEnteredRoom(in Player newPlayer)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            _s.SyncDataM();
        }
        public void OnDisconnected()
        {
            PhotonNetwork.OfflineMode = true;
        }
    }
}