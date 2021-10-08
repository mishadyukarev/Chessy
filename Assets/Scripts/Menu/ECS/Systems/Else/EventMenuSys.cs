﻿using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using Scripts.Common;
using UnityEditor;
using UnityEngine;

namespace Scripts.Menu
{
    internal sealed class EventMenuSys : IEcsInitSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom, BackgroundMenuUICom> _rightZoneFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom, BackgroundMenuUICom> _leftZoneFilter = default;
        private EcsFilter<DownZoneUIMenuCom> _downZoneUIFilt = default;
        private EcsFilter<ShopZoneUIMenuCom> _shopZoneUIFilt = default;

        private const byte MAX_PLAYERS = 2;

        public void Init()
        {
            ref var downZoneUICom = ref _downZoneUIFilt.Get1(0);

            ref var rightConnectCom = ref _rightZoneFilter.Get1(0);
            ref var rightOnlineCom = ref _rightZoneFilter.Get2(0);



            downZoneUICom.AddListHelp_Button(Help);
            downZoneUICom.AddListQuit_Button(delegate { Application.Quit(); });


            rightConnectCom.AddListConnect_Button(ConnectOnline);
            rightOnlineCom.AddListCreatePublicRoom(CreateRoom);
            rightOnlineCom.AddListJoinRandomPublicRoom(JoinRandomRoom);

            rightOnlineCom.AddListCreateFriendRoom(CreateFriendRoom);
            rightOnlineCom.AddListJoinFriendRoom(JoinFriendRoom);


            _leftZoneFilter.Get1(0).AddListConnect_Button(ConnectOffline);
            _leftZoneFilter.Get2(0).AddListTrain(delegate { CreateOffGame(GameModes.TrainingOff); });
            _leftZoneFilter.Get2(0).AddListFriend(delegate { CreateOffGame(GameModes.WithFriendOff); });


            _shopZoneUIFilt.Get1(0).AddListExit_Button(ExitShop);
        }


        private void ConnectOnline()
        {
            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Application.version;

            PhotonNetwork.ConnectUsingSettings();
        }

        private void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
            else PhotonNetwork.OfflineMode = true;
        }

        private void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();

            GameModesCom.CurGameMode = GameModes.PublicOn;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }

        private void CreateFriendRoom()
        {
            ref var rightOnlineUICom = ref _rightZoneFilter.Get2(0);
            var roomName = rightOnlineUICom.TextCreateFriendRoom;

            GameModesCom.CurGameMode = GameModes.WithFriendOn;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom()
        {
            GameModesCom.CurGameMode = GameModes.PublicOn;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom()
        {
            GameModesCom.CurGameMode = GameModes.WithFriendOn;
            PhotonNetwork.JoinRoom(_rightZoneFilter.Get2(0).TextJoinFriendRoom);
        }

        private void CreateOffGame(GameModes offGameMode)
        {
            GameModesCom.CurGameMode = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        private void Help()
        {
            _shopZoneUIFilt.Get1(0).EnableZone();
        }
        private void ExitShop()
        {
            _shopZoneUIFilt.Get1(0).DisableZone();
        }
    }
}