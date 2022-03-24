using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
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
            eUIM.CenterE.DiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });
            eUIM.CenterE.LikeGameButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            eUIM.CenterE.ExitButtonC.AddListener(delegate { Application.Quit(); });


            ConnectorUIC.AddListConnect_Button(ConnectOnline);
            OnZoneUIC.AddListCreatePublicRoom(delegate { CreateRoom(ref eMC.GameModeTC); });
            OnZoneUIC.AddListJoinRandomPublicRoom(delegate { JoinRandomRoom(ref eMC.GameModeTC); });

            OnZoneUIC.AddListCreateFriendRoom(delegate { CreateFriendRoom(ref eMC.GameModeTC); });
            OnZoneUIC.AddListJoinFriendRoom(delegate { JoinFriendRoom(ref eMC.GameModeTC); });


            ConUIC.AddListConnect_Button(ConnectOffline);
            OffZoneUIC.AddListTrain(delegate { CreateOffGame(ref eMC.GameModeTC, GameModes.TrainingOff); });
            OffZoneUIC.AddListFriend(delegate { CreateOffGame(ref eMC.GameModeTC, GameModes.WithFriendOff); });



            //LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            //LikeGameUICom.AddListenerExit_But(ExitLikeGame);



            eUIM.CenterE.BookButtonC.AddListener(delegate
            {
                eMC.BookE.IsOpenedBook = !eMC.BookE.IsOpenedBook;
                eMC.SoundActionC(eMC.BookE.IsOpenedBook ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();
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

            gameModeC.GameMode = GameModes.PublicOn;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }

        private void CreateFriendRoom(ref GameModeTC gameModeC)
        {
            var roomName = OnZoneUIC.TextCreateFriendRoom;

            gameModeC.GameMode = GameModes.WithFriendOn;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom(ref GameModeTC gameModeC)
        {
            gameModeC.GameMode = GameModes.PublicOn;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom(ref GameModeTC gameModeC)
        {
            gameModeC.GameMode = GameModes.WithFriendOn;
            PhotonNetwork.JoinRoom(OnZoneUIC.TextJoinFriendRoom);
        }

        private void CreateOffGame(ref GameModeTC gameModeC, in GameModes offGameMode)
        {
            gameModeC.GameMode = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        private void ShopZone(in ShopUIE shopUIE)
        {
            shopUIE.ShopZoneGOC.SetActive(true);
        }
        private void ExitLikeGame()
        {
            //LikeGameUICom.SetActiveZone(false);
        }
    }
}
