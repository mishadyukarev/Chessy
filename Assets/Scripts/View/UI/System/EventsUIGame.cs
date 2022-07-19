using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.System;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using System.Collections.Generic;
using UnityEngine;
namespace Chessy.View.UI
{
    public sealed class EventsUIGame : IUpdate
    {
        readonly Dictionary<PageBookTypes, PressHintS> _pressHintSs = new Dictionary<PageBookTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _abilities = new Dictionary<ButtonTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _effects = new Dictionary<ButtonTypes, PressHintS>();

        public EventsUIGame(SystemsModel sM, EntitiesViewUI eUI, EntitiesModel eM)
        {
            for (var pageBookT = (PageBookTypes)0; pageBookT < PageBookTypes.End; pageBookT++)
            {
                _pressHintSs.Add(pageBookT, new PressHintS(pageBookT, sM, eM));
            }
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
            {
                _abilities.Add(buttonT, new PressHintS(buttonT, sM, eM));
                _effects.Add(buttonT, new PressHintS((byte)buttonT, sM, eM));
            }


            #region Menu

            eUI.CenterE.DiscordButtonC.AddListener(() => Application.OpenURL(URLC.URL_DISCORD));
            eUI.CenterE.LikeGameButtonC.AddListener(() => Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY));
            eUI.CenterE.ExitButtonC.AddListener(() => Application.Quit());


            eUI.OnlineZoneE.JoinButtonC.AddListener(sM.ForUISs.ClickConnectOnlineMenu);
            eUI.OnlineZoneE.CreatePublicRoomButtonC.AddListener(sM.ForUISs.ClickCreatePublickRoom);
            eUI.OnlineZoneE.JoinRandomPublicRoomButtonC.AddListener(sM.ForUISs.ClickJoinRandomPublicRoomInMenu);
            eUI.OnlineZoneE.CreateFriendRoomButtonC.AddListener(() => sM.ForUISs.ClickCreateFriendRoomInMenu(eUI.OnlineZoneE.CreateFriendRoomInputFieldC.InputField.text));
            eUI.OnlineZoneE.JoinFriendRoomButtonC.AddListener(() => sM.ForUISs.ClickJoinFriendRoomInMenu(eUI.OnlineZoneE.JoinFriendRoomInputFieldC.InputField.text));


            eUI.OfflineZoneE.JoinButtonC.AddListener(sM.ForUISs.ConnectOffline);
            eUI.OfflineZoneE.TrainingButtonC.AddListener(() => sM.ForUISs.ClickCreateOffGameInMenu(GameModeTypes.TrainingOffline));
            eUI.OfflineZoneE.WithFriendButtonC.AddListener(() => sM.ForUISs.ClickCreateOffGameInMenu(GameModeTypes.WithFriendOffline));



            //LikeGameUICom.AddListLikeGame_But(delegate { Application.OpenURL(URLC.URL_GAME_IN_GOOGLE_PLAY); });
            //LikeGameUICom.AddListenerExit_But(ExitLikeGame);

            eUI.CenterE.BookButtonC.AddListener(sM.ForUISs.ClickOpenBookCenterInMenu);

            eUI.CenterE.SettingsButtonC.AddListener(sM.ForUISs.ClickSettingsCenterMenu);

            #endregion


            #region Common

            var bookE = eUI.BookE;
            bookE.ExitButtonC.AddListener(sM.ForUISs.ClickExitBookInBookZone);
            bookE.NextButtonC.AddListener(sM.ForUISs.ClickNextButtonInBookZone);
            bookE.BackButtonC.AddListener(sM.ForUISs.ClickBackButtonInBookZone);

            eUI.SettingsE.ExitButtonC.AddListener(sM.ForUISs.ClickExitInOpenedSettingBarZone);

            eUI.ShopE.BuyButtonC.AddListener(sM.ForUISs.ClickBuyPremiumProduct);
            eUI.ShopE.ExitButtonC.AddListener(sM.ForUISs.ClickExitShopInShopZone);

            #endregion







            #region Down

            //eUI.DownEs.DonerE.ButtonC.AddListener(sM.ForUISs.DoneReadyClick);
            //eUI.DownEs.DonerE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.DonerReady].Press(b));

