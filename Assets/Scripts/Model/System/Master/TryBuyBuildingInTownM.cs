using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryBuyBuildingInTownM(in BuildingTypes buildT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

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
                                need = CostsForBuyBuildingInTownValues.NEED_FOOD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = CostsForBuyBuildingInTownValues.NEED_FOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = CostsForBuyBuildingInTownValues.NEED_FOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }

                        break;

                    case ResourceTypes.Wood:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = _e.PlayerInfoE(whoseMove).PlayerInfoC.WoodForBuyHouse;
                                break;

                            case BuildingTypes.Market:
                                need = CostsForBuyBuildingInTownValues.NEED_WOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = CostsForBuyBuildingInTownValues.NEED_WOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Ore:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = CostsForBuyBuildingInTownValues.NEED_ORE_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = CostsForBuyBuildingInTownValues.NEED_ORE_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = CostsForBuyBuildingInTownValues.NEED_ORE_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Iron:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = CostsForBuyBuildingInTownValues.NEED_IRON_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = CostsForBuyBuildingInTownValues.NEED_IRON_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = CostsForBuyBuildingInTownValues.NEED_IRON_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Gold:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = CostsForBuyBuildingInTownValues.NEED_GOLD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = CostsForBuyBuildingInTownValues.NEED_GOLD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = CostsForBuyBuildingInTownValues.NEED_GOLD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    default: throw new Exception();
                }

                needRes.Add(resT, need);
                if (need > _e.ResourcesInInventory(whoseMove, resT)) canBuild = false;
            }

            if (canBuild)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.ResourcesInInventoryC(whoseMove).Subtract(resT, needRes[resT]);
                }

                switch (buildT)
                {
                    case BuildingTypes.House:
                        _e.PlayerInfoC(whoseMove).AmountBuiltHouses++;
                        //E.PlayerE(whoseMove).MaxPeopleInCity = (int)(E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns + E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns);
                        _e.PlayerInfoE(whoseMove).PlayerInfoC.WoodForBuyHouse += _e.PlayerInfoE(whoseMove).PlayerInfoC.WoodForBuyHouse;

                        if (_e.LessonT == LessonTypes.BuildHouseForWarrior)
                        {
                            SetNextLesson();
                        }
                        break;

                    case BuildingTypes.Market:
                        _e.PlayerInfoE(whoseMove).BuildingsInTownInfoC.Build(BuildingTypes.Market);
                        break;

                    case BuildingTypes.Smelter:
                        _e.PlayerInfoE(whoseMove).BuildingsInTownInfoC.Build(BuildingTypes.Smelter);
                        break;

                    default: throw new Exception();
                }





                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Building);
            }

            else
            {
                if (_e.LessonT.Is(Enum.LessonTypes.TryBuyingHouse))
                {
                     SetNextLesson();
                }

               RpcSs.SimpleMistakeToGeneral(sender, needRes);
            }

            //if (_eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
            //{
            //    _eMG.LessonTC.SetNextLesson();
            //}

        }

    }
}