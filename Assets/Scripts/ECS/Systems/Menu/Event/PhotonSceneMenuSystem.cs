using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Assets.Scripts.ECS.System.Data.Common;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Managers.Event
{
    internal sealed class PhotonSceneMenuSystem : MonoBehaviourPunCallbacks, IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<ConnectButtonUIComponent, OnlineZoneUIComponent, BackgroundImagesUIComponent> _rightZoneFilter;
        private EcsFilter<ConnectButtonUIComponent, OfflineZoneUIComponent, BackgroundImagesUIComponent> _leftZoneFilter;
        private EcsFilter<CenterMenuUIComponent> _centerUIZoneFilter;

        private TextMeshProUGUI _logTex;
        private const byte MAX_PLAYERS = 2;


        public void Init()
        {
            ref var rightUIEnt = ref _rightZoneFilter.GetEntity(0);

            _logTex = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent<TextMeshProUGUI>("LogText");
            MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent<Button>("QuitButton").onClick.AddListener(delegate { Application.Quit(); });

            rightUIEnt.Get<ConnectButtonUIComponent>().AddListenerToConnectButton(ConnectOnline);
            rightUIEnt.Get<OnlineZoneUIComponent>().AddListenerCreatePublicRoom(CreateRoom);
            rightUIEnt.Get<OnlineZoneUIComponent>().AddListenerJoinRandomPublicRoom(JoinRandomRoom);

            rightUIEnt.Get<OnlineZoneUIComponent>().AddListenerCreateFriendRoom(CreateFriendRoom);
            rightUIEnt.Get<OnlineZoneUIComponent>().AddListenerJoinFriendRoom(JoinFriendRoom);


            ref var leftEnt = ref _leftZoneFilter.GetEntity(0);

            _leftZoneFilter.Get1(0).AddListenerToConnectButton(ConnectOffline);
            _leftZoneFilter.Get2(0).AddListenerToStartWithBotButton(CreateTestSoloGame);
        }

        public void Run()
        {

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
            roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };


            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public void CreateFriendRoom()
        {
            ref var rightOnlineUICom = ref _rightZoneFilter.Get2(0);
            var roomName = rightOnlineUICom.TextCreateFriendRoom;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        public void JoinRandomRoom()
        {
            Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, MAX_PLAYERS);
        }

        private void JoinFriendRoom()
        {
            PhotonNetwork.JoinRoom(_rightZoneFilter.Get2(0).TextJoinFriendRoom);
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
            Main.Instance.ToggleScene(SceneTypes.Game);
        }

        public override void OnConnectedToMaster()
        {
            ref var rightUIEnt_ConnectButtonUICom = ref _rightZoneFilter.Get1(0);
            ref var rightUIEnt_OnlineZoneUICom = ref _rightZoneFilter.Get2(0);
            ref var rightUIEnt_BackgroundImagesUICom = ref _rightZoneFilter.Get3(0);

            ref var leftUIEnt_ConnectButtonUICom = ref _leftZoneFilter.Get1(0);
            ref var leftUIEnt_BackgroundImagesUICom = ref _leftZoneFilter.Get3(0);

            if (PhotonNetwork.OfflineMode)
            {
                rightUIEnt_BackgroundImagesUICom.SetActiveFrontImage(true);
                rightUIEnt_ConnectButtonUICom.SetActiveButton(true);
                rightUIEnt_BackgroundImagesUICom.SetActiveFrontImage(true);

                SetTextOnUpLog("Offline");
                leftUIEnt_ConnectButtonUICom.SetActiveButton(false);
                leftUIEnt_BackgroundImagesUICom.SetActiveFrontImage(false);
            }
            else
            {
                leftUIEnt_ConnectButtonUICom.SetActiveButton(true);
                leftUIEnt_BackgroundImagesUICom.SetActiveFrontImage(true);

                SetTextOnUpLog("Online");
                rightUIEnt_BackgroundImagesUICom.SetActiveFrontImage(false);
                rightUIEnt_ConnectButtonUICom.SetActiveButton(false);
                rightUIEnt_BackgroundImagesUICom.SetActiveFrontImage(false);
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            ConnectUsingSettingsWithData(true);
        }
    }
}
