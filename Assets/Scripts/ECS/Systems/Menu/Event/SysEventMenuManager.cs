using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.View.Menu;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Managers.Event
{
    internal sealed class SysEventMenuManager : MonoBehaviourPunCallbacks, IEcsInitSystem, IEcsRunSystem
    {
        private TextMeshProUGUI _logTex;
        private const byte MAX_PLAYERS = 2;

        public void Init()
        {
            _logTex = MainCommonSystem.CanvasEnt_CanvasCom.FindUnderParent<TextMeshProUGUI>("LogText");
            MainCommonSystem.CanvasEnt_CanvasCom.FindUnderParent<Button>("QuitButton").onClick.AddListener(delegate { Application.Quit(); });


            MainMenuSystem.JoinOnlineEnt_ButtonCom.Button.onClick.AddListener(ConnectOnline);
            MainMenuSystem.JoinOfflineEnt_ButtonCom.Button.onClick.AddListener(ConnectOffline);
            MainMenuSystem.CreateRoomEnt_ButtonCom.Button.onClick.AddListener(CreateRoom);
            MainMenuSystem.JoinRandomRoomEnt_ButtonCom.Button.onClick.AddListener(JoinRandomRoom);
            MainMenuSystem.TestSoloGameEnt_ButtonCom.Button.onClick.AddListener(CreateTestSoloGame);

            MainMenuSystem.CreateFriendRoomEnt_ButtonCom
                .Button.onClick.AddListener(delegate { CreateFriendRoom(MainMenuSystem.CreateFriendRoomEnt_InputFieldCom.TMP_InputField.text); });

            MainMenuSystem.JoinFriendRoomEnt_ButtonCom
                .Button.onClick.AddListener(delegate { JoinFriendRoom(MainMenuSystem.JoinFriendRoomEnt_InputFieldCom.TMP_InputField.text); });
        }

        public void Run()
        {

        }


        public override void OnConnected()
        {
            base.OnConnected();

            OnConnectedToMaster();
        }

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
            roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), MainMenuSystem.StepModUIEnt_DropDownTMPCom.StepModValue } };


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
            Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), MainMenuSystem.StepModUIEnt_DropDownTMPCom.StepModValue } };
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

        private void ConnectUsingSettingsWithData(bool isOffline)
        {
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = isOffline;

            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Main.VERSION_PHOTON_GAME;
            PhotonNetwork.PhotonServerSettings.name = "Player " + UnityEngine.Random.Range(1, 999999);

            PhotonNetwork.ConnectUsingSettings();
        }

        internal void SetTextOnUpLog(string message) => _logTex.text = message;









        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.OfflineMode)
            {
                MainMenuSystem.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(true);
                MainMenuSystem.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(true);
                MainMenuSystem.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(true);

                SetTextOnUpLog("Offline");
                MainMenuSystem.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(false);
                MainMenuSystem.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(false);
            }
            else
            {
                MainMenuSystem.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(true);
                MainMenuSystem.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(true);

                SetTextOnUpLog("Online");
                MainMenuSystem.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(false);
                MainMenuSystem.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(false);
                MainMenuSystem.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(false);
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            ConnectUsingSettingsWithData(true);
        }
    }
}
