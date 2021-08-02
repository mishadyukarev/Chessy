using Assets.Scripts.ECS.Menu.Entities;
using Assets.Scripts.Workers.Common;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonScene : MonoBehaviourPunCallbacks
    {
        #region Menu

        private EntViewMenuElseManager _entMenuManager;

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
                    throw new Exception();
            }
        }


        internal void ToggleScene(SceneTypes sceneType, EntViewMenuElseManager entMenuManager)
        {
            _entMenuManager = entMenuManager;

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _logTex = ViewCommonContainerUICanvas.FindUnderParent<TextMeshProUGUI>(SceneTypes.Menu, "LogText");
                    ViewCommonContainerUICanvas.FindUnderParent<Button>(sceneType, "QuitButton").onClick.AddListener(delegate { Application.Quit(); });


                    _entMenuManager.JoinOnlineEnt_ButtonCom.Button.onClick.AddListener(ConnectOnline);
                    _entMenuManager.JoinOfflineEnt_ButtonCom.Button.onClick.AddListener(ConnectOffline);
                    _entMenuManager.CreateRoomEnt_ButtonCom.Button.onClick.AddListener(CreateRoom);
                    _entMenuManager.JoinRandomRoomEnt_ButtonCom.Button.onClick.AddListener(JoinRandomRoom);
                    _entMenuManager.TestSoloGameEnt_ButtonCom.Button.onClick.AddListener(CreateTestSoloGame);

                    _entMenuManager.CreateFriendRoomEnt_ButtonCom
                        .Button.onClick.AddListener(delegate { CreateFriendRoom(_entMenuManager.CreateFriendRoomEnt_InputFieldCom.TMP_InputField.text); });

                    _entMenuManager.JoinFriendRoomEnt_ButtonCom
                        .Button.onClick.AddListener(delegate { JoinFriendRoom(_entMenuManager.JoinFriendRoomEnt_InputFieldCom.TMP_InputField.text); });
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
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

            roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _entMenuManager.StepModUIEnt_DropDownTMPCom.StepModValue } };


            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public void CreateFriendRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        public void JoinRandomRoom()
        {
            Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _entMenuManager.StepModUIEnt_DropDownTMPCom.StepModValue } };
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, MAX_PLAYERS);
        }

        private void JoinFriendRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        private void CreateTestSoloGame()
        {
            PhotonNetwork.CreateRoom(default);
        }
        internal static void LeaveRoom()
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
                _entMenuManager.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(true);
                _entMenuManager.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(true);
                _entMenuManager.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(true);

                SetTextOnUpLog("Offline");
                _entMenuManager.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(false);
                _entMenuManager.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(false);
            }
            else
            {
                _entMenuManager.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(true);
                _entMenuManager.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(true);

                SetTextOnUpLog("Online");
                _entMenuManager.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(false);
                _entMenuManager.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(false);
                _entMenuManager.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(false);
            }
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
            Instance.ToggleScene(SceneTypes.Menu);
        }




        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            LeaveRoom();
        }

        #endregion
    }
}