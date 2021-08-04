using Assets.Scripts.ECS.System.View.Common;
using Assets.Scripts.ECS.System.View.Menu;
using Assets.Scripts.Workers.Common;
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
            _logTex = MainCommonViewSys.CanvasEnt_CanvasCom.FindUnderParent<TextMeshProUGUI>(SceneTypes.Menu, "LogText");
            MainCommonViewSys.CanvasEnt_CanvasCom.FindUnderParent<Button>(Main.Instance.SceneType, "QuitButton").onClick.AddListener(delegate { Application.Quit(); });


            UIMenuMainViewSys.JoinOnlineEnt_ButtonCom.Button.onClick.AddListener(ConnectOnline);
            UIMenuMainViewSys.JoinOfflineEnt_ButtonCom.Button.onClick.AddListener(ConnectOffline);
            UIMenuMainViewSys.CreateRoomEnt_ButtonCom.Button.onClick.AddListener(CreateRoom);
            UIMenuMainViewSys.JoinRandomRoomEnt_ButtonCom.Button.onClick.AddListener(JoinRandomRoom);
            UIMenuMainViewSys.TestSoloGameEnt_ButtonCom.Button.onClick.AddListener(CreateTestSoloGame);

            UIMenuMainViewSys.CreateFriendRoomEnt_ButtonCom
                .Button.onClick.AddListener(delegate { CreateFriendRoom(UIMenuMainViewSys.CreateFriendRoomEnt_InputFieldCom.TMP_InputField.text); });

            UIMenuMainViewSys.JoinFriendRoomEnt_ButtonCom
                .Button.onClick.AddListener(delegate { JoinFriendRoom(UIMenuMainViewSys.JoinFriendRoomEnt_InputFieldCom.TMP_InputField.text); });
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
            roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), UIMenuMainViewSys.StepModUIEnt_DropDownTMPCom.StepModValue } };


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
            Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), UIMenuMainViewSys.StepModUIEnt_DropDownTMPCom.StepModValue } };
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
                UIMenuMainViewSys.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(true);
                UIMenuMainViewSys.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(true);
                UIMenuMainViewSys.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(true);

                SetTextOnUpLog("Offline");
                UIMenuMainViewSys.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(false);
                UIMenuMainViewSys.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(false);
            }
            else
            {
                UIMenuMainViewSys.JoinOfflineEnt_ButtonCom.Button.gameObject.SetActive(true);
                UIMenuMainViewSys.OfflineLeftZoneEnt_ImageCom.Image.gameObject.SetActive(true);

                SetTextOnUpLog("Online");
                UIMenuMainViewSys.OnlineRightZoneEnt_ImageCom.Image.gameObject.SetActive(false);
                UIMenuMainViewSys.JoinOnlineEnt_ButtonCom.Button.gameObject.SetActive(false);
                UIMenuMainViewSys.StepModUIEnt_DropDownTMPCom.TMP_Dropdown.gameObject.SetActive(false);
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            ConnectUsingSettingsWithData(true);
        }
    }
}
