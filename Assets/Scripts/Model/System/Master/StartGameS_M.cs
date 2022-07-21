﻿using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void StartGame(in bool withTraining)
        {
            _e.Dispose();

            _zonesInfoC.IsActiveFriend = _aboutGameC.GameModeT.Is(GameModeTypes.WithFriendOffline);
            _aboutGameC.CellClickT = StartGameValues.CELL_CLICK;

            _windC.Set(StartGameValues.DIRECT_WIND, StartGameValues.SPEED_WIND);
            _sunC.SunSideT = StartGameValues.SUN_SIDE;


            SetClouds(StartGameValues.CLOUD_CELL_INDEX);


            _selectedToolWeaponC.ToolWeaponT = StartGameValues.SELECTED_TOOL_WEAPON;
            _selectedToolWeaponC.LevelT = StartGameValues.SELECTED_LEVEL_TOOL_WEAPON;

            _aboutGameC.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _e.PawnPeopleInfoC(playerT).PeopleInCity = StartGameValues.PEOPLE_IN_CITY;

                if (playerT == PlayerTypes.Second)
                {
                    if (_aboutGameC.GameModeT == GameModeTypes.TrainingOffline)
                        _e.PlayerInfoC(playerT).AmountBuiltHouses += 5;
                }



                _e.PlayerInfoE(playerT).PlayerInfoC.HaveKingInInventor = true;
                _e.PlayerInfoE(playerT).PlayerInfoC.WoodForBuyHouse = StartGameValues.NEED_WOOD_FOR_BUILDING_HOUSE;

                _e.PlayerInfoE(playerT).GodInfoC.HaveGodInInventor = true;

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.SetResourcesInInventory(playerT, resT, StartGameValues.AmountResourceEveryone(resT));
                }

                if (withTraining)
                {
                    if (playerT == PlayerTypes.First)
                    {
                        //_eMG.ResourcesC(playerT, ResourceTypes.Food).Resources = 3f;
                        //_eMG.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }
                }
            }



            if (_aboutGameC.GameModeT.IsOffline())
            {
                _aboutGameC.CurrentPlayerIT = PlayerTypes.First;
            }
            else
            {
                _aboutGameC.CurrentPlayerIT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    var x = _xyCellsCs[cell_0].X;
                    var y = _xyCellsCs[cell_0].Y;

                    _fireCs[cell_0].HaveFire = false;


                    if (!_cellCs[cell_0].IsBorder)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.Mountain))
                            {
                                _e.MountainC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    _e.AdultForestC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _e.AdultForestC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            _riverCs[cell_0].RiverT = RiverTypes.Start;
                            _haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            _riverCs[cell_0].RiverT = RiverTypes.Start;
                            _haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                            _haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            _riverCs[cell_0].RiverT = RiverTypes.Start;
                            _haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (_haveRiverAroundCellCs[cell_0].HaveRive(dir))
                            {
                                var idx_next = _e.GetIdxCellByDirectAround(cell_0, dir);

                                _riverCs[idx_next].RiverT = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _e.GetIdxCellByDirectAround(cell_0, dir);

                            _riverCs[idx_next].RiverT = RiverTypes.Corner;
                        }


                        if (cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_1_PLAYER || cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_2_PLAYER)
                        {
                            ClearAllEnvironment(cell_0);
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    if (!_cellCs[cell_0].IsBorder)
                    {
                        if (_e.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _e.GetIdxCellByDirectAround(cell_0, dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!_e.MountainC(_e.GetIdxCellByDirectAround(cell_0, dirT)).HaveAnyResources && !_buildingCs[idx_1].HaveBuilding)
                                    {
                                        _e.HillC(idx_1).Resources = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (_aboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
            {
                _e.SetResourcesInInventory(PlayerTypes.Second, ResourceTypes.Food, 999999);


                for (byte cellUdxCurrent = 0; cellUdxCurrent < IndexCellsValues.CELLS; cellUdxCurrent++)
                {
                    var x = _xyCellsCs[cellUdxCurrent].X;
                    var y = _xyCellsCs[cellUdxCurrent].Y;

                    if (x == 7 && y == 8)
                    {
                        //if (withTraining)
                        //{
                        _e.MountainC(cellUdxCurrent).Resources = 0;

                        TryDestroyAdultForest(cellUdxCurrent);

                        SetNewUnitOnCellS.Set(UnitTypes.King, PlayerTypes.Second, cellUdxCurrent);
                        //}
                    }

                    if (x == 8 && y == 3)
                    {
                        _e.AdultForestC(cellUdxCurrent).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        //if (withTraining)
                        //{
                        _e.MountainC(cellUdxCurrent).Resources = 0;

                        SetNewUnitOnCellS.Set(UnitTypes.Pawn, PlayerTypes.Second, cellUdxCurrent);


                        _extraTWC[cellUdxCurrent].Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.Second, ValuesChessy.MaxShieldProtection(LevelTypes.Second));

                        var needShield = UnityEngine.Random.Range(0f, 1f) >= StartGameValues.PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT;

                        if (needShield)
                        {
                            _extraTWC[cellUdxCurrent].Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.Second, ValuesChessy.MaxShieldProtection(LevelTypes.Second));
                        }
                        else
                        {
                            _extraTWC[cellUdxCurrent].Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.First, ValuesChessy.MaxShieldProtection(LevelTypes.First));
                        }
                        //}
                    }

                    if (cellUdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        _e.AdultForestC(cellUdxCurrent).Resources = 0.4f;
                    }
                    else if (cellUdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        ClearAllEnvironment(cellUdxCurrent);
                    }
                    //else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                    //{
                    //    MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    //}
                    //else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                    //{
                    //    MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    //    _eMG.HillC(cell_0).Resources = ValuesChessy.MAX_RESOURCES / 5;
                    //}
                    else if (cellUdxCurrent == KeyIndexCellsForLesson.CELL_MOUNTAIN_LESSON)
                    {
                        ClearAllEnvironment(cellUdxCurrent);
                        _e.MountainC(cellUdxCurrent).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                    }
                }
            }

            GetDataCellsS.GetDataCells();
        }

        internal void SetClouds(in byte centerCellIdx)
        {
            _cloudCs[centerCellIdx].IsCenter = true;
            SetDataAndSkinCloud(centerCellIdx);

            _shiftCloudCs[centerCellIdx].WhereNeedShiftIdxCell = _e.GetIdxCellByDirectAround(centerCellIdx, _windC.DirectT);

            foreach (var aroundCell_0 in _e.IdxsCellsAround(centerCellIdx))
            {
                SetDataAndSkinCloud(aroundCell_0);
            }

            void SetDataAndSkinCloud(in byte cellIdx)
            {
                for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
                {
                    if (_cellCs[currentCellIdx].IsBorder) continue;

                    if (_e.CloudWhereViewDataOnCellC(currentCellIdx).HaveDataReference) continue;

                    _e.CloudWhereViewDataOnCellC(currentCellIdx).DataIdxCell = cellIdx;
                    _e.CloudWhereViewDataOnCellC(cellIdx).ViewIdxCell = currentCellIdx;


                    break;
                }
            }
        }
    }
}