            eUI.DownEs.HeroE.ButtonC.AddListener(delegate { sM.ForUISs.OpenHeroClick(); });
            eUI.DownEs.HeroE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.God].Press(b));

            eUI.DownEs.PawnE.ButtonC.AddListener(delegate { sM.ForUISs.GetPawn(); });
            eUI.DownEs.PawnE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pawn].Press(b));

            eUI.DownEs.CityButtonUIE.ButtonC.AddListener(sM.ForUISs.OpenCityClick);
            eUI.DownEs.CityButtonUIE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Town].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Pick).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.Pick); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Pick).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pick].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Sword).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.Sword); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Sword).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Sword].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Shield).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.Shield); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Shield).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Shield].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.BowCrossbow).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.BowCrossbow); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.BowCrossbow).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.BowCrossbow].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Axe).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.Axe); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Axe).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Axe].Press(b));

            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Staff).AddListener(delegate { sM.ForUISs.ToggleToolWeapon(ToolsWeaponsWarriorTypes.Staff); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolsWeaponsWarriorTypes.Staff).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Staff].Press(b));

            eUI.DownEs.BookLittleE.ButtonC.AddListener(sM.ForUISs.ClickLittleBookDownInGame);

            #endregion


            #region Up


            eUI.UpEs.SettingsButtonC.AddListener(sM.ForUISs.ClickSettingUpGame);
            eUI.UpEs.WindButtonC.AddListener(sM.ForUISs.ClickWindButtonUp);
            eUI.UpEs.DiscordButtonC.AddListener(sM.ForUISs.ClickDiscordUpButton);

            #endregion


            #region Left

            var leftEs = eUI.LeftEs;
            eUI.LeftEs.EnvironmentEs.InfoButtonC.AddListener(sM.ForUISs.EnvironmentClick);
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(() => sM.ForUISs.ClickOntoTownBuilding(BuildingTypes.House));
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(() => sM.ForUISs.ClickOntoTownBuilding(BuildingTypes.Market));
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(() => sM.ForUISs.ClickOntoTownBuilding(BuildingTypes.Smelter));

            leftEs.PremiumButtonC.AddListener(sM.ForUISs.ClickPremiumButtonLeftInGame);

            #endregion


            #region Right

            eUI.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sM.ForUISs.Click(ButtonTypes.First); });
            eUI.RightEs.Unique(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.First].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sM.ForUISs.Click(ButtonTypes.Second); });
            eUI.RightEs.Unique(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Second].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sM.ForUISs.Click(ButtonTypes.Third); });
            eUI.RightEs.Unique(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Third].Press(b));


            eUI.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sM.ForUISs.Click(ButtonTypes.Fourth); });
            eUI.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Fourth].Press(b));

            eUI.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sM.ForUISs.Click(ButtonTypes.Fifth); });


            eUI.RightEs.Effect(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.First].Press(b));
            eUI.RightEs.Effect(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Second].Press(b));
            eUI.RightEs.Effect(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Third].Press(b));



            eUI.RightEs.ProtectE.ButtonC.AddListener(() => sM.ForUISs.Click(ConditionUnitTypes.Protected));
            eUI.RightEs.ProtectE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Defend].Press(b));

            eUI.RightEs.RelaxE.ButtonC.AddListener(delegate { sM.ForUISs.Click(ConditionUnitTypes.Relaxed); });
            eUI.RightEs.RelaxE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.ExtractPawn].Press(b));

            //eUI.RightEs.StatsEs.EnergyE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Steps].Press(b));
            eUI.RightEs.StatsEs.DamageE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Damage].Press(b));
            eUI.RightEs.StatsEs.WaterE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Water].Press(b));

            #endregion


            #region Center

            var centerGameEs = eUI.CenterEs;
            centerGameEs.ReadyButtonC.AddListener(sM.ForUISs.ClickReady);
            centerGameEs.KingE.Button.AddListener(sM.ForUISs.GetClickEffect);
            centerGameEs.FriendE.ButtonC.AddListener(sM.ForUISs.ClickFriendReadyButtonInGame);
            centerGameEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(() => sM.ForUISs.GetHeroClickCenter(UnitTypes.Elfemale));
            centerGameEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(() => sM.ForUISs.GetHeroClickCenter(UnitTypes.Snowy));
            centerGameEs.OpenShopButtonC.AddListener(sM.ForUISs.ClickPremiumButtonInHeroZoneInGame);

            //Building
            centerGameEs.MarketE.ExitButtonC.AddListener(sM.ForUISs.ClickExitMarketBarInGame);
            centerGameEs.SmelterE.ExitButtonC.AddListener(sM.ForUISs.ClickExitSmelterBarInGame);
            centerGameEs.SmelterE.ButtonC.AddListener(() => sM.ForUISs.Melt());
            centerGameEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(() => sM.ForUISs.TryBuyFromMarketBuilding(MarketBuyTypes.FoodToWood));
            centerGameEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(() => sM.ForUISs.TryBuyFromMarketBuilding(MarketBuyTypes.WoodToFood));
            centerGameEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(() => sM.ForUISs.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToFood));
            centerGameEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(() => sM.ForUISs.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToWood));


            centerGameEs.SkipLessonE.ButtonUIC.AddListener(sM.ForUISs.ClickSkipLesson);


            #endregion


            //Up
            eUI.UpEs.LeaveButtonC.AddListener(sM.ForUISs.ClickLeaveButtonInGame);
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