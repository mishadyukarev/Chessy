using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;
using Chessy.Game.Entity.Model;
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
            eUIGame.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { sMGame.ForUISystems.EnvironmentInfoClickS.Click(); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.House); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.Market); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClickS.Click(BuildingTypes.Smelter); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eUICommon.ShopE, eMGame);
            });


            //Right
            eUIGame.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.First); });
            eUIGame.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Second); });
            eUIGame.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Third); });
            eUIGame.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Fourth); });
            eUIGame.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sMGame.ForUISystems.AbilityClickS.Click(ButtonTypes.Fifth); });

            eUIGame.RightEs.ProtectE.ButtonC.AddListener(delegate { sMGame.ForUISystems.ConditionClickS.Click(ConditionUnitTypes.Protected); });
            eUIGame.RightEs.RelaxE.ButtonC.AddListener(delegate { sMGame.ForUISystems.ConditionClickS.Click(ConditionUnitTypes.Relaxed); });


            #region Center

            var centerEs = eUIGame.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { sMGame.ForUISystems.ReadyClickS.Click(); });
            centerEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });
            centerEs.KingE.Button.AddListener(sMGame.ForUISystems.GetKingClickS.Click);
            centerEs.FriendE.ButtonC.AddListener(delegate { eMGame.ZoneInfoC.IsActiveFriend = false; });
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { sMGame.ForUISystems.GetHeroClickCenterS.Get(UnitTypes.Elfemale); });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { sMGame.ForUISystems.GetHeroClickCenterS.Get(UnitTypes.Snowy); });
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