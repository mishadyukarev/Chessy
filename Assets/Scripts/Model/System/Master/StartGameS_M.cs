using Chessy.Model.Enum;
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

            _zonesInfoC.IsActiveFriend = AboutGameC.GameModeT.Is(GameModeTypes.WithFriendOffline);
            AboutGameC.CellClickT = StartGameValues.CELL_CLICK;

            WindC.Set(StartGameValues.DIRECT_WIND, StartGameValues.SPEED_WIND);
            SunC.SunSideT = StartGameValues.SUN_SIDE;


            SetClouds(StartGameValues.CLOUD_CELL_INDEX);


            _selectedToolWeaponC.ToolWeaponT = StartGameValues.SELECTED_TOOL_WEAPON;
            _selectedToolWeaponC.LevelT = StartGameValues.SELECTED_LEVEL_TOOL_WEAPON;

            AboutGameC.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                PawnPeopleInfoC(playerT).PeopleInCity = StartGameValues.PEOPLE_IN_CITY;

                if (playerT == PlayerTypes.Second)
                {
                    if (AboutGameC.GameModeT == GameModeTypes.TrainingOffline)
                        PlayerInfoC(playerT).AmountBuiltHouses += 5;
                }



                PlayerInfoE(playerT).PlayerInfoC.HaveKingInInventor = true;
                PlayerInfoE(playerT).PlayerInfoC.WoodForBuyHouse = StartGameValues.NEED_WOOD_FOR_BUILDING_HOUSE;

                PlayerInfoE(playerT).GodInfoC.HaveGodInInventor = true;

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    ResourcesInInventoryC(playerT).Set(resT, StartGameValues.AmountResourceEveryone(resT));
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



            if (AboutGameC.GameModeT.IsOffline())
            {
                AboutGameC.CurrentPlayerIT = PlayerTypes.First;
            }
            else
            {
                AboutGameC.CurrentPlayerIT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    var x = XyCellC(cell_0).X;
                    var y = XyCellC(cell_0).Y;

                    _fireCs[cell_0].HaveFire = false;


                    if (!_cellCs[cell_0].IsBorder)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.Mountain))
                            {
                                _environmentCs[cell_0].SetRandom(EnvironmentTypes.Mountain, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    _environmentCs[cell_0].SetRandom(EnvironmentTypes.AdultForest, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _environmentCs[cell_0].SetRandom(EnvironmentTypes.AdultForest, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
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
                                var idx_next = _cellsByDirectAroundC[cell_0].Get(dir);

                                _riverCs[idx_next].RiverT = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _cellsByDirectAroundC[cell_0].Get(dir);

                            _riverCs[idx_next].RiverT = RiverTypes.Corner;
                        }


                        if (cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_1_PLAYER || cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_2_PLAYER)
                        {
                            _environmentCs[cell_0].Dispose();
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    if (!_cellCs[cell_0].IsBorder)
                    {
                        if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _cellsByDirectAroundC[cell_0].Get(dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!_environmentCs[_cellsByDirectAroundC[cell_0].Get(dirT)].HaveEnvironment(EnvironmentTypes.Mountain) && !_buildingCs[idx_1].HaveBuilding)
                                    {
                                        _environmentCs[idx_1].ResourcesRef(EnvironmentTypes.Hill) = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (AboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
            {
                ResourcesInInventoryC(PlayerTypes.Second).Set(ResourceTypes.Food, 999999);


                for (byte cellUdxCurrent = 0; cellUdxCurrent < IndexCellsValues.CELLS; cellUdxCurrent++)
                {
                    var x = XyCellC(cellUdxCurrent).X;
                    var y = XyCellC(cellUdxCurrent).Y;

                    if (x == 7 && y == 8)
                    {
                        //if (withTraining)
                        //{
                        _environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, 0);

                        TryDestroyAdultForest(cellUdxCurrent);

                        SetNewUnitOnCellS.Set(UnitTypes.King, PlayerTypes.Second, cellUdxCurrent);
                        //}
                    }

                    if (x == 8 && y == 3)
                    {
                        _environmentCs[cellUdxCurrent].Set(EnvironmentTypes.AdultForest, 1);
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        //if (withTraining)
                        //{
                        _environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, 0);

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
                        _environmentCs[cellUdxCurrent].Set(EnvironmentTypes.AdultForest, 0.4f);
                    }
                    else if (cellUdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        _environmentCs[cellUdxCurrent].Dispose();
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
                        _environmentCs[cellUdxCurrent].Dispose();
                        _environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }
                }
            }

            GetDataCellsS.GetDataCells();
        }

        internal void SetClouds(in byte centerCellIdx)
        {
            CloudC(centerCellIdx).IsCenter = true;
            SetDataAndViewCloud(centerCellIdx);

            CloudShiftC(centerCellIdx).WhereNeedShiftIdxCell = _cellsByDirectAroundC[centerCellIdx].Get(WindC.DirectT);

            foreach (var aroundCell_0 in _idxsAroundCellCs[centerCellIdx].IdxCellsAroundArray)
            {
                SetDataAndViewCloud(aroundCell_0);
            }
        }

        internal void SetDataAndViewCloud(in byte cellIdx)
        {
            for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            {
                if (CellC(currentCellIdx).IsBorder) continue;

                if (CloudViewDataC(currentCellIdx).HaveDataReference) continue;

                CloudViewDataC(currentCellIdx).DataIdxCell = cellIdx;
                CloudViewDataC(cellIdx).ViewIdxCell = currentCellIdx;


                break;
            }
        }
    }
}