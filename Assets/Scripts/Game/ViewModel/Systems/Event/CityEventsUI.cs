using Chessy.Game.Values;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game.EventsUI.Left
{
    public sealed class CityEventsUI : SystemAbstract
    {
        internal CityEventsUI(in LeftUIEs leftEs, in EntitiesModel ents) : base(ents)
        {
            leftEs.CityE(BuildingTypes.House).Button.AddListener(delegate { Build(BuildingTypes.House); });
            leftEs.CityE(BuildingTypes.Market).Button.AddListener(delegate { Build(BuildingTypes.Market); });
            leftEs.CityE(BuildingTypes.Smelter).Button.AddListener(delegate { Build(BuildingTypes.Smelter); });
        }

        void Build(in BuildingTypes buildT)
        {
            var curPlayerI = E.CurPlayerITC.Player;

            if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
            {
                if (E.SelectedBuildingsC.Is(buildT))
                {
                    E.SelectedBuildingsC.Set(buildT, false);
                    E.Sound(ClipTypes.Click).Invoke();
                }
                else if (E.PlayerInfoE(curPlayerI).HaveBuilding(buildT))
                {
                    E.SelectedBuildingsC.Set(buildT, true);
                    E.Sound(ClipTypes.Click).Invoke();
                }
                else
                {
                    E.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    E.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                    break;

                case BuildingTypes.Market:
                    break;

                case BuildingTypes.Smelter:
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
                                need = E.PlayerInfoE(whoseMove).WoodForBuyHouse;
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
                if (need > E.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
            }

            if (canBuild)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    E.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                }

                switch (buildT)
                {
                    case BuildingTypes.House:
                        E.PlayerInfoE(whoseMove).MaxAvailablePawns++;
                        //E.PlayerE(whoseMove).MaxPeopleInCity = (int)(E.PlayerE(whoseMove).MaxAvailablePawns + E.PlayerE(whoseMove).MaxAvailablePawns);
                        E.PlayerInfoE(whoseMove).WoodForBuyHouse += E.PlayerInfoE(whoseMove).WoodForBuyHouse;
                        break;

                    case BuildingTypes.Market:
                        E.PlayerInfoE(whoseMove).SetHaveBuilding(BuildingTypes.Market, true);
                        break;

                    case BuildingTypes.Smelter:
                        E.PlayerInfoE(whoseMove).SetHaveBuilding(BuildingTypes.Smelter, true);
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