using Assets.Scripts.ECS.Manager.View.Menu;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PhotonSceneSys : MonoBehaviourPunCallbacks, IEcsInitSystem
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
            base.OnPlayerLeftRoom(otherPlayer);

            PhotonNetwork.LeaveRoom();

            //Main.ToggleScene(SceneTypes.Menu);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);


            //PhotonViewComponent.PhotonView = new GameObject().AddComponent<PhotonView>();
            //PhotonViewComponent.PhotonView.ViewID = ValuesConst.NUMBER_PHOTON_VIEW;

            //PhotonViewComponent.PhotonView.ViewID = 1002;

            //PhotonViewComponent

            //PhotonViewComponent.PhotonView. SetControllerInternal(newMasterClient.ActorNumber);

            PhotonNetwork.LeaveRoom();
            //Main.ToggleScene(SceneTypes.Menu);
        }











        public override void OnConnected()
        {
            base.OnConnected();

            OnConnectedToMaster();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
        }
        public override void OnJoinedRoom()
        {
            Main.ToggleScene(SceneTypes.Game);
        }

        public override void OnConnectedToMaster()
        {
            MenuSystemManager.ConnectToMasterMenuSys.Run();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
            MenuSystemManager.ConnUsingSettingsMenuSys.Run();
        }
    }
}