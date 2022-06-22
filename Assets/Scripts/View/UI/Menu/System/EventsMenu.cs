using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Game.System;
using Chessy.Menu.View.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Chessy.Menu
{
    public sealed class EventsMenu
    {
        private const byte MAX_PLAYERS = 2;

        public EventsMenu(EntitiesModelCommon eMC, EntitiesViewUIMenu eUIM)
        {
            eUIM.CenterE.DiscordButtonC.AddListener(() => Application.OpenURL(URLC.URL_DISCORD));
            eUIM.CenterE.LikeGameButtonC.AddListener(() => Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY));
            eUIM.CenterE.ExitButtonC.AddListener(() => Application.Quit());


            eUIM.OnlineZoneE.JoinButtonC.AddListener(ConnectOnline);
            eUIM.OnlineZoneE.CreatePublicRoomButtonC.AddListener(() => CreateRoom(eMC));
            eUIM.OnlineZoneE.JoinRandomPublicRoomButtonC.AddListener(() => JoinRandomRoom(eMC));
            eUIM.OnlineZoneE.CreateFriendRoomButtonC.AddListener(() => CreateFriendRoom(eUIM, eMC));
            eUIM.OnlineZoneE.JoinFriendRoomButtonC.AddListener(() => JoinFriendRoom(eUIM, eMC));


            eUIM.OfflineZoneE.JoinButtonC.AddListener(ConnectOffline);
            eUIM.OfflineZoneE.TrainingButtonC.AddListener(() => CreateOffGame(eMC, GameModeTypes.TrainingOffline));
            eUIM.OfflineZoneE.WithFriendButtonC.AddListener(() => CreateOffGame(eMC, GameModeTypes.WithFriendOffline));



            //LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            //LikeGameUICom.AddListenerExit_But(ExitLikeGame);



            eUIM.CenterE.BookButtonC.AddListener(delegate
            {
                eMC.SoundActionC(eMC.BookC.IsOpenedBook() ? ClipCommonTypes.OpenBook : ClipCommonTypes.CloseBook).Invoke();

                eMC.NeedUpdateView = true;
            });

            eUIM.CenterE.SettingsButtonC.AddListener(delegate
            {
                eMC.SettingsC.IsOpenedBarWithSettings = !eMC.SettingsC.IsOpenedBarWithSettings;
                eMC.NeedUpdateView = true;
            });

        }


        private void ConnectOnline()
        {
            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.BestRegionSummaryFromStorage = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Application.version;

            PhotonNetwork.ConnectUsingSettings();
        }

        private void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
            else PhotonNetwork.OfflineMode = true;
        }

        private void CreateRoom(in EntitiesModelCommon e)
        {
            RoomOptions roomOptions = new RoomOptions();

            e.GameModeT = GameModeTypes.PublicOnline;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }

        private void CreateFriendRoom(in EntitiesViewUIMenu eUIM, in EntitiesModelCommon e)
        {
            var roomName = eUIM.OnlineZoneE.CreateFriendRoomInputFieldC.InputField.text;

            e.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom(in EntitiesModelCommon e)
        {
            e.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom(in EntitiesViewUIMenu eUIM, in EntitiesModelCommon e)
        {
            e.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(eUIM.OnlineZoneE.JoinFriendRoomInputFieldC.InputField.text);
        }

        private void CreateOffGame(in EntitiesModelCommon e, in GameModeTypes offGameMode)
        {
            e.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }
    }
}
