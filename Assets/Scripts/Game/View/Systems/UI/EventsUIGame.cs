using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;
using Chessy.Game.System.Model;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game.EventsUI
{
    public sealed class EventsUIGame
    {
        public EventsUIGame(EntitiesViewUICommon eUIC, EntitiesModelCommon eC, SystemsModelGame systems, in EntitiesViewUIGame eUI, Entity.Model.EntitiesModelGame e)
        {
            #region Down

            eUI.DownEs.DonerE.ButtonC.AddListener(delegate { systems.DoneClickS.Click(systems.MistakeS, e); });
            eUI.DownEs.HeroE.ButtonC.AddListener(delegate { systems.GetHeroClickDownS.Get(e); });
            eUI.DownEs.PawnE.ButtonUIC.AddListener(delegate { systems.GetPawnClickS.Get(e); });
            eUI.DownEs.CityButtonUIE.ButtonC.AddListener(delegate { systems.OpenCityClickS.Click(e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Pick, e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Sword, e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Shield, e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.BowCrossbow, e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Axe, e); });
            eUI.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { systems.ToggleToolWeaponClickS.Toggle(ToolWeaponTypes.Staff, e); });
            eUI.DownEs.BookButtonC.AddListener(delegate
            {
                eC.BookC.IsOpenedBook = !eC.BookC.IsOpenedBook;
                //e.Sound(eC.IsOpenedBook ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();

            });

            #endregion


            #region Up

            eUI.UpEs.WindButtonC.AddListener(delegate
            {
                eC.BookC.PageBookT = PageBoookTypes.Wind;
                eC.BookC.IsOpenedBook = true;
                //e.Sound(ClipTypes.OpenBook).Invoke();
            });

            #endregion


            //Left
            var leftEs = eUI.LeftEs;
            eUI.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { systems.EnvironmentInfoClickS.Info(e); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { systems.BuildBuildingClickS.Click(BuildingTypes.House, e); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { systems.BuildBuildingClickS.Click(BuildingTypes.Market, e); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { systems.BuildBuildingClickS.Click(BuildingTypes.Smelter, e); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eUIC.ShopE, e);
            });


            //Right
            eUI.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { systems.AbilityClickS.Click(ButtonTypes.First, e); });
            eUI.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { systems.AbilityClickS.Click(ButtonTypes.Second, e); });
            eUI.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { systems.AbilityClickS.Click(ButtonTypes.Third, e); });
            eUI.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { systems.AbilityClickS.Click(ButtonTypes.Fourth, e); });
            eUI.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { systems.AbilityClickS.Click(ButtonTypes.Fifth, e); });

            eUI.RightEs.ProtectE.ButtonC.AddListener(delegate { systems.ConditionClickS.Click(ConditionUnitTypes.Protected, e); });
            eUI.RightEs.RelaxE.ButtonC.AddListener(delegate { systems.ConditionClickS.Click(ConditionUnitTypes.Relaxed, e); });


            #region Center

            var centerEs = eUI.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { systems.ReadyClickS.Ready(e); });
            centerEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });
            centerEs.KingE.Button.AddListener(delegate { systems.GetKingClickS.Click(e); });
            centerEs.FriendE.ButtonC.AddListener(delegate { e.ZoneInfoC.IsActiveFriend = false; });
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { systems.GetHeroClickCenterS.Get(UnitTypes.Elfemale, e); });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { systems.GetHeroClickCenterS.Get(UnitTypes.Snowy, e); });
            centerEs.OpenShopButtonC.AddListener(delegate { OpenShop(eUIC.ShopE, e); });

            //Building
            centerEs.MarketE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Market, e); });
            centerEs.SmelterE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Smelter, e); });
            centerEs.SmelterE.ButtonC.AddListener(delegate { e.RpcPoolEs.Melt_ToMaster(); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(delegate { e.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.FoodToWood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(delegate { e.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.WoodToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(delegate { e.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToFood); });
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(delegate { e.RpcPoolEs.BuyResource_ToMaster(MarketBuyTypes.GoldToWood); });


            #endregion


            //Up
            eUI.UpEs.AlphaC.AddListener(delegate
            {
                OpenShop(eUIC.ShopE, e);
            });
            eUI.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        void Exit(in BuildingTypes buildingT, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            e.SelectedE.BuildingsC.Set(buildingT, false);
        }

        void OpenShop(in ShopUIE shopUIE, in Entity.Model.EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            shopUIE.ShopZoneGOC.SetActive(false);
        }
    }
}