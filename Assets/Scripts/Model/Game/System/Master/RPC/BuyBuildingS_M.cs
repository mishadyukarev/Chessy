﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class BuyBuildingS_M : SystemModelGameAbs
    {
        internal BuyBuildingS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Buy(in BuildingTypes buildT, in Player sender)
        {
            var whoseMove = eMG.WhoseMove.PlayerT;

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
                        eMG.PlayerInfoE(whoseMove).PawnInfoE.MaxAvailable++;
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
                if (eMG.LessonTC.LessonT == Enum.LessonTypes.BuyingHouse)
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