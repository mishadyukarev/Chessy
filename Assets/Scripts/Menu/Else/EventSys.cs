using Chessy.Common;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Chessy.Menu
{
    public sealed class EventSys
    {
        private const byte MAX_PLAYERS = 2;

        public EventSys()
        {
            CenterZoneUICom.AddListShop_But(ShopZone);

            CenterZoneUICom.AddListDiscord_But(delegate { Application.OpenURL(URLC.URL_DISCORD); });
            CenterZoneUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            CenterZoneUICom.AddListQuit_But(delegate { Application.Quit(); });


            ConnectorUIC.AddListConnect_Button(ConnectOnline);
            OnZoneUIC.AddListCreatePublicRoom(CreateRoom);
            OnZoneUIC.AddListJoinRandomPublicRoom(JoinRandomRoom);

            OnZoneUIC.AddListCreateFriendRoom(CreateFriendRoom);
            OnZoneUIC.AddListJoinFriendRoom(JoinFriendRoom);


            ConUIC.AddListConnect_Button(ConnectOffline);
            OffZoneUIC.AddListTrain(delegate { CreateOffGame(GameModes.TrainingOff); });
            OffZoneUIC.AddListFriend(delegate { CreateOffGame(GameModes.WithFriendOff); });



            LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            LikeGameUICom.AddListenerExit_But(ExitLikeGame);
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

        private void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();

            GameModeC.CurGameMode = GameModes.PublicOn;

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
            var roomName = OnZoneUIC.TextCreateFriendRoom;

            GameModeC.CurGameMode = GameModes.WithFriendOn;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom()
        {
            GameModeC.CurGameMode = GameModes.PublicOn;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom()
        {
            GameModeC.CurGameMode = GameModes.WithFriendOn;
            PhotonNetwork.JoinRoom(OnZoneUIC.TextJoinFriendRoom);
        }

        private void CreateOffGame(GameModes offGameMode)
        {
            GameModeC.CurGameMode = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        private void ShopZone()
        {
            ShopUIC.EnableZone();
        }
        private void ExitLikeGame()
        {
            LikeGameUICom.SetActiveZone(false);
        }
    }
}
