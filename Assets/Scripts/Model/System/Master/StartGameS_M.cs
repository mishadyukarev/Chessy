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
            ResetAll();

            _e.ZoneInfoC.IsActiveFriend = _e.GameModeT.Is(GameModeTypes.WithFriendOffline);
            _e.WhoseMovePlayerT = StartGameValues.WHOSE_MOVE;
            _e.CellClickT = StartGameValues.CELL_CLICK;

            _e.WeatherE.WindC = new WindC() { DirectT = StartGameValues.DIRECT_WIND, Speed = StartGameValues.SPEED_WIND };
            _e.SunSideT = StartGameValues.SUN_SIDE;
            _e.CenterCloudCellIdx = StartGameValues.CLOUD_CELL_INDEX;

            _e.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartGameValues.SELECTED_TOOL_WEAPON, StartGameValues.SELECTED_LEVEL_TOOL_WEAPON);

            _e.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _e.PawnPeopleInfoC(playerT).PeopleInCity = StartGameValues.PEOPLE_IN_CITY;

                if (playerT == PlayerTypes.Second)
                {
                    if (_e.GameModeT == GameModeTypes.TrainingOffline)
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



            if (_e.GameModeT.IsOffline())
            {
                _e.CurrentPlayerIT = PlayerTypes.First;
            }
            else
            {
                _e.CurrentPlayerIT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    var x = _e.XCell(cell_0);
                    var y = _e.YCell(cell_0);

                    _e.HaveFire(cell_0) = false;


                    if (!_e.IsBorder(cell_0))
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.Mountain))
                            {
                                _e.MountainC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    _e.AdultForestC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartGameValues.SpawnPercentEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _e.AdultForestC(cell_0).SetRandom(ValuesChessy.MININMUM_AMOUNT_RESOURCES_ENVIRONMENT, ValuesChessy.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            _e.SetRiverT(cell_0, RiverTypes.Start);
                            _e.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            _e.SetRiverT(cell_0, RiverTypes.Start);
                            _e.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                            _e.HaveRiverC(cell_0).HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            _e.SetRiverT(cell_0, RiverTypes.Start);
                            _e.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (_e.HaveRiverC(cell_0).HaveRive(dir))
                            {
                                var idx_next = _e.AroundCellsE(cell_0).IdxCell(dir);

                                _e.SetRiverT(idx_next, RiverTypes.EndRiver);
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _e.AroundCellsE(cell_0).IdxCell(dir);

                            _e.SetRiverT(idx_next, RiverTypes.Corner);
                        }


                        if (cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_1_PLAYER || cell_0 == StartGameValues.CELL_IDX_FOR_CLEARING_FOREST_FOR_2_PLAYER)
                        {
                            ClearAllEnvironment(cell_0);
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    if (!_e.IsBorder(cell_0))
                    {
                        if (_e.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _e.AroundCellsE(cell_0).IdxCell(dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!_e.MountainC(_e.AroundCellsE(cell_0).IdxCell(dirT)).HaveAnyResources && !_e.HaveBuildingOnCell(idx_1))
                                    {
                                        _e.HillC(idx_1).Resources = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (_e.GameModeT.Is(GameModeTypes.TrainingOffline))
            {
                _e.SetResourcesInInventory(PlayerTypes.Second, ResourceTypes.Food, 999999);


                for (byte cellUdxCurrent = 0; cellUdxCurrent < IndexCellsValues.CELLS; cellUdxCurrent++)
                {
                    var x = _e.XCell(cellUdxCurrent);
                    var y = _e.YCell(cellUdxCurrent);

                    if (x == 7 && y == 8)
                    {
                        //if (withTraining)
                        //{
                        _e.MountainC(cellUdxCurrent).Resources = 0;

                        TryDestroyAdultForest(cellUdxCurrent);

                        SetNewUnitOnCellS(UnitTypes.King, PlayerTypes.Second, cellUdxCurrent);
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

                        SetNewUnitOnCellS(UnitTypes.Pawn, PlayerTypes.Second, cellUdxCurrent);


                        _e.UnitExtraTWE(cellUdxCurrent).Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.Second, ValuesChessy.MaxShieldProtection(LevelTypes.Second));

                        var needShield = UnityEngine.Random.Range(0f, 1f) >= StartGameValues.PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT;

                        if (needShield)
                        {
                            _e.UnitExtraTWE(cellUdxCurrent).Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.Second, ValuesChessy.MaxShieldProtection(LevelTypes.Second));
                        }
                        else
                        {
                            _e.UnitExtraTWE(cellUdxCurrent).Set(ToolsWeaponsWarriorTypes.Shield, LevelTypes.First, ValuesChessy.MaxShieldProtection(LevelTypes.First));
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
                        _e.MountainC(cellUdxCurrent).Resources = ValuesChessy.MAX_RESOURCES;
                    }
                }
            }

            GetDataCellsS.GetDataCells();
        }
    }
}