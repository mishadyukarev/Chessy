using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Manager.View.Menu;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.ECS.Managers.Event
{
    internal sealed class EventMenuSys : IEcsInitSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom, BackgroundMenuUICom> _rightZoneFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom, BackgroundMenuUICom> _leftZoneFilter = default;
        private EcsFilter<CenterMenuUICom> _centerUIZoneFilter = default;

        public void Init()
        {
            ref var centerCom = ref _centerUIZoneFilter.Get1(0);

            ref var rightConnectCom = ref _rightZoneFilter.Get1(0);
            ref var rightOnlineCom = ref _rightZoneFilter.Get2(0);

            centerCom.AddListQuit_Button(delegate { Application.Quit(); });

            rightConnectCom.AddListConnect_Button(ConnectOnline);
            rightOnlineCom.AddListCreatePublicRoom(CreateRoom);
            rightOnlineCom.AddListJoinRandomPublicRoom(JoinRandomRoom);

            rightOnlineCom.AddListCreateFriendRoom(CreateFriendRoom);
            rightOnlineCom.AddListJoinFriendRoom(JoinFriendRoom);


            _leftZoneFilter.Get1(0).AddListConnect_Button(ConnectOffline);
            _leftZoneFilter.Get2(0).AddListTrain(delegate { CreateOffGame(GameModes.TrainingOff); });
            _leftZoneFilter.Get2(0).AddListFriend(delegate { CreateOffGame(GameModes.WithFriendOff); });
        }


        private void ConnectOnline()
        {
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;
            MenuSystemManager.ConnUsingSettingsMenuSys.Run();
        }

        private void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
            else
            {
                PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
                MenuSystemManager.ConnUsingSettingsMenuSys.Run();
            }
        }

        private void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();

            GameModesCom.GameMode = GameModes.PublicOn;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };


            roomOptions.MaxPlayers = Main.MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        private void CreateFriendRoom()
        {
            ref var rightOnlineUICom = ref _rightZoneFilter.Get2(0);
            var roomName = rightOnlineUICom.TextCreateFriendRoom;

            GameModesCom.GameMode = GameModes.WithFriendOn;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = Main.MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom()
        {
            GameModesCom.GameMode = GameModes.PublicOn;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom()
        {
            GameModesCom.GameMode = GameModes.WithFriendOn;
            PhotonNetwork.JoinRoom(_rightZoneFilter.Get2(0).TextJoinFriendRoom);
        }

        private void CreateOffGame(GameModes offGameMode)
        {
            GameModesCom.GameMode = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }
    }
}
