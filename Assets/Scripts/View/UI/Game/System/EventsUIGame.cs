﻿using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game.EventsUI
{
    public sealed class EventsUIGame
    {
        public EventsUIGame(EntitiesViewUICommon eUICommon, EntitiesModelCommon eMCommon, SystemsModelGame sMGame, EntitiesViewUIGame eUIGame, EntitiesModelGame eMGame)
        {
            eUICommon.BookE.ExitButtonC.AddListener(delegate
            {
                eMGame.NeedUpdateView = true;
            });




            #region Down

            eUIGame.DownEs.DonerE.ButtonC.AddListener(sMGame.ForUISystems.DoneClickS.Click);
            eUIGame.DownEs.HeroE.ButtonC.AddListener(delegate { sMGame.ForUISystems.GetHeroClickDownS.Click(); });
            eUIGame.DownEs.PawnE.ButtonUIC.AddListener(delegate { sMGame.ForUISystems.GetPawnClickS.Click(); });
            eUIGame.DownEs.CityButtonUIE.ButtonC.AddListener(sMGame.ForUISystems.OpenCityClickS.Click);
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.Pick); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.Sword); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.Shield); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.BowCrossbow); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.Axe); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeaponClickS.Click(ToolWeaponTypes.Staff); });
            eUIGame.DownEs.BookLittleE.ButtonC.AddListener(delegate
            {
                eMCommon.IsOpenedBook = !eMCommon.IsOpenedBook;
                eMCommon.SoundActionC(eMCommon.IsOpenedBook ? Common.Enum.ClipCommonTypes.OpenBook : Common.Enum.ClipCommonTypes.CloseBook).Invoke();
                eMGame.NeedUpdateView = true;

                eUIGame.DownEs.BookLittleE.AnimationVC.Play();
            });

            #endregion


            #region Up


            eUIGame.UpEs.SettingsButtonC.AddListener(delegate
            {
                eMCommon.IsOpenSettings = !eMCommon.IsOpenSettings;
                eMCommon.SoundActionC(ClipCommonTypes.Click);
            });
            eUIGame.UpEs.WindButtonC.AddListener(delegate
            {
                if (eMGame.LessonTC.Is(LessonTypes.ClickWindInfo))
                {
                    eMGame.WeatherE.SunSideTC.SunSideT = SunSideTypes.Dawn;
                    eMGame.LessonTC.SetNextLesson();    
                }
                else
                {
                    if (eMCommon.IsOpenedBook)
                    {
                        eMCommon.IsOpenedBook = false;
                        eMCommon.SoundActionC(ClipCommonTypes.CloseBook).Invoke();
                    }
                    else
                    {
                        eMCommon.IsOpenedBook = true;
                        eMCommon.PageBookTC.PageBookT = PageBookTypes.Wind;
                        eMCommon.SoundActionC(ClipCommonTypes.OpenBook).Invoke();
                    }
                }

                eMGame.NeedUpdateView = true;
            });
            eUIGame.UpEs.DiscordButtonC.AddListener(() =>
            {
                Application.OpenURL(URLC.URL_DISCORD);
            });

            #endregion


            #region Left

            var leftEs = eUIGame.LeftEs;
            eUIGame.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { sMGame.ForUISystems.EnvironmentInfoClickS.Click(); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.House); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.Market); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.Smelter); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eMGame, eMCommon);
            });

            #endregion


            #region Right

            eUIGame.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.First); });
            eUIGame.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Second); });
            eUIGame.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Third); });
            eUIGame.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Fourth); });
            eUIGame.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Fifth); });

            eUIGame.RightEs.ProtectE.ButtonC.AddListener(delegate { sMGame.ForUISystems.ConditionClickS.Click(ConditionUnitTypes.Protected); });
            eUIGame.RightEs.RelaxE.ButtonC.AddListener(delegate { sMGame.ForUISystems.ConditionClickS.Click(ConditionUnitTypes.Relaxed); });

            #endregion


            #region Center

            var centerEs = eUIGame.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { sMGame.ForUISystems.ReadyClickS.Click(); });
            centerEs.JoinDiscordButtonC.AddListener(delegate
            {
                Application.OpenURL(URLC.URL_DISCORD);
            });
            centerEs.KingE.Button.AddListener(sMGame.ForUISystems.GetKingClickS.Click);
            centerEs.FriendE.ButtonC.AddListener(delegate
            {
                eMGame.ZoneInfoC.IsActiveFriend = false;
                eMGame.NeedUpdateView = true;
            });
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate
            {
                sMGame.ForUISystems.GetHeroClickCenterS.Get(UnitTypes.Elfemale);
            });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate
            {
                sMGame.ForUISystems.GetHeroClickCenterS.Get(UnitTypes.Snowy);
            });
            centerEs.OpenShopButtonC.AddListener(delegate
            {
                OpenShop(eMGame, eMCommon);
                eMGame.NeedUpdateView = true;
            });

            //Building
            centerEs.MarketE.ExitButtonC.AddListener(delegate
            {
                Exit(BuildingTypes.Market, eMGame, eMCommon);
                eMGame.NeedUpdateView = true;
            });
            centerEs.SmelterE.ExitButtonC.AddListener(delegate
            {
                Exit(BuildingTypes.Smelter, eMGame, eMCommon);
                eMGame.NeedUpdateView = true;
            });
            centerEs.SmelterE.ButtonC.AddListener(delegate
            {
                eMGame.RpcPoolEs.Melt_ToMaster();
            });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.FoodToWood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.WoodToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToWood); });


            centerEs.SkipLessonE.ButtonUIC.AddListener(sMGame.ForUISystems.ClickSkipLessonCenterS.Click);


            #endregion


            //Up
            eUIGame.UpEs.AlphaC.AddListener(delegate
            {
                OpenShop(eMGame, eMCommon);
            });
            eUIGame.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        void Exit(in BuildingTypes buildingT, in EntitiesModelGame eMG, in EntitiesModelCommon eMC)
        {
            eMC.SoundActionC(Common.Enum.ClipCommonTypes.Click).Invoke();
            eMG.SelectedE.BuildingsC.Set(buildingT, false);

            eMG.NeedUpdateView = true;
        }

        void OpenShop(in EntitiesModelGame e, in EntitiesModelCommon eMC)
        {
            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();

            eMC.ShopC.IsOpenedShopZone = true;

            e.NeedUpdateView = true;
        }
    }
}