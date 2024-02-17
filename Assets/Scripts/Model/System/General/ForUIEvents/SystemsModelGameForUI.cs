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
            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryBuyFromMarketBuildingM), marketBuyT });
        }
        public void ClickFriendReadyButtonInGame()
        {
            zonesInfoC.IsActiveFriend = false;
            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickWindButtonUp()
        {
            if (aboutGameC.LessonT == LessonTypes.ClickWindInfo)
            {
                sunC.SunSideT = SunSideTypes.Dawn;
                s.SetNextLesson();
            }
            else
            {
                if (bookC.IsOpenedBook())
                {
                    bookC.OpenedNowPageBookT = PageBookTypes.None;
                    dataFromViewC.SoundAction(ClipTypes.CloseBook).Invoke();
                }
                else
                {
                    bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                    dataFromViewC.SoundAction(ClipTypes.OpenBook).Invoke();
                }
            }

            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickSettingUpGame()
        {
            settingsC.IsOpenedBarWithSettings = !settingsC.IsOpenedBarWithSettings;
            dataFromViewC.SoundAction(ClipTypes.Click);
        }
        public void ClickDiscordUpButton()
        {
            Application.OpenURL(URLC.URL_DISCORD);
        }
        public void ClickSettingsCenterMenu()
        {
            settingsC.IsOpenedBarWithSettings = !settingsC.IsOpenedBarWithSettings;
            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickBuyPremiumProduct()
        {
            if (shopC.IsInitialized) //если покупка инициализирована 
            {
                var product = shopC.StoreController.products.WithID(ShopValues.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    shopC.StoreController.InitiatePurchase(product); //покупаем
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
            if (bookC.IsOpenedBook)
            {
                bookC.CloseBook();
            }
            else
            {
                bookC.OpenBook();
            }

            dataFromViewC.SoundAction(bookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();
            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickPremiumButtonInHeroZoneInGame()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            shopC.IsOpenedShopZone = true;
            updateAllViewC.NeedUpdateView = true;

            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickExitSmelterBarInGame()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            selectedBuildingsInTownC.Set(BuildingTypes.Smelter, false);

            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickExitMarketBarInGame()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            selectedBuildingsInTownC.Set(BuildingTypes.Market, false);

            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickPremiumButtonLeftInGame()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            shopC.IsOpenedShopZone = true;
            updateAllViewC.NeedUpdateView = true;
        }
        public void ClickLeaveButtonInGame()
        {
            PhotonNetwork.LeaveRoom();
        }


        #region BookZoneButtons

        public void ClickNextButtonInBookZone()
        {
            if (bookC.OpenedNowPageBookT < PageBookTypes.End - 1)
            {
                bookC.OpenedNowPageBookT++;
                dataFromViewC.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                updateAllViewC.NeedUpdateView = true;
            }
        }
        public void ClickBackButtonInBookZone()
        {
            if (bookC.OpenedNowPageBookT > 0)
            {
                bookC.OpenedNowPageBookT--;
                dataFromViewC.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                updateAllViewC.NeedUpdateView = true;
            }
        }
        public void ClickExitBookInBookZone()
        {
            bookC.WasOpenedBookT = bookC.OpenedNowPageBookT;
            bookC.OpenedNowPageBookT = PageBookTypes.None;

            dataFromViewC.SoundAction(ClipTypes.CloseBook).Invoke();

            updateAllViewC.NeedUpdateView = true;
        }

        #endregion


        #region SettingsZone

        public void ClickExitInOpenedSettingBarZone()
        {
            settingsC.IsOpenedBarWithSettings = false;
            //eVCommon.Sound(ClipTypes.Click).Play();

            updateAllViewC.NeedUpdateView = true;
        }

        #endregion


        #region ShopZone

        public void ClickExitShopInShopZone()
        {
            shopC.IsOpenedShopZone = false;
        }

        #endregion



        #region Menu

        public void ClickOpenBookCenterInMenu()
        {
            bookC.TryOpenBook();

            dataFromViewC.SoundAction(bookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();

            updateAllViewC.NeedUpdateView = true;
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

            aboutGameC.GameModeT = GameModeTypes.PublicOnline;

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
            aboutGameC.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomNameFromViewBar, roomOptions, default);
        }
        public void ClickJoinRandomPublicRoomInMenu()
        {
            aboutGameC.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }
        public void ClickJoinFriendRoomInMenu(in string nameRoomFromViewBar)
        {
            aboutGameC.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(nameRoomFromViewBar);
        }

        public void ClickCreateOffGameInMenu(in GameModeTypes offGameMode)
        {
            aboutGameC.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        #endregion
    }
}