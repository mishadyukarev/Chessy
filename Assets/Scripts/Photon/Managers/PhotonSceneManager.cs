using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {
        public const string VERSION_PHOTON_GAME = "0.1g";

        #region Menu

        private TextMeshProUGUI _logTex;
        private const byte MAX_PLAYERS = 2;
        //private LoadBalancingClient _loadBalancingClient = new LoadBalancingClient();
        private bool _inCreatingRoom;

        internal void Constructor()
        {

        }

        internal void OwnUpdate(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    break;
            }
        }


        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    _logTex = Instance.CanvasManager.FindUnderParent<TextMeshProUGUI>(SceneTypes.Menu, "LogText");
                    Instance.CanvasManager.FindUnderParent<Button>(sceneType, "QuitButton").onClick.AddListener(delegate { Application.Quit(); });


                    Instance.EntMenuM.JoinOnlineEnt_ButtonCom.AddListener(ConnectOnline);
                    Instance.EntMenuM.JoinOfflineEnt_ButtonCom.AddListener(ConnectOffline);
                    Instance.EntMenuM.CreateRoomEnt_ButtonCom.AddListener(CreateRoom);
                    Instance.EntMenuM.JoinRandomRoomEnt_ButtonCom.AddListener(JoinRandomRoom);
                    Instance.EntMenuM.TestSoloGameEnt_ButtonCom.AddListener(CreateTestSoloGame);

                    Instance.EntMenuM.CreateFriendRoomEnt_ButtonCom
                        .AddListener(delegate { CreateFriendRoom(Instance.EntMenuM.CreateFriendRoomEnt_InputFieldCom.Text); });

                    Instance.EntMenuM.JoinFriendRoomEnt_ButtonCom
                        .AddListener(delegate { JoinFriendRoom(Instance.EntMenuM.JoinFriendRoomEnt_InputFieldCom.Text); });
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    break;
            }
        }




        #region Buttons

        private void ConnectOnline()
        {
            ConnectUsingSettingsWithData(false);
        }

        private void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
            else
            {
                ConnectUsingSettingsWithData(true);
            }
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }

        public void CreateFriendRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        private void JoinFriendRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        private void CreateTestSoloGame()
        {
            PhotonNetwork.CreateRoom(default);
        }
        internal void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        private void ConnectUsingSettingsWithData(bool isOffline)
        {
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = isOffline;

            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = VERSION_PHOTON_GAME;
            PhotonNetwork.PhotonServerSettings.name = "Player " + UnityEngine.Random.Range(1, 999999);

            PhotonNetwork.ConnectUsingSettings();
        }

        internal void SetTextOnUpLog(string message) => _logTex.text = message;




        public override void OnConnected()
        {
            base.OnConnected();

            OnConnectedToMaster();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            ConnectUsingSettingsWithData(true);
        }

        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.OfflineMode)
            {
                Instance.EntMenuM.OnlineRightZoneEnt_ImageCom.SetActive(true);
                Instance.EntMenuM.JoinOnlineEnt_ButtonCom.SetActive(true);

                SetTextOnUpLog("Offline");
                Instance.EntMenuM.JoinOfflineEnt_ButtonCom.SetActive(false);
                Instance.EntMenuM.OfflineLeftZoneEnt_ImageCom.SetActive(false);
            }
            else
            {
                Instance.EntMenuM.JoinOfflineEnt_ButtonCom.SetActive(true);
                Instance.EntMenuM.OfflineLeftZoneEnt_ImageCom.SetActive(true);

                SetTextOnUpLog("Online");
                Instance.EntMenuM.OnlineRightZoneEnt_ImageCom.SetActive(false);
                Instance.EntMenuM.JoinOnlineEnt_ButtonCom.SetActive(false);
            }

            //PhotonNetwork.ConnectToRegion("ru");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
        }



        public override void OnJoinedRoom()
        {
            Instance.ToggleScene(SceneTypes.Game);
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            Instance.ToggleScene(SceneTypes.Menu);
        }



        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            LeaveRoom();
            ToggleScene(SceneTypes.Menu);
        }




        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            LeaveRoom();
        }

        #endregion
    }
}