using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Enum;
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
            eUIM.OnlineZoneE.CreatePublicRoomButtonC.AddListener(() => CreateRoom(ref eMC.GameModeTC));
            eUIM.OnlineZoneE.JoinRandomPublicRoomButtonC.AddListener(() => JoinRandomRoom(ref eMC.GameModeTC));
            eUIM.OnlineZoneE.CreateFriendRoomButtonC.AddListener(() => CreateFriendRoom(eUIM, ref eMC.GameModeTC));
            eUIM.OnlineZoneE.JoinFriendRoomButtonC.AddListener(() => JoinFriendRoom(eUIM, ref eMC.GameModeTC));


            eUIM.OfflineZoneE.JoinButtonC.AddListener(ConnectOffline);
            eUIM.OfflineZoneE.TrainingButtonC.AddListener(() => CreateOffGame(ref eMC.GameModeTC, GameModeTypes.TrainingOffline));
            eUIM.OfflineZoneE.WithFriendButtonC.AddListener(() => CreateOffGame(ref eMC.GameModeTC, GameModeTypes.WithFriendOffline));



            //LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            //LikeGameUICom.AddListenerExit_But(ExitLikeGame);



            eUIM.CenterE.BookButtonC.AddListener(delegate
            {
                eMC.IsOpenedBook = !eMC.IsOpenedBook;
                eMC.SoundActionC(eMC.IsOpenedBook ? ClipCommonTypes.OpenBook : ClipCommonTypes.CloseBook).Invoke();
            });

            eUIM.CenterE.SettingsButtonC.AddListener(delegate
            {
                eMC.IsOpenSettings = !eMC.IsOpenSettings;

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

        private void CreateRoom(ref GameModeTC gameModeC)
        {
            RoomOptions roomOptions = new RoomOptions();

            gameModeC.GameModeT = GameModeTypes.PublicOnline;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }

        private void CreateFriendRoom(in EntitiesViewUIMenu eUIM, ref GameModeTC gameModeC)
        {
            var roomName = eUIM.OnlineZoneE.CreateFriendRoomInputFieldC.InputField.text;

            gameModeC.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom(ref GameModeTC gameModeC)
        {
            gameModeC.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom(in EntitiesViewUIMenu eUIM, ref GameModeTC gameModeC)
        {
            gameModeC.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(eUIM.OnlineZoneE.JoinFriendRoomInputFieldC.InputField.text);
        }

        private void CreateOffGame(ref GameModeTC gameModeC, in GameModeTypes offGameMode)
        {
            gameModeC.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }
    }
}
