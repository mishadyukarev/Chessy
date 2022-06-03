using Chessy.Common.Enum;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed class BuildBuildingClickS : SystemModel
    {
        internal BuildBuildingClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click(in BuildingTypes buildT)
        {
            if (eMG.CurPlayerIT == eMG.WhoseMovePlayerT)
            {
                if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
                {
                    if (eMG.SelectedE.BuildingsC.Is(buildT))
                    {
                        eMG.SelectedE.BuildingsC.Set(buildT, false);
                        eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
                    }
                    else if (eMG.PlayerInfoE(eMG.CurPlayerIT).BuildingsInfoC.HaveBuilding(buildT))
                    {
                        eMG.SelectedE.BuildingsC.Set(buildT, true);
                        eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
                    }
                    else
                    {
                        eMG.RpcPoolEs.Action0(eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.BuyBuilding, buildT });
                    }
                }



                switch (buildT)
                {
                    case BuildingTypes.House:
                        eMG.RpcPoolEs.Action0(eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.BuyBuilding, buildT });
                        break;

                    case BuildingTypes.Market:
                        if (eMG.LessonT == LessonTypes.ClickBuyMarketInTown)
                        {
                            eMG.LessonTC.SetNextLesson();
                            eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = EnvironmentValues.MAX_RESOURCES;
                            eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                        }
                        break;

                    case BuildingTypes.Smelter:
                        break;

                    default: throw new Exception();
                }


            }
            else
            {
                sMG.MistakeSs.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);
            }

            eMG.NeedUpdateView = true;
        }

        internal void TryBuy(in BuildingTypes buildT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            var needRes = new Dictionary<ResourceTypes, float>();
            var canBuild = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                var need = 0f;

                switch (resT)
                {
                    case ResourceTypes.Food:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = EconomyValues.NEED_FOOD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = EconomyValues.NEED_FOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = EconomyValues.NEED_FOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }

                        break;

                    case ResourceTypes.Wood:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = eMG.PlayerInfoE(whoseMove).WoodForBuyHouse;
                                break;

                            case BuildingTypes.Market:
                                need = EconomyValues.NEED_WOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = EconomyValues.NEED_WOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Ore:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = EconomyValues.NEED_ORE_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = EconomyValues.NEED_ORE_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = EconomyValues.NEED_ORE_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Iron:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = EconomyValues.NEED_IRON_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = EconomyValues.NEED_IRON_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = EconomyValues.NEED_IRON_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Gold:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = EconomyValues.NEED_GOLD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = EconomyValues.NEED_GOLD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = EconomyValues.NEED_GOLD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    default: throw new Exception();
                }

                needRes.Add(resT, need);
                if (need > eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
            }

            if (canBuild)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                }

                switch (buildT)
                {
                    case BuildingTypes.House:
                        eMG.PlayerInfoE(whoseMove).PawnInfoC.MaxAvailable++;
                        //E.PlayerE(whoseMove).MaxPeopleInCity = (int)(E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns + E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns);
                        eMG.PlayerInfoE(whoseMove).WoodForBuyHouse += eMG.PlayerInfoE(whoseMove).WoodForBuyHouse;
                        break;

                    case BuildingTypes.Market:
                        eMG.PlayerInfoE(whoseMove).BuildingsInfoC.Build(BuildingTypes.Market);
                        break;

                    case BuildingTypes.Smelter:
                        eMG.PlayerInfoE(whoseMove).BuildingsInfoC.Build(BuildingTypes.Smelter);
                        break;

                    default: throw new Exception();
                }

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
            }

            else
            {
                if (eMG.LessonTC.Is(Enum.LessonTypes.BuyingHouse))
                {
                    eMG.LessonTC.SetNextLesson();
                }

                eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }

            if (eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
            {
                eMG.LessonTC.SetNextLesson();
            }

        }
    }
}