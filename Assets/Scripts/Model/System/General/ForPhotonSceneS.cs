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

        internal void OnLeftRoom()
        {
            _aboutGameC.SceneT = SceneTypes.Menu;
            _e.NeedUpdateView = true;
        }
        internal void OnPlayerLeftRoom(in Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
        internal void OnMasterClientSwitched(in Player newMasterClient)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
        internal void OnPlayerEnteredRoom(in Player newPlayer)
        {
            //foreach (var photonV in PhotonNetwork.PhotonViews)
            //{
            //    photonV.ControllerActorNr = 1;
            //    //PhotonNetwork.AllocateViewID(photonV);
            //}

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        internal void OnDisconnected()
        {
            //PhotonNetwork.OfflineMode = true;
        }
        internal void OnConnected()
        {
            _e.NeedUpdateView = true;
        }
        internal void OnConnectedToMaster()
        {
            _e.NeedUpdateView = true;
        }
        internal void OnJoinedRoom()
        {
            _s.ToggleScene(SceneTypes.Game);
            _s.StartGame(_aboutGameC.GameModeT == GameModeTypes.TrainingOffline);

            //if (PhotonNetwork.IsMasterClient)
            //{
            foreach (var photonV in PhotonNetwork.PhotonViews)
            {
                var masterActorNr = PhotonNetwork.MasterClient.ActorNumber;

                photonV.ControllerActorNr = masterActorNr;
                photonV.OwnerActorNr = masterActorNr;
            }
            //}
        }
    }
}