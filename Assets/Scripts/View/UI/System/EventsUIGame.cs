using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;
using Chessy.Model.Model.System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.EventsUI
{
    public sealed class EventsUIGame : IUpdate
    {
        readonly Dictionary<PageBookTypes, PressHintS> _pressHintSs = new Dictionary<PageBookTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _abilities = new Dictionary<ButtonTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _effects = new Dictionary<ButtonTypes, PressHintS>();

        private const byte MAX_PLAYERS = 2;

        public EventsUIGame(SystemsModel sM, EntitiesViewUI eUI, EntitiesView eV, EntitiesModel eM)
        {
            #region Menu

            eUI.CenterE.DiscordButtonC.AddListener(() => Application.OpenURL(URLC.URL_DISCORD));
            eUI.CenterE.LikeGameButtonC.AddListener(() => Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY));
            eUI.CenterE.ExitButtonC.AddListener(() => Application.Quit());


            eUI.OnlineZoneE.JoinButtonC.AddListener(ConnectOnline);
            eUI.OnlineZoneE.CreatePublicRoomButtonC.AddListener(() => CreateRoom(eM));
            eUI.OnlineZoneE.JoinRandomPublicRoomButtonC.AddListener(() => JoinRandomRoom(eM));
            eUI.OnlineZoneE.CreateFriendRoomButtonC.AddListener(() => CreateFriendRoom(eUI, eM));
            eUI.OnlineZoneE.JoinFriendRoomButtonC.AddListener(() => JoinFriendRoom(eUI, eM));


            eUI.OfflineZoneE.JoinButtonC.AddListener(ConnectOffline);
            eUI.OfflineZoneE.TrainingButtonC.AddListener(() => CreateOffGame(eM, GameModeTypes.TrainingOffline));
            eUI.OfflineZoneE.WithFriendButtonC.AddListener(() => CreateOffGame(eM, GameModeTypes.WithFriendOffline));



            //LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            //LikeGameUICom.AddListenerExit_But(ExitLikeGame);

            eUI.CenterE.BookButtonC.AddListener(delegate
             {
                 eM.SoundAction(eM.BookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();

                 eM.NeedUpdateView = true;
             });

            eUI.CenterE.SettingsButtonC.AddListener(delegate
            {
                eM.SettingsC.IsOpenedBarWithSettings = !eM.SettingsC.IsOpenedBarWithSettings;
                eM.NeedUpdateView = true;
            });

            #endregion


            #region Common

            eUI.ShopE.ExitButtonC.AddListener(() => { eM.ShopC.IsOpenedShopZone = false; });


            var bookE = eUI.BookE;
            bookE.ExitButtonC.AddListener(() =>
            {
                eV.SoundASC(ClipTypes.CloseBook).Play();

                eM.NeedUpdateView = true;
            });

            bookE.NextButtonC.AddListener(() =>
            {
                if (eM.OpenedNowPageBookT < PageBookTypes.End - 1)
                {
                    eM.OpenedNowPageBookT++;
                    eV.SoundASC(ClipTypes.ShiftBookSheet).Play();

                    eM.NeedUpdateView = true;
                }
            });

            bookE.BackButtonC.AddListener(() =>
            {
                if (eM.OpenedNowPageBookT > 0)
                {
                    eM.OpenedNowPageBookT--;
                    eV.SoundASC(ClipTypes.ShiftBookSheet).Play();

                    eM.NeedUpdateView = true;
                }
            });


            eUI.SettingsE.ExitButtonC.AddListener(() =>
            {
                eM.SettingsC.IsOpenedBarWithSettings = false;
                //eVCommon.Sound(ClipTypes.Click).Play();

                eM.NeedUpdateView = true;
            });


            eUI.ShopE.BuyButtonC.AddListener(sM.BuyPremiumProduct);



            #endregion




            for (var pageBookT = (PageBookTypes)0; pageBookT < PageBookTypes.End; pageBookT++)
            {
                _pressHintSs.Add(pageBookT, new PressHintS(pageBookT, sM, eM));
            }
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
            {
                _abilities.Add(buttonT, new PressHintS(buttonT, sM, eM));
                _effects.Add(buttonT, new PressHintS((byte)buttonT, sM, eM));
            }



            eUI.BookE.ExitButtonC.AddListener(delegate
            {
                eM.NeedUpdateView = true;
            });




            #region Down

            eUI.DownEs.DonerE.ButtonC.AddListener(sM.ForUISystems.DoneReadyClick);
            eUI.DownEs.DonerE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.DonerReady].Press(b));

            eUI.DownEs.HeroE.ButtonC.AddListener(delegate { sM.ForUISystems.OpenHeroClick(); });
            eUI.DownEs.HeroE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.God].Press(b));

            eUI.DownEs.PawnE.ButtonC.AddListener(delegate { sM.ForUISystems.GetPawn(); });
            eUI.DownEs.PawnE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pawn].Press(b));

            eUI.DownEs.CityButtonUIE.ButtonC.AddListener(sM.ForUISystems.OpenCityClick);
            eUI.DownEs.CityButtonUIE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Town].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Pick); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pick].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Sword); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Sword].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Shield); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Shield].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.BowCrossbow].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Axe); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Axe].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { sM.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Staff); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Staff].Press(b));

            eUI.DownEs.BookLittleE.ButtonC.AddListener(delegate
            {
                eM.SoundAction(eM.BookC.IsOpenedBook() ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();
                eM.NeedUpdateView = true;

                eUI.DownEs.BookLittleE.AnimationVC.Play();
            });

            #endregion


            #region Up


            eUI.UpEs.SettingsButtonC.AddListener(delegate
             {
                 eM.SettingsC.IsOpenedBarWithSettings = !eM.SettingsC.IsOpenedBarWithSettings;
                 eM.SoundAction(ClipTypes.Click);
             });
            eUI.UpEs.WindButtonC.AddListener(delegate
            {
                if (eM.LessonT.Is(LessonTypes.ClickWindInfo))
                {
                    eM.WeatherE.SunC.SunSideT = SunSideTypes.Dawn;
                    eM.CommonInfoAboutGameC.SetNextLesson();
                }
                else
                {
                    if (eM.BookC.IsOpenedBook())
                    {
                        eM.OpenedNowPageBookT = PageBookTypes.None;
                        eM.SoundAction(ClipTypes.CloseBook).Invoke();
                    }
                    else
                    {
                        eM.OpenedNowPageBookT = PageBookTypes.Wind;
                        eM.SoundAction(ClipTypes.OpenBook).Invoke();
                    }
                }

                eM.NeedUpdateView = true;
            });
            eUI.UpEs.DiscordButtonC.AddListener(() =>
            {
                Application.OpenURL(URLC.URL_DISCORD);
            });

            #endregion


            #region Left

            var leftEs = eUI.LeftEs;
            eUI.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { sM.ForUISystems.EnvironmentClick(); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { sM.ForUISystems.BuildBuildingClick(BuildingTypes.House); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { sM.ForUISystems.BuildBuildingClick(BuildingTypes.Market); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { sM.ForUISystems.BuildBuildingClick(BuildingTypes.Smelter); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eM, eM);
            });

            #endregion


            #region Right

            eUI.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sM.ForUISystems.Click(ButtonTypes.First); });
            eUI.RightEs.Unique(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.First].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sM.ForUISystems.Click(ButtonTypes.Second); });
            eUI.RightEs.Unique(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Second].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sM.ForUISystems.Click(ButtonTypes.Third); });
            eUI.RightEs.Unique(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Third].Press(b));


            eUI.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sM.ForUISystems.Click(ButtonTypes.Fourth); });
            eUI.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Fourth].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sM.ForUISystems.Click(ButtonTypes.Fifth); });


            eUI.RightEs.Effect(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.First].Press(b));
            eUI.RightEs.Effect(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Second].Press(b));
            eUI.RightEs.Effect(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Third].Press(b));



            eUI.RightEs.ProtectE.ButtonC.AddListener(() => sM.ForUISystems.Click(ConditionUnitTypes.Protected));
            eUI.RightEs.ProtectE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Defend].Press(b));

            eUI.RightEs.RelaxE.ButtonC.AddListener(delegate { sM.ForUISystems.Click(ConditionUnitTypes.Relaxed); });
            eUI.RightEs.RelaxE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.ExtractPawn].Press(b));

            eUI.RightEs.StatsEs.EnergyE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Steps].Press(b));
            eUI.RightEs.StatsEs.DamageE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Damage].Press(b));
            eUI.RightEs.StatsEs.WaterE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Water].Press(b));

            #endregion


            #region Center

            var centerEs = eUI.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { sM.ForUISystems.ClickReady(); });
            centerEs.JoinDiscordButtonC.AddListener(delegate
            {
                Application.OpenURL(URLC.URL_DISCORD);
            });
            centerEs.KingE.Button.AddListener(sM.ForUISystems.GetClickEffect);
            centerEs.FriendE.ButtonC.AddListener(() => sM.ForUISystems.ClickFriendReady());
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate
            {
                sM.ForUISystems.GetHeroClickCenter(UnitTypes.Elfemale);
            });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(() => sM.ForUISystems.GetHeroClickCenter(UnitTypes.Snowy));
            centerEs.OpenShopButtonC.AddListener(delegate
            {
                OpenShop(eM, eM);
                eM.NeedUpdateView = true;
            });

            //Building
            centerEs.MarketE.ExitButtonC.AddListener(delegate
            {
                Exit(BuildingTypes.Market, eM, eM);
                eM.NeedUpdateView = true;
            });
            centerEs.SmelterE.ExitButtonC.AddListener(delegate
            {
                Exit(BuildingTypes.Smelter, eM, eM);
                eM.NeedUpdateView = true;
            });
            centerEs.SmelterE.ButtonC.AddListener(() => sM.ForUISystems.Melt());
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(() => sM.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.FoodToWood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(() => sM.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.WoodToFood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(() => sM.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToFood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(() => sM.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToWood));


            centerEs.SkipLessonE.ButtonUIC.AddListener(sM.ForUISystems.ClickSkipLesson);


            #endregion


            //Up
            eUI.UpEs.AlphaC.AddListener(delegate
            {
                OpenShop(eM, eM);
            });
            eUI.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        #region Menu

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

        private void CreateRoom(in EntitiesModel e)
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

        private void CreateFriendRoom(in EntitiesViewUI eUI, in EntitiesModel eM)
        {
            var roomName = eUI.OnlineZoneE.CreateFriendRoomInputFieldC.InputField.text;

            eM.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions, default);
        }

        private void JoinRandomRoom(in EntitiesModel eM)
        {
            eM.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }

        private void JoinFriendRoom(in EntitiesViewUI eUI, in EntitiesModel eM)
        {
            eM.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(eUI.OnlineZoneE.JoinFriendRoomInputFieldC.InputField.text);
        }

        private void CreateOffGame(in EntitiesModel eM, in GameModeTypes offGameMode)
        {
            eM.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }

        #endregion



        void Exit(in BuildingTypes buildingT, in EntitiesModel eMG, in EntitiesModel eM)
        {
            eM.SoundAction(ClipTypes.Click).Invoke();
            eMG.SelectedE.BuildingsC.Set(buildingT, false);

            eMG.NeedUpdateView = true;
        }

        void OpenShop(in EntitiesModel e, in EntitiesModel eM)
        {
            eM.SoundAction(ClipTypes.Click).Invoke();

            eM.ShopC.IsOpenedShopZone = true;

            e.NeedUpdateView = true;
        }


        public void Update()
        {
            for (var pageBookT = (PageBookTypes)0; pageBookT < PageBookTypes.End; pageBookT++)
            {
                _pressHintSs[pageBookT].Update();
            }
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.Fifth; buttonT++)
            {
                _abilities[buttonT].Update();
                _effects[buttonT].Update();
            }
        }
    }
}