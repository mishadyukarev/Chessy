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

            zonesInfoC.IsActiveFriend = aboutGameC.GameModeT.Is(GameModeTypes.WithFriendOffline);
            aboutGameC.CellClickT = StartGameValues.CELL_CLICK;

            windC.Set(StartGameValues.DIRECT_WIND, StartGameValues.SPEED_WIND);
            sunC.SunSideT = StartGameValues.SUN_SIDE;


            SetClouds(StartGameValues.CLOUD_CELL_INDEX);


            selectedToolWeaponC.ToolWeaponT = StartGameValues.SELECTED_TOOL_WEAPON;
            selectedToolWeaponC.LevelT = StartGameValues.SELECTED_LEVEL_TOOL_WEAPON;

            aboutGameC.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                PawnPeopleInfoC(playerT).PeopleInCity = StartGameValues.PEOPLE_IN_CITY;

                if (playerT == PlayerTypes.Second)
                {
                    if (aboutGameC.GameModeT == GameModeTypes.TrainingOffline)
                        playerInfoCs[(byte)playerT].AmountBuiltHouses += 5;
                }



                playerInfoCs[(byte)playerT].HaveKingInInventor = true;
                playerInfoCs[(byte)playerT].WoodForBuyHouse = StartGameValues.NEED_WOOD_FOR_BUILDING_HOUSE;

                godInfoCs[(byte)playerT].HaveGodInInventor = true;

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



            if (aboutGameC.GameModeT.IsOffline())
            {
                aboutGameC.CurrentPlayerIT = PlayerTypes.First;
            }
            else
            {
                aboutGameC.CurrentPlayerIT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    var x = xyCellsCs[cell_0].X;
                    var y = xyCellsCs[cell_0].Y;

                    fireCs[cell_0].HaveFire = false;


                    if (!cellCs[cell_0].IsBorder)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.Mountain))
                            {
                                environmentCs[cell_0].SetRandom(EnvironmentTypes.Mountain, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    environmentCs[cell_0].SetRandom(EnvironmentTypes.AdultForest, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                            {
                                environmentCs[cell_0].SetRandom(EnvironmentTypes.AdultForest, ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            riverCs[cell_0].RiverT = RiverTypes.Start;
                            haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            riverCs[cell_0].RiverT = RiverTypes.Start;
                            haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                            haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            riverCs[cell_0].RiverT = RiverTypes.Start;
                            haveRiverAroundCellCs[cell_0].HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (haveRiverAroundCellCs[cell_0].HaveRive(dir))
                            {
                                var idx_next = cellsByDirectAroundC[cell_0].Get(dir);

                                riverCs[idx_next].RiverT = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = cellsByDirectAroundC[cell_0].Get(dir);

                            riverCs[idx_next].RiverT = RiverTypes.Corner;
                        }


                        if (cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_1_PLAYER || cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_2_PLAYER)
                        {
                            environmentCs[cell_0].Dispose();
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    if (!cellCs[cell_0].IsBorder)
                    {
                        if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = cellsByDirectAroundC[cell_0].Get(dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!environmentCs[cellsByDirectAroundC[cell_0].Get(dirT)].HaveEnvironment(EnvironmentTypes.Mountain) && !buildingCs[idx_1].HaveBuilding)
                                    {
                                        environmentCs[idx_1].ResourcesRef(EnvironmentTypes.Hill) = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (aboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
            {
                ResourcesInInventoryC(PlayerTypes.Second).Set(ResourceTypes.Food, 999999);


                for (byte cellUdxCurrent = 0; cellUdxCurrent < IndexCellsValues.CELLS; cellUdxCurrent++)
                {
                    var x = xyCellsCs[cellUdxCurrent].X;
                    var y = xyCellsCs[cellUdxCurrent].Y;

                    if (x == 7 && y == 8)
                    {
                        //if (withTraining)
                        //{
                        environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, 0);

                        TryDestroyAdultForest(cellUdxCurrent);

                        SetNewUnitOnCellS.Set(UnitTypes.King, PlayerTypes.Second, cellUdxCurrent);
                        //}
                    }

                    if (x == 8 && y == 3)
                    {
                        environmentCs[cellUdxCurrent].Set(EnvironmentTypes.AdultForest, 1);
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        //if (withTraining)
                        //{
                        environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, 0);

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
                        environmentCs[cellUdxCurrent].Set(EnvironmentTypes.AdultForest, 0.4f);
                    }
                    else if (cellUdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        environmentCs[cellUdxCurrent].Dispose();
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
                        environmentCs[cellUdxCurrent].Dispose();
                        environmentCs[cellUdxCurrent].Set(EnvironmentTypes.Mountain, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }
                }
            }

            GetDataCellsS.GetDataCells();
        }

        internal void SetClouds(in byte centerCellIdx)
        {
            cloudCs[centerCellIdx].IsCenter = true;
            SetDataAndViewCloud(centerCellIdx);

            shiftCloudCs[centerCellIdx].WhereNeedShiftIdxCell = cellsByDirectAroundC[centerCellIdx].Get(windC.DirectT);

            foreach (var aroundCell_0 in idxsAroundCellCs[centerCellIdx].IdxCellsAroundArray)
            {
                SetDataAndViewCloud(aroundCell_0);
            }
        }

        internal void SetDataAndViewCloud(in byte cellIdx)
        {
            for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            {
                if (cellCs[currentCellIdx].IsBorder) continue;

                if (cloudWhereViewDataCs[currentCellIdx].HaveDataReference) continue;

                cloudWhereViewDataCs[currentCellIdx].DataIdxCell = cellIdx;
                cloudWhereViewDataCs[cellIdx].ViewIdxCell = currentCellIdx;


                break;
            }
        }
    }
}