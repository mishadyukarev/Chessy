using Chessy.Common;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Photon.Pun;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class StartGameS_M : SystemModel
    {
        internal StartGameS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Start(in bool withTraining)
        {
            sMG.MasterSs.ResetAllS.ResetAll();

            eMG.ZoneInfoC.IsActiveFriend = eMG.Common.GameModeTC.Is(GameModeTypes.WithFriendOffline);
            eMG.WhoseMovePlayerTC.PlayerT = StartValues.WHOSE_MOVE;
            eMG.CellClickTC.CellClickT = StartValues.CELL_CLICK;

            eMG.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.SPEED_WIND_IN_START_GAME, StartValues.MAX_SPEED_WIND, StartValues.MIN_SPEED_WIND);
            eMG.WeatherE.SunSideTC.SunSideT = StartValues.SUN_SIDE;
            eMG.WeatherE.CloudC.Center = StartValues.CELL_IDX_START_GAME_CLOUD;

            eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);

            eMG.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = StartValues.PEOPLE_IN_CITY;
                eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = StartValues.MAX_AVAILABLE_PAWN;

                if(playerT == PlayerTypes.Second)
                {
                    if (eMG.Common.GameModeT == GameModeTypes.TrainingOffline)
                        eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable += 5;
                }



                eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;
                eMG.PlayerInfoE(playerT).WoodForBuyHouse = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;

                eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = StartValues.Resources(resT);
                }

                if (withTraining)
                {
                    if (playerT == PlayerTypes.First)
                    {
                        //eMG.ResourcesC(playerT, ResourceTypes.Food).Resources = 3f;
                        //eMG.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }
                }
            }



            if (eMG.Common.GameModeTC.IsOffline)
            {
                eMG.CurPlayerIT = PlayerTypes.First;
            }
            else
            {
                eMG.CurPlayerIT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = eMG.XyCellC(cell_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    eMG.HaveFire(cell_0) = false;


                    if (!eMG.IsBorder(cell_0))
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                eMG.MountainC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    eMG.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                eMG.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                            eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (eMG.HaveRiverC(cell_0).HaveRive(dir))
                            {
                                var idx_next = eMG.AroundCellsE(cell_0).IdxCell(dir);

                                eMG.RiverTC(idx_next).RiverT = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = eMG.AroundCellsE(cell_0).IdxCell(dir);

                            eMG.RiverTC(idx_next).RiverT = RiverTypes.Corner;
                        }


                        if (cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_1_PLAYER || cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_2_PLAYER)
                        {
                            sMG.MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    if (!eMG.IsBorder(cell_0))
                    {
                        if (eMG.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!eMG.MountainC(eMG.AroundCellsE(cell_0).IdxCell(dirT)).HaveAnyResources && !eMG.BuildingTC(idx_1).HaveBuilding)
                                    {
                                        eMG.HillC(idx_1).Resources = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
            {
                eMG.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = eMG.XyCellC(cell_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        //if (withTraining)
                        //{
                            eMG.MountainC(cell_0).Resources = 0;

                            sMG.MasterSs.TryDestroyAdultForestS.TryDestroy(cell_0);

                            sMG.SetNewUnitOnCellS(UnitTypes.King, PlayerTypes.Second, cell_0);
                        //}
                    }

                    if (x == 8 && y == 3)
                    {
                        eMG.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        //if (withTraining)
                        //{
                            eMG.MountainC(cell_0).Resources = 0;

                            sMG.SetNewUnitOnCellS(UnitTypes.Pawn, PlayerTypes.Second, cell_0);

                            sMG.UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second));

                            var needShield = UnityEngine.Random.Range(0f, 1f) >= StartValues.PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT;

                            if (needShield)
                            {
                                sMG.UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second));
                            }
                            else
                            {
                                sMG.UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.First, ToolWeaponValues.ShieldProtection(LevelTypes.First));
                            }
                        //}
                    }

                    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        eMG.AdultForestC(cell_0).Resources = 0.4f;
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        sMG.MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    }
                    //else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                    //{
                    //    sMG.MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    //}
                    //else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                    //{
                    //    sMG.MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    //    eMG.HillC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES / 5;
                    //}
                    else if (cell_0 == StartValues.CELL_MOUNTAIN_LESSON)
                    {
                        sMG.MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                        eMG.MountainC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }


            eMG.NeedUpdateView = true;
            sMG.MasterSs.GetDataCellsS.Run();
        }
    }
}