using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.Model;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game.EventsUI
{
    public sealed class EventsUIGame
    {
        public EventsUIGame(EntitiesViewUICommon eUICommon, EntitiesModelCommon eMCommon, SystemsModelGame sMGame, in EntitiesViewUIGame eUIGame, EntitiesModelGame eMGame)
        {
            #region Down

            eUIGame.DownEs.DonerE.ButtonC.AddListener(delegate { sMGame.DoneClickS.Click(sMGame.MistakeS, eMGame); });
            eUIGame.DownEs.HeroE.ButtonC.AddListener(delegate { sMGame.GetHeroClickDownS.Get(eMGame); });
            eUIGame.DownEs.PawnE.ButtonUIC.AddListener(delegate { sMGame.GetPawnClickS.Get(eMGame); });
            eUIGame.DownEs.CityButtonUIE.ButtonC.AddListener(delegate { sMGame.OpenCityClickS.Click(eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Pick, eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Sword, eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Shield, eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.BowCrossbow, eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Axe, eMGame); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { sMGame.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Staff, eMGame); });
            eUIGame.DownEs.BookButtonC.AddListener(delegate
            {
                eMCommon.BookE.IsOpenedBook = !eMCommon.BookE.IsOpenedBook;
                eMCommon.SoundActionC(eMCommon.BookE.IsOpenedBook ? Common.Enum.ClipTypes.OpenBook : Common.Enum.ClipTypes.CloseBook).Invoke();

            });

            #endregion


            #region Up

            eUIGame.UpEs.WindButtonC.AddListener(delegate
            {
                eMCommon.BookE.PageBookTC.PageBookT = PageBookTypes.Wind;
                eMCommon.BookE.IsOpenedBook = true;
                //e.Sound(ClipTypes.OpenBook).Invoke();
            });

            #endregion


            //Left
            var leftEs = eUIGame.LeftEs;
            eUIGame.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { sMGame.EnvironmentInfoClickS.Info(eMGame); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { sMGame.BuildBuildingClickS.Click(BuildingTypes.House, eMGame); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { sMGame.BuildBuildingClickS.Click(BuildingTypes.Market, eMGame); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { sMGame.BuildBuildingClickS.Click(BuildingTypes.Smelter, eMGame); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eUICommon.ShopE, eMGame);
            });


            //Right
            eUIGame.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sMGame.AbilityClickS.Click(ButtonTypes.First, eMGame); });
            eUIGame.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sMGame.AbilityClickS.Click(ButtonTypes.Second, eMGame); });
            eUIGame.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sMGame.AbilityClickS.Click(ButtonTypes.Third, eMGame); });
            eUIGame.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sMGame.AbilityClickS.Click(ButtonTypes.Fourth, eMGame); });
            eUIGame.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sMGame.AbilityClickS.Click(ButtonTypes.Fifth, eMGame); });

            eUIGame.RightEs.ProtectE.ButtonC.AddListener(delegate { sMGame.ConditionClickS.Click(ConditionUnitTypes.Protected, eMGame); });
            eUIGame.RightEs.RelaxE.ButtonC.AddListener(delegate { sMGame.ConditionClickS.Click(ConditionUnitTypes.Relaxed, eMGame); });


            #region Center

            var centerEs = eUIGame.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { sMGame.ReadyClickS.Ready(eMGame); });
            centerEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });
            centerEs.KingE.Button.AddListener(delegate { sMGame.GetKingClickS.Click(eMGame); });
            centerEs.FriendE.ButtonC.AddListener(delegate { eMGame.ZoneInfoC.IsActiveFriend = false; });
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { sMGame.GetHeroClickCenterS.Get(UnitTypes.Elfemale, eMGame); });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { sMGame.GetHeroClickCenterS.Get(UnitTypes.Snowy, eMGame); });
            centerEs.OpenShopButtonC.AddListener(delegate { OpenShop(eUICommon.ShopE, eMGame); });

            //Building
            centerEs.MarketE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Market, eMGame); });
            centerEs.SmelterE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Smelter, eMGame); });
            centerEs.SmelterE.ButtonC.AddListener(delegate { eMGame.RpcPoolEs.Melt_ToMaster(); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.FoodToWood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.WoodToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(delegate { eMGame.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToWood); });

            #endregion


            //Up
            eUIGame.UpEs.AlphaC.AddListener(delegate
            {
                OpenShop(eUICommon.ShopE, eMGame);
            });
            eUIGame.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        void Exit(in BuildingTypes buildingT, in EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            e.SelectedE.BuildingsC.Set(buildingT, false);
        }

        void OpenShop(in ShopUIE shopUIE, in EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            shopUIE.ShopZoneGOC.SetActive(false);
        }
    }
}