using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class LeftCityEventUI : SystemUIAbstract
    {
        internal LeftCityEventUI( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            UIE.LeftCityEs.BuildE(BuildingTypes.House).AddListener(delegate { Build(BuildingTypes.House); });
            UIE.LeftCityEs.BuildE(BuildingTypes.Market).AddListener(delegate { Build(BuildingTypes.Market); });
            UIE.LeftCityEs.BuildE(BuildingTypes.Smelter).AddListener(delegate { Build(BuildingTypes.Smelter); });
        }

        void Build(in BuildingTypes buildT)
        {
            var curPlayerI = E.CurPlayerITC.Player;

            switch (buildT)
            {
                case BuildingTypes.House:
                    E.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                    break;

                case BuildingTypes.Market:
                    {
                        if (E.SelectedBuildingTC.Is(BuildingTypes.Market))
                        {
                            E.SelectedBuildingTC.Building = BuildingTypes.None;
                        }
                        else if (E.PlayerE(curPlayerI).HaveMarket)
                        {
                            E.SelectedBuildingTC.Building = BuildingTypes.Market;
                        }
                        else
                        {
                            E.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                        }
                    }
                    break;

                case BuildingTypes.Smelter:
                    if (E.PlayerE(curPlayerI).HaveSmelter)
                    {

                    }
                    else
                    {
                        E.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                    }
                    break;

                default: throw new Exception();
            }
        }

        public void BuyBuilding_Master(in BuildingTypes buildT, in Player sender)
        {
            var whoseMove = E.WhoseMove.Player;

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
                                need = ResourcesEconomy_Values.NEED_FOOD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = ResourcesEconomy_Values.NEED_FOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = ResourcesEconomy_Values.NEED_FOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }

                        break;

                    case ResourceTypes.Wood:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = ResourcesEconomy_Values.NEED_WOOD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = ResourcesEconomy_Values.NEED_WOOD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = ResourcesEconomy_Values.NEED_WOOD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Ore:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = ResourcesEconomy_Values.NEED_ORE_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = ResourcesEconomy_Values.NEED_ORE_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = ResourcesEconomy_Values.NEED_ORE_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Iron:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = ResourcesEconomy_Values.NEED_IRON_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = ResourcesEconomy_Values.NEED_IRON_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = ResourcesEconomy_Values.NEED_IRON_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    case ResourceTypes.Gold:
                        switch (buildT)
                        {
                            case BuildingTypes.House:
                                need = ResourcesEconomy_Values.NEED_GOLD_FOR_BUILDING_HOUSE;
                                break;

                            case BuildingTypes.Market:
                                need = ResourcesEconomy_Values.NEED_GOLD_FOR_BUILDING_MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                need = ResourcesEconomy_Values.NEED_GOLD_FOR_BUILDING_SMELTER;
                                break;

                            default:
                                break;
                        }
                        break;

                    default: throw new Exception();
                }

                needRes.Add(resT, need);
                if (need > E.PlayerE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
            }

            if (canBuild)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    E.PlayerE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                }

                switch (buildT)
                {
                    case BuildingTypes.House:
                        E.PlayerE(whoseMove).MaxAvailablePawns++;
                        break;

                    case BuildingTypes.Market:
                        E.PlayerE(whoseMove).HaveMarket = true;
                        break;

                    case BuildingTypes.Smelter:
                        E.PlayerE(whoseMove).HaveSmelter = true;
                        break;

                    default: throw new Exception();
                }

                E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
            }

            else
            {
                E.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}