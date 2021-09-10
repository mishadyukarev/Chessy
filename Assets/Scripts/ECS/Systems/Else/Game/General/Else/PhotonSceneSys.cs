using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Manager.View.Menu;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PhotonSceneSys : MonoBehaviourPunCallbacks, IEcsInitSystem
    {
        private MenuSystemManager _menuSystemManager;


        public void Init()
        {

        }

        internal void ToggleScene(SceneTypes sceneType, MenuSystemManager menuSystemManager)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new System.Exception();

                case SceneTypes.Menu:
                    _menuSystemManager = menuSystemManager;
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new System.Exception();
            }
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
            _menuSystemManager.PhotonSceneMenuSystem.ConnectedToMaster();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            _menuSystemManager.PhotonSceneMenuSystem.ConnectUsingSettingsWithData(true);
        }
    }
}