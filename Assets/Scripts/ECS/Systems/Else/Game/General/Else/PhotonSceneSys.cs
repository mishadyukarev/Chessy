using Leopotam.Ecs;
using Photon;

namespace Assets.Scripts
{
    public sealed class PhotonSceneSys : PunBehaviour, IEcsInitSystem
    {
        public void Init()
        {

        }


        public override sealed void OnLeftRoom()
        {
            base.OnLeftRoom();

            Main.ToggleScene(SceneTypes.Menu);
        }

        public override sealed void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            base.OnPhotonPlayerDisconnected(otherPlayer);

            PhotonNetwork.LeaveRoom();
            Main.ToggleScene(SceneTypes.Menu);
        }

        public override sealed void OnMasterClientSwitched(PhotonPlayer newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            PhotonNetwork.LeaveRoom();
        }







        public override sealed void OnJoinedRoom()
        {
            Main.ToggleScene(SceneTypes.Game);
        }

        public override sealed void OnDisconnectedFromPhoton()
        {
            base.OnDisconnectedFromPhoton();

            PhotonNetwork.offlineMode = true;
        }
    }
}