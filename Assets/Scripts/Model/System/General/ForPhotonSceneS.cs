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
            aboutGameC.SceneT = SceneTypes.Menu;
            updateAllViewC.NeedUpdateView = true;
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
            updateAllViewC.NeedUpdateView = true;
        }
        internal void OnConnectedToMaster()
        {
            updateAllViewC.NeedUpdateView = true;
        }
        internal void OnJoinedRoom()
        {
            s.ToggleScene(SceneTypes.Game);
            s.StartGame(aboutGameC.GameModeT == GameModeTypes.TrainingOffline);

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