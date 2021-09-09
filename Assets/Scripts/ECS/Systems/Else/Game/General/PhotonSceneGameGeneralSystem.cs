using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;

namespace Assets.Scripts
{
    public sealed class PhotonSceneGameGeneralSystem : MonoBehaviourPunCallbacks, IEcsInitSystem
    {
        public void Init()
        {

        }


        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            Main.ToggleScene(SceneTypes.Menu);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PhotonNetwork.LeaveRoom();

            //Main.ToggleScene(SceneTypes.Menu);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            PhotonNetwork.LeaveRoom();
            //Main.ToggleScene(SceneTypes.Menu);
        }
    }
}