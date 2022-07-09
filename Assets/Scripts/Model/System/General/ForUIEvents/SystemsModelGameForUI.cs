using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel : SystemModelAbstract
    {
        const byte MAX_PLAYERS = 2;

        internal ForButtonsSystemsModel(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG) { }

        public void TryBuyFromMarketBuilding(in MarketBuyTypes marketBuyT)
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyFromMarketBuildingM), marketBuyT });
        }
        public void ClickFriendReadyButtonInGame()
        {
            _e.ZoneInfoC.IsActiveFriend = false;
            _e.NeedUpdateView = true;
        }
        public void ClickWindButtonUp()
        {
            if (_e.LessonT == LessonTypes.ClickWindInfo)
            {
                _e.SunSideT = SunSideTypes.Dawn;
                 _s.SetNextLesson();
            }
            else
            {
                if (_e.BookC.IsOpenedBook())
                {
                    _e.OpenedNowPageBookT = PageBookTypes.None;
                    _e.SoundAction(ClipTypes.CloseBook).Invoke();
                }
                else
                {
                    _e.OpenedNowPageBookT = PageBookTypes.Wind;
                    _e.SoundAction(ClipTypes.OpenBook).Invoke();
                }
            }

            _e.NeedUpdateView = true;
        }
        public void ClickSettingUpGame()
        {
            _e.SettingsC.IsOpenedBarWithSettings = !_e.SettingsC.IsOpenedBarWithSettings;
            _e.SoundAction(ClipTypes.Click);
        }
        public void ClickDiscordUpButton()
        {
            Application.OpenURL(URLC.URL_DISCORD);
        }
        public void ClickSettingsCenterMenu()
        {
            _e.SettingsC.IsOpenedBarWithSettings = !_e.SettingsC.IsOpenedBarWithSettings;
            _e.NeedUpdateView = true;
        }
        public void ClickBuyPremiumProduct()
        {
            if (_e.ShopC.IsInitialized) //если покупка инициализирована 
            {
                var product = _e.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _e.ShopC.StoreController.InitiatePurchase(product); //покупаем
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        public void ClickLittleBookDownInGame()
        {
            if (_e.BookC.IsOpenedBook)
            {
                _e.BookC.CloseBook();
            }
            else
            {
                _e.BookC.OpenBook();
            }

            _e.SoundAction(_e.BookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();
            _e.NeedUpdateView = true;
        }
        public void ClickPremiumButtonInHeroZoneInGame()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.ShopC.IsOpenedShopZone = true;
            _e.NeedUpdateView = true;

            _e.NeedUpdateView = true;
        }
        public void ClickExitSmelterBarInGame()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.SelectedE.BuildingsC.Set(BuildingTypes.Smelter, false);

            _e.NeedUpdateView = true;
        }
        public void ClickExitMarketBarInGame()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.SelectedE.BuildingsC.Set(BuildingTypes.Market, false);

            _e.NeedUpdateView = true;
        }
        public void ClickPremiumButtonLeftInGame()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.ShopC.IsOpenedShopZone = true;
            _e.NeedUpdateView = true;
        }
        public void ClickLeaveButtonInGame()
        {
            PhotonNetwork.LeaveRoom();
        }


        #region BookZoneButtons

        public void ClickNextButtonInBookZone()
        {
            if (_e.OpenedNowPageBookT < PageBookTypes.End - 1)
            {
                _e.OpenedNowPageBookT++;
                _e.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _e.NeedUpdateView = true;
            }
        }
        public void ClickBackButtonInBookZone()
        {
            if (_e.OpenedNowPageBookT > 0)
            {
                _e.OpenedNowPageBookT--;
                _e.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _e.NeedUpdateView = true;
            }
        }
        public void ClickExitBookInBookZone()
        {
            _e.WasOpenedLastPageBookT = _e.OpenedNowPageBookT;
            _e.OpenedNowPageBookT = PageBookTypes.None;

            _e.SoundAction(ClipTypes.CloseBook).Invoke();

            _e.NeedUpdateView = true;
        }

        #endregion


        #region SettingsZone

        public void ClickExitInOpenedSettingBarZone()
        {
            _e.SettingsC.IsOpenedBarWithSettings = false;
            //eVCommon.Sound(ClipTypes.Click).Play();

            _e.NeedUpdateView = true;
        }

        #endregion


        #region ShopZone

        public void ClickExitShopInShopZone()
        {
            _e.ShopC.IsOpenedShopZone = false;
        }

        #endregion



        #region Menu

        public void ClickOpenBookCenterInMenu()
        {
            _e.SoundAction(_e.BookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();

            _e.NeedUpdateView = true;
        }

        public void ClickConnectOnlineMenu()
        {
            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.BestRegionSummaryFromStorage = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Application.version;

            PhotonNetwork.ConnectUsingSettings();
        }
        public void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
            else PhotonNetwork.OfflineMode = true;
        }

        public void ClickCreatePublickRoom()
        {
            RoomOptions roomOptions = new RoomOptions();

            _e.GameModeT = GameModeTypes.PublicOnline;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }
        public void ClickCreateFriendRoomInMenu(in string roomNameFromViewBar)
        {
            _e.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomNameFromViewBar, roomOptions, default);
        }
        public void ClickJoinRandomPublicRoomInMenu()
        {
            _e.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }
        public void ClickJoinFriendRoomInMenu(in string nameRoomFromViewBar)
        {
            _e.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(nameRoomFromViewBar);
        }

        public void ClickCreateOffGameInMenu(in GameModeTypes offGameMode)
        {
            _e.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        #endregion
    }
}