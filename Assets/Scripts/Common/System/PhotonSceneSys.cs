using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;

namespace Game.Common
{
    public sealed class PhotonSceneSys : MonoBehaviourPunCallbacks, IEcsInitSystem
    {
        public void Init()
        {

        }

        public override sealed void OnLeftRoom()
        {
            base.OnLeftRoom();

            DataSC.ToggleScene(SceneTypes.Menu);
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
            DataSC.ToggleScene(SceneTypes.Menu);
        }

        public override sealed void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            PhotonNetwork.LeaveRoom();
        }







        public override sealed void OnJoinedRoom()
        {
            DataSC.ToggleScene(SceneTypes.Game);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
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