using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Model.System;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    public sealed class SystemsModelGame : SystemModelGameAbs, IToggleScene, IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly List<IEcsRunSystem> _runs;
        readonly CellSs[] _cellSs = new CellSs[StartValues.CELLS];

        internal readonly MistakeS MistakeS;
        internal readonly TakeAdultForestResourcesS TakeAdultForestResourcesS;


        internal readonly SetNewUnitOnCellS SetNewUnitS;
        internal readonly ShiftUnitS ShiftUnitS;

        internal readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        internal readonly BuyS_M BuyS_M;
        internal readonly MeltS_M MeltS_M;
        internal readonly BuyBuildingS_M BuyBuildingS_M;
        internal readonly GetHeroS_M GetHeroS_M;
        internal readonly ReadyS_M ReadyS_M;
        internal readonly DonerS_M DonerS_M;
        internal readonly UpdateS_M UpdateS_M;
        internal readonly GetDataCellsS_M GetDataCellsS;
        internal readonly AttackUnit_M AttackUnit_M;
        internal readonly SetUnitS_M SetUnitS_M;
        internal readonly ShiftUnitS_M ShiftUnitS_M;
        internal readonly SeedPawnS_M SeedPawnS_M;
        internal readonly BuildFarmS_M BuildFarmS_M;
        internal readonly SetConditionUnitS_M SetConditionUnitS_M;


        public readonly SystemsModelGameForUI ForUISystems;

        internal CellSs CellSs(in byte cell_0) => _cellSs[cell_0];


        public SystemsModelGame(in EntitiesModelCommon eMCommon, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _eMCommon = eMCommon;

            _runs = new List<IEcsRunSystem>()
            {
                new InputS(eMGame),
                new RayS(eMGame),
                new SelectorS(eMCommon, eMGame),
            };


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _cellSs[cell_0] = new CellSs(cell_0, this, eMGame);
            }

            MistakeS = new MistakeS(eMGame);
            TakeAdultForestResourcesS = new TakeAdultForestResourcesS(eMGame);

            SetNewUnitS = new SetNewUnitOnCellS(this, eMGame);
            ShiftUnitS = new ShiftUnitS(this, eMGame);

            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(eMGame);
            BuyS_M = new BuyS_M(eMGame);
            MeltS_M = new MeltS_M(eMGame);
            BuyBuildingS_M = new BuyBuildingS_M(eMGame);
            GetHeroS_M = new GetHeroS_M(eMGame);
            ReadyS_M = new ReadyS_M(eMGame);
            UpdateS_M = new UpdateS_M(this, eMGame);
            DonerS_M = new DonerS_M(UpdateS_M, eMGame);
            GetDataCellsS = new GetDataCellsS_M(this, eMGame);
            AttackUnit_M = new AttackUnit_M(this, eMGame);
            SetUnitS_M = new SetUnitS_M(this, eMGame);
            ShiftUnitS_M = new ShiftUnitS_M(this, eMGame);
            SeedPawnS_M = new SeedPawnS_M(this, eMGame);
            BuildFarmS_M = new BuildFarmS_M(this, eMGame);
            SetConditionUnitS_M = new SetConditionUnitS_M(this, eMGame);

            ForUISystems = new SystemsModelGameForUI(this, eMGame);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;


            e.NeedUpdateView = true;

            e.ZoneInfoC.IsActiveFriend = _eMCommon.GameModeTC.Is(GameModes.WithFriendOff);
            e.WhoseMove.Player = StartValues.WHOSE_MOVE;
            e.CellClickTC.Click = StartValues.CELL_CLICK;
            e.IsSelectedCity = false;
            e.HaveTreeUnit = false;
            e.MistakeC.MistakeT = MistakeTypes.None;
            e.WinnerC.Player = PlayerTypes.None;
            e.ZoneInfoC = default;
            e.CellsC = default;

            e.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.STRENGTH_WIND, StartValues.MAX_STREANGTH_WIND, StartValues.MIN_SNREANGTH_WIND);
            e.WeatherE.SunC = new SunC(StartValues.SUN_SIDE);
            e.WeatherE.CloudC = new CloudC(StartValues.START_CLOUD);


            e.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);






            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                CellSs(cell_0).ClearAllEnvironmentS.Clear();
                CellSs(cell_0).ClearUnitS.Clear();

                e.BuildingTC(cell_0).Building = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    e.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                e.PlayerInfoE(playerT).ToggleScene(newSceneT);


                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    e.PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }

                if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
                {
                    if (playerT == PlayerTypes.First)
                    {
                        e.ResourcesC(playerT, ResourceTypes.Food).Resources = 3;
                        e.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }

                    e.LessonTC.LessonT = (LessonTypes)1;
                }
            }


            switch (_eMCommon.GameModeTC.GameMode)
            {
                case GameModes.TrainingOff:
                    e.CurPlayerITC.Player = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    e.CurPlayerITC.Player = e.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    e.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    e.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = e.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    e.HaveFire(cell_0) = false;


                    if (e.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                e.MountainC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    e.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                e.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            e.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            e.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            e.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            e.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                            e.RiverEs(cell_0).HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            e.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            e.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (e.RiverEs(cell_0).HaveRive(dir))
                            {
                                var idx_next = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                                e.RiverEs(idx_next).RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                            e.RiverEs(idx_next).RiverTC.River = RiverTypes.Corner;
                        }


                        if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                e.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                            else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                            {
                                CellSs(cell_0).ClearAllEnvironmentS.Clear();
                            }
                            else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                            {
                                CellSs(cell_0).ClearAllEnvironmentS.Clear();
                            }
                            else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                            {
                                CellSs(cell_0).ClearAllEnvironmentS.Clear();
                                e.HillC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                            else if (cell_0 == StartValues.CELL_MOUNTAIN_LESSON)
                            {
                                CellSs(cell_0).ClearAllEnvironmentS.Clear();
                                e.MountainC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    if (e.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (e.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.5)
                                {
                                    if (!e.MountainC(e.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx).HaveAnyResources && !e.BuildingTC(idx_1).HaveBuilding)
                                    {
                                        e.HillC(idx_1).Resources = UnityEngine.Random.Range(0f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
            {
                e.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = e.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        e.MountainC(cell_0).Resources = 0;

                        TakeAdultForestResourcesS.Take(1f, cell_0);

                        SetNewUnitS.Set(UnitTypes.King, PlayerTypes.Second, cell_0);
                    }


                    if (x == 8 && y == 3)
                    {
                        e.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        e.MountainC(cell_0).Resources = 0;

                        SetNewUnitS.Set(UnitTypes.Pawn, PlayerTypes.Second, cell_0);

                        var needSword = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
                        CellSs(cell_0).SetExtraTWS.Set(needSword ? ToolWeaponTypes.Sword : ToolWeaponTypes.Shield, needSword ? LevelTypes.Second : LevelTypes.First, ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST);
                    }
                }
            }



           GetDataCellsS.Run();
        }

        public void Run()
        {
            if (_eMCommon.SceneTC.Scene != SceneTypes.Game) return;

            _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());
        }
    }
}