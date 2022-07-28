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
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyFromMarketBuildingM), marketBuyT });
        }
        public void ClickFriendReadyButtonInGame()
        {
            _zonesInfoC.IsActiveFriend = false;
            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickWindButtonUp()
        {
            if (AboutGameC.LessonT == LessonTypes.ClickWindInfo)
            {
                SunC.SunSideT = SunSideTypes.Dawn;
                _s.SetNextLesson();
            }
            else
            {
                if (_bookC.IsOpenedBook())
                {
                    _bookC.OpenedNowPageBookT = PageBookTypes.None;
                    _dataFromViewC.SoundAction(ClipTypes.CloseBook).Invoke();
                }
                else
                {
                    _bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                    _dataFromViewC.SoundAction(ClipTypes.OpenBook).Invoke();
                }
            }

            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickSettingUpGame()
        {
            _settingsC.IsOpenedBarWithSettings = !_settingsC.IsOpenedBarWithSettings;
            _dataFromViewC.SoundAction(ClipTypes.Click);
        }
        public void ClickDiscordUpButton()
        {
            Application.OpenURL(URLC.URL_DISCORD);
        }
        public void ClickSettingsCenterMenu()
        {
            _settingsC.IsOpenedBarWithSettings = !_settingsC.IsOpenedBarWithSettings;
            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickBuyPremiumProduct()
        {
            if (_shopC.IsInitialized) //если покупка инициализирована 
            {
                var product = _shopC.StoreController.products.WithID(ShopValues.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _shopC.StoreController.InitiatePurchase(product); //покупаем
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
            if (_bookC.IsOpenedBook)
            {
                _bookC.CloseBook();
            }
            else
            {
                _bookC.OpenBook();
            }

            _dataFromViewC.SoundAction(_bookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();
            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickPremiumButtonInHeroZoneInGame()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            _shopC.IsOpenedShopZone = true;
            _updateAllViewC.NeedUpdateView = true;

            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickExitSmelterBarInGame()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            _selectedBuildingsInTownC.Set(BuildingTypes.Smelter, false);

            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickExitMarketBarInGame()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            _selectedBuildingsInTownC.Set(BuildingTypes.Market, false);

            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickPremiumButtonLeftInGame()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            _shopC.IsOpenedShopZone = true;
            _updateAllViewC.NeedUpdateView = true;
        }
        public void ClickLeaveButtonInGame()
        {
            PhotonNetwork.LeaveRoom();
        }


        #region BookZoneButtons

        public void ClickNextButtonInBookZone()
        {
            if (_bookC.OpenedNowPageBookT < PageBookTypes.End - 1)
            {
                _bookC.OpenedNowPageBookT++;
                _dataFromViewC.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _updateAllViewC.NeedUpdateView = true;
            }
        }
        public void ClickBackButtonInBookZone()
        {
            if (_bookC.OpenedNowPageBookT > 0)
            {
                _bookC.OpenedNowPageBookT--;
                _dataFromViewC.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _updateAllViewC.NeedUpdateView = true;
            }
        }
        public void ClickExitBookInBookZone()
        {
            _bookC.WasOpenedBookT = _bookC.OpenedNowPageBookT;
            _bookC.OpenedNowPageBookT = PageBookTypes.None;

            _dataFromViewC.SoundAction(ClipTypes.CloseBook).Invoke();

            _updateAllViewC.NeedUpdateView = true;
        }

        #endregion


        #region SettingsZone

        public void ClickExitInOpenedSettingBarZone()
        {
            _settingsC.IsOpenedBarWithSettings = false;
            //eVCommon.Sound(ClipTypes.Click).Play();

            _updateAllViewC.NeedUpdateView = true;
        }

        #endregion


        #region ShopZone

        public void ClickExitShopInShopZone()
        {
            _shopC.IsOpenedShopZone = false;
        }

        #endregion



        #region Menu

        public void ClickOpenBookCenterInMenu()
        {
            _bookC.TryOpenBook();

            _dataFromViewC.SoundAction(_bookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();

            _updateAllViewC.NeedUpdateView = true;
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

            AboutGameC.GameModeT = GameModeTypes.PublicOnline;

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
            AboutGameC.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomNameFromViewBar, roomOptions, default);
        }
        public void ClickJoinRandomPublicRoomInMenu()
        {
            AboutGameC.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }
        public void ClickJoinFriendRoomInMenu(in string nameRoomFromViewBar)
        {
            AboutGameC.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(nameRoomFromViewBar);
        }

        public void ClickCreateOffGameInMenu(in GameModeTypes offGameMode)
        {
            AboutGameC.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        #endregion
    }
}