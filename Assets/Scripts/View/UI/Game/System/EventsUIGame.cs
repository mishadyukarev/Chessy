using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.EventsUI
{
    public sealed class EventsUIGame : IUpdate
    {
        readonly Dictionary<PageBookTypes, PressHintS> _pressHintSs = new Dictionary<PageBookTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _abilities = new Dictionary<ButtonTypes, PressHintS>();
        readonly Dictionary<ButtonTypes, PressHintS> _effects = new Dictionary<ButtonTypes, PressHintS>();

        public EventsUIGame(EntitiesViewUICommon eUICommon, EntitiesModelCommon eMCommon, SystemsModelGame sMGame, EntitiesViewUIGame eUIGame, EntitiesModelGame eMGame)
        {
            for (var pageBookT = (PageBookTypes)0; pageBookT < PageBookTypes.End; pageBookT++)
            {
                _pressHintSs.Add(pageBookT, new PressHintS(pageBookT, sMGame, eMGame));
            }
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
            {
                _abilities.Add(buttonT, new PressHintS(buttonT, sMGame, eMGame));
                _effects.Add(buttonT, new PressHintS((byte)buttonT, sMGame, eMGame));
            }



            eUICommon.BookE.ExitButtonC.AddListener(delegate
            {
                eMGame.NeedUpdateView = true;
            });




            #region Down

            eUIGame.DownEs.DonerE.ButtonC.AddListener(sMGame.ForUISystems.DoneReadyClick);
            eUIGame.DownEs.DonerE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.DonerReady].Press(b));

            eUIGame.DownEs.HeroE.ButtonC.AddListener(delegate { sMGame.ForUISystems.OpenHeroClick(); });
            eUIGame.DownEs.HeroE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.God].Press(b));

            eUIGame.DownEs.PawnE.ButtonC.AddListener(delegate { sMGame.ForUISystems.GetPawn(); });
            eUIGame.DownEs.PawnE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pawn].Press(b));

            eUIGame.DownEs.CityButtonUIE.ButtonC.AddListener(sMGame.ForUISystems.OpenCityClick);
            eUIGame.DownEs.CityButtonUIE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Town].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Pick); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Pick].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Sword); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Sword].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Shield); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Shield].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.BowCrossbow].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Axe); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Axe].Press(b));

            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { sMGame.ForUISystems.ToggleToolWeapon(ToolWeaponTypes.Staff); });
            eUIGame.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Staff].Press(b));

            eUIGame.DownEs.BookLittleE.ButtonC.AddListener(delegate
            {
                eMCommon.SoundActionC(eMCommon.BookC.IsOpenedBook() ? Common.Enum.ClipCommonTypes.OpenBook : Common.Enum.ClipCommonTypes.CloseBook).Invoke();
                eMGame.NeedUpdateView = true;

                eUIGame.DownEs.BookLittleE.AnimationVC.Play();
            });

            #endregion


            #region Up


           eUIGame.UpEs.SettingsButtonC.AddListener(delegate
            {
                eMCommon.SettingsC.IsOpenedBarWithSettings = !eMCommon.SettingsC.IsOpenedBarWithSettings;
                eMCommon.SoundActionC(ClipCommonTypes.Click);
            });
            eUIGame.UpEs.WindButtonC.AddListener(delegate
            {
                if (SystemStatic.Is(eMGame.LessonT, LessonTypes.ClickWindInfo))
                {
                    eMGame.WeatherE.SunSideT = SunSideTypes.Dawn;
                    SystemStatic.SetNextLesson(eMGame.LessonT);    
                }
                else
                {
                    if (eMCommon.BookC.IsOpenedBook())
                    {
                        eMCommon.OpenedNowPageBookT = PageBookTypes.None;
                        eMCommon.SoundActionC(ClipCommonTypes.CloseBook).Invoke();
                    }
                    else
                    {
                        eMCommon.OpenedNowPageBookT = PageBookTypes.Wind;
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
            eUIGame.LeftEs.EnvironmentEs.InfoButtonC.AddListener(delegate { sMGame.ForUISystems.EnvironmentClick(); });
            //City
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClick(BuildingTypes.House); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClick(BuildingTypes.Market); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { sMGame.ForUISystems.BuildBuildingClick(BuildingTypes.Smelter); });

            leftEs.PremiumButtonC.AddListener(delegate
            {
                OpenShop(eMGame, eMCommon);
            });

            #endregion


            #region Right

            eUIGame.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ButtonTypes.First); });
            eUIGame.RightEs.Unique(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.First].Press(b));

            eUIGame.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ButtonTypes.Second); });
            eUIGame.RightEs.Unique(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Second].Press(b));

            eUIGame.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ButtonTypes.Third); });
            eUIGame.RightEs.Unique(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Third].Press(b));


            eUIGame.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ButtonTypes.Fourth); });
            eUIGame.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _abilities[ButtonTypes.Fourth].Press(b));

            eUIGame.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ButtonTypes.Fifth); });
                

            eUIGame.RightEs.Effect(ButtonTypes.First).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.First].Press(b));
            eUIGame.RightEs.Effect(ButtonTypes.Second).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Second].Press(b));
            eUIGame.RightEs.Effect(ButtonTypes.Third).ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _effects[ButtonTypes.Third].Press(b));



            eUIGame.RightEs.ProtectE.ButtonC.AddListener(() => sMGame.ForUISystems.Click(ConditionUnitTypes.Protected));
            eUIGame.RightEs.ProtectE.ButtonC.GameObject.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Defend].Press(b));

            eUIGame.RightEs.RelaxE.ButtonC.AddListener(delegate { sMGame.ForUISystems.Click(ConditionUnitTypes.Relaxed); });
            eUIGame.RightEs.RelaxE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.ExtractPawn].Press(b));

            eUIGame.RightEs.StatsEs.EnergyE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Steps].Press(b));
            eUIGame.RightEs.StatsEs.DamageE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Damage].Press(b));
            eUIGame.RightEs.StatsEs.WaterE.ButtonC.AddComponent<PressedButtonUIS>().SetAction((bool b) => _pressHintSs[PageBookTypes.Water].Press(b));

            #endregion


            #region Center

            var centerEs = eUIGame.CenterEs;
            centerEs.ReadyButtonC.AddListener(delegate { sMGame.ForUISystems.ClickReady(); });
            centerEs.JoinDiscordButtonC.AddListener(delegate
            {
                Application.OpenURL(URLC.URL_DISCORD);
            });
            centerEs.KingE.Button.AddListener(sMGame.ForUISystems.GetClickEffect);
            centerEs.FriendE.ButtonC.AddListener(() => sMGame.ForUISystems.ClickFriendReady());
            centerEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate
            {
                sMGame.ForUISystems.GetHeroClickCenter(UnitTypes.Elfemale);
            });
            centerEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(()=> sMGame.ForUISystems.GetHeroClickCenter(UnitTypes.Snowy));
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
            centerEs.SmelterE.ButtonC.AddListener(() => sMGame.ForUISystems.Melt());
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(() => sMGame.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.FoodToWood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(() => sMGame.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.WoodToFood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(() => sMGame.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToFood));
            centerEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(() => sMGame.ForUISystems.TryBuyFromMarketBuilding(MarketBuyTypes.GoldToWood));


            centerEs.SkipLessonE.ButtonUIC.AddListener(sMGame.ForUISystems.ClickSkipLesson);


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