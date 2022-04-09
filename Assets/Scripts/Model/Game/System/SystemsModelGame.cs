using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    public sealed class SystemsModelGame : IToggleScene, IUpdate
    {
        readonly EntitiesModelCommon _eMC;
        readonly EntitiesModelGame _eMG;

        readonly List<Action> _runs;


        #region Environment

        internal readonly TakeAdultForestResourcesS TakeAdultForestResourcesS;
        internal readonly DestroyAdultForestS DestroyAdultForestS;
        internal readonly ClearAllEnvironmentS ClearAllEnvironmentS;

        #endregion


        #region Mistake

        internal readonly MistakeS MistakeS;
        internal readonly SetMistakeS SetMistakeS;

        #endregion


        #region Building

        internal readonly BuildS BuildS;
        internal readonly AttackBuildingS DestroyBuildingS;
        internal readonly ClearBuildingS ClearBuildingS;

        #endregion


        #region Else

        internal DestroyAllTrailS DestroyAllTrailS;

        #endregion


        internal readonly UnitSystems UnitSs;
        internal readonly MasterSystems MasterSs;

        public readonly SystemsModelGameForUI ForUISystems;

        public SystemsModelGame(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in EntitiesModelGame eMG)
        {
            _eMC = eMC;
            _eMG = eMG;

            _runs = new List<Action>()
            {
                new InputS(sMC, eMC, this, eMG).Update,
                new CheatsS(sMC, eMC, this, eMG).Update,
                new RayS(sMC, eMC, this, eMG).Update,
                new SelectorS(sMC, eMC, this, eMG).Update,

                new Chessy.Game.MistakeS(sMC, eMC, this, eMG).Update,
            };

            MistakeS = new MistakeS(sMC, eMC, this, eMG);
            SetMistakeS = new SetMistakeS(sMC, eMC, this, eMG);

            TakeAdultForestResourcesS = new TakeAdultForestResourcesS(sMC, eMC, this, eMG);
            ClearAllEnvironmentS = new ClearAllEnvironmentS(sMC, eMC, this, eMG);
            DestroyAdultForestS = new DestroyAdultForestS(sMC, eMC, this, eMG);

            BuildS = new BuildS(sMC, eMC, this, eMG);
            ClearBuildingS = new ClearBuildingS(sMC, eMC, this, eMG);
            DestroyBuildingS = new AttackBuildingS(sMC, eMC, this, eMG);

            DestroyAllTrailS = new DestroyAllTrailS(sMC, eMC, this, eMG);

            UnitSs = new UnitSystems(sMC, eMC, this, eMG);
            MasterSs = new MasterSystems(sMC, eMC, this, eMG);

            ForUISystems = new SystemsModelGameForUI(sMC, eMC, this, eMG);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;


            _eMG.NeedUpdateView = true;

            _eMG.IsStartedGame = false;
            _eMG.MotionsC.Motions = 0;
            _eMG.ZoneInfoC.IsActiveFriend = _eMC.GameModeTC.Is(GameModes.WithFriendOff);
            _eMG.WhoseMovePlayerTC.PlayerT = StartValues.WHOSE_MOVE;
            _eMG.CellClickTC.CellClickT = StartValues.CELL_CLICK;
            _eMG.IsSelectedCity = false;
            _eMG.HaveTreeUnit = false;
            _eMG.MistakeTC.MistakeT = MistakeTypes.None;
            _eMG.WinnerPlayerTC.PlayerT = PlayerTypes.None;
            _eMG.ZoneInfoC = default;
            _eMG.CellsC = default;


            _eMG.LessonTC.LessonT = _eMC.GameModeTC.Is(GameModes.TrainingOff) ? (LessonTypes)1 : LessonTypes.None;


            _eMG.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.SPEED_WIND, StartValues.MAX_SPEED_WIND, StartValues.MIN_SPEED_WIND);
            _eMG.WeatherE.SunSideTC = new SunSideTC(StartValues.SUN_SIDE);
            _eMG.WeatherE.CloudC.Center = StartValues.START_CLOUD;


            _eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);






            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                ClearAllEnvironmentS.Clear(cell_0);
                UnitSs.ClearUnitS.Clear(cell_0);

                _eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    _eMG.HealthTrail(cell_0).Health(dirT) = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).IsReady = false;

                _eMG.PlayerInfoE(playerT).BuildingsInfoC.Destroy(BuildingTypes.Market);
                _eMG.PlayerInfoE(playerT).BuildingsInfoC.Destroy(BuildingTypes.Smelter);


                _eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People = StartValues.PEOPLE_IN_CITY;
                _eMG.PlayerInfoE(playerT).PawnInfoE.MaxAvailable = StartValues.MAX_AVAILABLE_PAWN;
                _eMG.PlayerInfoE(playerT).PawnInfoE.PawnsInGame = 0;

                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;
                _eMG.PlayerInfoE(playerT).WoodForBuyHouse = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                _eMG.PlayerInfoE(playerT).IsReady = false;

                _eMG.PlayerInfoE(playerT).GodInfoE = default;
                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;
                _eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    _eMG.PlayerInfoE(playerT).LevelE(levT).StartGame();
                }




                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }

                if (_eMC.GameModeTC.Is(GameModes.TrainingOff))
                {
                    if (playerT == PlayerTypes.First)
                    {
                        _eMG.ResourcesC(playerT, ResourceTypes.Food).Resources = 3;
                        _eMG.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }
                }
            }


            switch (_eMC.GameModeTC.GameModeT)
            {
                case GameModes.TrainingOff:
                    _eMG.CurPlayerITC.PlayerT = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    _eMG.CurPlayerITC.PlayerT = _eMG.WhoseMovePlayerTC.PlayerT;
                    break;

                case GameModes.PublicOn:
                    _eMG.CurPlayerITC.PlayerT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    _eMG.CurPlayerITC.PlayerT = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
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


                        if(cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_1_PLAYER || cell_0 == StartValues.CELL_FOR_CLEAR_FOREST_FOR_2_PLAYER)
                        {
                            ClearAllEnvironmentS.Clear(cell_0);
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


            if (_eMC.GameModeTC.Is(GameModes.TrainingOff))
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

                        DestroyAdultForestS.Destroy(cell_0);

                        UnitSs.SetNewUnitS.Set(UnitTypes.King, PlayerTypes.Second, cell_0);
                    }


                    if (x == 8 && y == 3)
                    {
                        _eMG.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        _eMG.MountainC(cell_0).Resources = 0;

                        UnitSs.SetNewUnitS.Set(UnitTypes.Pawn, PlayerTypes.Second, cell_0);

                        UnitSs.SetExtraTWS.Set(ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second), cell_0);

                        var needShield = UnityEngine.Random.Range(0f, 1f) >= StartValues.PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT;

                        if (needShield)
                        {
                            UnitSs.SetExtraTWS.Set(ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.ShieldProtection(LevelTypes.Second), cell_0);
                        }
                        else
                        {
                            UnitSs.SetExtraTWS.Set(ToolWeaponTypes.Shield, LevelTypes.First, ToolWeaponValues.ShieldProtection(LevelTypes.First), cell_0);
                        }
                    }

                    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        _eMG.AdultForestC(cell_0).Resources = 0.4f;
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                    {
                        ClearAllEnvironmentS.Clear(cell_0);
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                    {
                        ClearAllEnvironmentS.Clear(cell_0);
                    }
                    else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                    {
                        ClearAllEnvironmentS.Clear(cell_0);
                        _eMG.HillC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES / 5;
                    }
                    else if (cell_0 == StartValues.CELL_MOUNTAIN_LESSON)
                    {
                        ClearAllEnvironmentS.Clear(cell_0);
                        _eMG.MountainC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }



            MasterSs.GetDataCellsS_M.Run();
        }

        public void Update()
        {
            _runs.ForEach((Action action) => action());

            _eMG.ForUpdateViewTimer += Time.deltaTime;

            if (_eMG.ForUpdateViewTimer >= 1)
            {
                _eMG.NeedUpdateView = true;
                _eMG.ForUpdateViewTimer = 0;
            }
        }
    }
}