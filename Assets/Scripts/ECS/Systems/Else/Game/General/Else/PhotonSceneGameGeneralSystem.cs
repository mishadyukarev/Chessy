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



        internal static void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
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
            LeaveRoom();
            Main.ToggleScene(SceneTypes.Menu);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            LeaveRoom();
        }
    }
}