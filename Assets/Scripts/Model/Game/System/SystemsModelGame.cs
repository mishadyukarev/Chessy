using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    public sealed class SystemsModelGame : IUpdate
    {
        readonly EntitiesModelGame _eMG;

        readonly List<Action> _runs;

        public readonly SystemsModelCommon CommonSs;
        internal readonly MistakeSs MistakeSs;
        internal readonly MasterSystems MasterSs;
        public readonly SystemsModelGameForUI ForUISystems;

        internal UnitSystems UnitSs => MasterSs.UnitSs;
        internal BuildingSystems BuildingSs => MasterSs.BuildingSs;



        public SystemsModelGame(in SystemsModelCommon sMC, in EntitiesModelGame eMG)
        {
            CommonSs = sMC;

            _eMG = eMG;

            _runs = new List<Action>()
            {
                new InputS(this, eMG).Update,
                new CheatsS(this, eMG).Update,
                new RayS(this, eMG).Update,
                new SelectorS(this, eMG).Update,

                new Chessy.Game.MistakeS(this, eMG).Update,
            };

            MistakeSs = new MistakeSs(this, eMG);
            MasterSs = new MasterSystems(this, eMG);
            ForUISystems = new SystemsModelGameForUI(this, eMG);
        }

        void ResetAll()
        {
            _eMG.IsStartedGame = default;
            _eMG.MotionsC.Motions = default;
            _eMG.ZoneInfoC.IsActiveFriend = default;
            _eMG.ZoneInfoC = default;
            _eMG.WhoseMovePlayerTC.PlayerT = default;
            _eMG.CellClickTC.CellClickT = default;
            _eMG.IsSelectedCity = default;
            _eMG.HaveTreeUnit = default;
            _eMG.MistakeT = default;
            _eMG.WinnerPlayerT = default;
            _eMG.CellsC = default;
            _eMG.CurPlayerIT = default;

            _eMG.WeatherE.WindC = new WindC(default, default, default, default);
            _eMG.WeatherE.SunSideTC.SunSideT = default;
            _eMG.WeatherE.CloudC.Center = default;

            _eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(default, default);

            _eMG.LessonT = default;

            for (byte cellIdx = 0; cellIdx < StartValues.CELLS; cellIdx++)
            {
                MasterSs.ClearAllEnvironmentS.Clear(cellIdx);

                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++) 
                    _eMG.HealthTrail(cellIdx).Health(dirT) = 0;

                UnitSs.ClearUnit(cellIdx);
                _eMG.BuildingTC(cellIdx).BuildingT = default;
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).IsReady = default;

                _eMG.PlayerInfoE(playerT).BuildingsInfoC.Clear();


                _eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People = default;
                _eMG.PlayerInfoE(playerT).PawnInfoE.MaxAvailable = default;
                _eMG.PlayerInfoE(playerT).PawnInfoE.PawnsInGame = default;

                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = default;
                _eMG.PlayerInfoE(playerT).WoodForBuyHouse = default;
                _eMG.PlayerInfoE(playerT).IsReady = default;

                _eMG.PlayerInfoE(playerT).GodInfoE = default;
                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = default;
                _eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT) = default;
                    }

                    for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT).IdxC.Clear();
                    }
                }

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = default;
                }
            }
        }



        public void StartGame(in bool withTraining)
        {
            ResetAll();

            _eMG.ZoneInfoC.IsActiveFriend = _eMG.Common.GameModeTC.Is(GameModeTypes.WithFriendOffline);
            _eMG.WhoseMovePlayerTC.PlayerT = StartValues.WHOSE_MOVE;
            _eMG.CellClickTC.CellClickT = StartValues.CELL_CLICK;

            _eMG.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.SPEED_WIND, StartValues.MAX_SPEED_WIND, StartValues.MIN_SPEED_WIND);
            _eMG.WeatherE.SunSideTC.SunSideT = StartValues.SUN_SIDE;
            _eMG.WeatherE.CloudC.Center = StartValues.START_CLOUD;

            _eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);

            _eMG.LessonT = withTraining ? (LessonTypes)1 : 0;


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People = StartValues.PEOPLE_IN_CITY;
                _eMG.PlayerInfoE(playerT).PawnInfoE.MaxAvailable = StartValues.MAX_AVAILABLE_PAWN;

                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;
                _eMG.PlayerInfoE(playerT).WoodForBuyHouse = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;

                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = StartValues.Resources(resT);
                }

                if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                {
                    if (playerT == PlayerTypes.First)
                    {
                        _eMG.ResourcesC(playerT, ResourceTypes.Food).Resources = 3;
                        _eMG.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }
                }
            }



            if (_eMG.Common.GameModeTC.IsOffline)
            {
                _eMG.CurPlayerITC.PlayerT = PlayerTypes.First;
            }
            else
            {
                _eMG.CurPlayerITC.PlayerT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = _eMG.XyCellC(cell_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    _eMG.HaveFire(cell_0) = false;


                    if (_eMG.IsActiveParentSelf(cell_0))
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                _eMG.MountainC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    _eMG.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                _eMG.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            _eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            _eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            _eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            _eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                            _eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            _eMG.RiverTC(cell_0).RiverT = RiverTypes.Start;
                            _eMG.HaveRiverC(cell_0).HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (_eMG.HaveRiverC(cell_0).HaveRive(dir))
                            {
                                var idx_next = _eMG.AroundCellsE(cell_0).IdxCell(dir);

                                _eMG.RiverTC(idx_next).RiverT = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _eMG.AroundCellsE(cell_0).IdxCell(dir);

                            _eMG.RiverTC(idx_next).RiverT = RiverTypes.Corner;
                        }


                        if (cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_1_PLAYER || cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_2_PLAYER)
                        {
                            MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    if (_eMG.IsActiveParentSelf(cell_0))
                    {
                        if (_eMG.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _eMG.AroundCellsE(cell_0).IdxCell(dirT);

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.7)
                                {
                                    if (!_eMG.MountainC(_eMG.AroundCellsE(cell_0).IdxCell(dirT)).HaveAnyResources && !_eMG.BuildingTC(idx_1).HaveBuilding)
                                    {
                                        _eMG.HillC(idx_1).Resources = UnityEngine.Random.Range(0.5f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
            {
                _eMG.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = _eMG.XyCellC(cell_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        _eMG.MountainC(cell_0).Resources = 0;

                        MasterSs.TryDestroyAdultForestS.TryDestroy(cell_0);

                        UnitSs.SetNewOnCellS.Set(UnitTypes.King, PlayerTypes.Second, cell_0);
                    }


                    if (x == 8 && y == 3)
                    {
                        _eMG.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        _eMG.MountainC(cell_0).Resources = 0;

                        UnitSs.SetNewOnCellS.Set(UnitTypes.Pawn, PlayerTypes.Second, cell_0);

                        UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second));

                        var needShield = UnityEngine.Random.Range(0f, 1f) >= StartValues.PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT;

                        if (needShield)
                        {
                            UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second));
                        }
                        else
                        {
                            UnitSs.SetExtraToolWeapon(cell_0, ToolWeaponTypes.Shield, LevelTypes.First, ToolWeaponValues.ShieldProtection(LevelTypes.First));
                        }
                    }

                    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        _eMG.AdultForestC(cell_0).Resources = 0.4f;
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                    {
                        MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                    {
                        MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                    {
                        MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                        _eMG.HillC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES / 5;
                    }
                    else if (cell_0 == StartValues.CELL_MOUNTAIN_LESSON)
                    {
                        MasterSs.ClearAllEnvironmentS.Clear(cell_0);
                        _eMG.MountainC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }


            _eMG.NeedUpdateView = true;
            MasterSs.GetDataCellsS.Run();
        }
        public void TurnOffTraining()
        {
            StartGame(false);
        }

        public void Update()
        {
            _runs.ForEach((Action action) => action());

            _eMG.ForUpdateViewTimer += Time.deltaTime;

            if (_eMG.ForUpdateViewTimer >= 0.5f)
            {
                _eMG.NeedUpdateView = true;
                _eMG.ForUpdateViewTimer = 0;
            }
        }
    }
}