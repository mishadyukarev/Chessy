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
    public sealed partial class SystemsModelGameForUI
    {
        int _buildingsForSkipLesson = 6;

        public void BuildBuildingClick(in BuildingTypes buildT)
        {
            if (_eMG.CurPlayerIT == _eMG.WhoseMovePlayerT)
            {
                if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
                {
                    if (_eMG.SelectedE.BuildingsC.Is(buildT))
                    {
                        _eMG.SelectedE.BuildingsC.Set(buildT, false);
                        _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
                    }
                    else if (_eMG.PlayerInfoE(_eMG.CurPlayerIT).BuildingsInfoC.HaveBuilding(buildT))
                    {
                        _eMG.SelectedE.BuildingsC.Set(buildT, true);
                        _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
                    }
                    else
                    {
                        _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.ForUISystems.TryBuyBuildingM), buildT });
                    }
                }



                switch (buildT)
                {
                    case BuildingTypes.House:
                        _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.ForUISystems.TryBuyBuildingM), buildT });
                        break;

                    case BuildingTypes.Market:
                        //if (_eMG.LessonT == LessonTypes.ClickBuyMarketInTown)
                        //{
                        //    _eMG.LessonTC.SetNextLesson();
                        //    _eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = EnvironmentValues.MAX_RESOURCES;
                        //    _eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                        //}
                        break;

                    case BuildingTypes.Smelter:
                        break;

                    default: throw new Exception();
                }


            }
            else
            {
                _sMG.Mistake(MistakeTypes.NeedWaitQueue);
            }

            _eMG.NeedUpdateView = true;
        }

        internal void TryBuyBuildingM(in BuildingTypes buildT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

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
                                need = _eMG.PlayerInfoE(whoseMove).WoodForBuyHouse;
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
                if (need > _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
            }

            if (canBuild)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                }

                switch (buildT)
                {
                    case BuildingTypes.House:
                        _eMG.PlayerInfoE(whoseMove).PawnInfoC.MaxAvailable++;
                        //E.PlayerE(whoseMove).MaxPeopleInCity = (int)(E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns + E.PlayerE(whoseMove).PawnInfoE.MaxAvailablePawns);
                        _eMG.PlayerInfoE(whoseMove).WoodForBuyHouse += _eMG.PlayerInfoE(whoseMove).WoodForBuyHouse;
                        break;

                    case BuildingTypes.Market:
                        _eMG.PlayerInfoE(whoseMove).BuildingsInfoC.Build(BuildingTypes.Market);
                        break;

                    case BuildingTypes.Smelter:
                        _eMG.PlayerInfoE(whoseMove).BuildingsInfoC.Build(BuildingTypes.Smelter);
                        break;

                    default: throw new Exception();
                }


                if (_eMG.LessonT == LessonTypes.BuildHouses)
                {
                    if(_eMG.PlayerInfoE(PlayerTypes.First).PawnInfoC.MaxAvailable >= _buildingsForSkipLesson)
                    {
                        _eMG.LessonTC.SetNextLesson();
                    }
                }


                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
            }

            else
            {
                if (_eMG.LessonTC.Is(Enum.LessonTypes.TryBuyingHouse))
                {
                    _eMG.LessonTC.SetNextLesson();
                }

                _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }

            //if (_eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
            //{
            //    _eMG.LessonTC.SetNextLesson();
            //}

        }
    }
}