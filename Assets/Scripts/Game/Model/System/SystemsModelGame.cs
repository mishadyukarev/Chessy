using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    public class SystemsModelGame : SystemModelGameAbs, IToggleScene, IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly List<IEcsRunSystem> _runs;

        internal readonly MistakeS MistakeS;
        internal readonly BuildS BuildS;
        internal readonly ClearAllEnvironmentS ClearAllEnvironmentS;
        internal readonly TakeAdultForestResourcesS TakeAdultForestResourcesS;
        internal readonly SystemsModelGameUnit UnitSystems;
        internal readonly SystemsModelGameMaster MasterSystems;

        public readonly SystemsModelGameForUI ForUISystems;

        public SystemsModelGame(in EntitiesModelCommon eMCommon, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _eMCommon = eMCommon;

            _runs = new List<IEcsRunSystem>()
            {
                new InputS(eMGame),
                new RayS(eMGame),
                new SelectorS(eMGame),
            };

            UnitSystems = new SystemsModelGameUnit(this, eMGame);
            MistakeS = new MistakeS(eMGame);
            BuildS = new BuildS(eMGame);
            ClearAllEnvironmentS = new ClearAllEnvironmentS(eMGame);
            TakeAdultForestResourcesS = new TakeAdultForestResourcesS(eMGame);

            MasterSystems = new SystemsModelGameMaster(this, eMGame);
            ForUISystems = new SystemsModelGameForUI(this, eMGame);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;


            eMGame.ZoneInfoC.IsActiveFriend = _eMCommon.GameModeTC.Is(GameModes.WithFriendOff);
            eMGame.WhoseMove = new PlayerTC(StartValues.WHOSE_MOVE);
            eMGame.CellClickTC = new CellClickC(StartValues.CELL_CLICK);
            eMGame.IsSelectedCity = false;
            eMGame.HaveTreeUnit = false;
            eMGame.MistakeC.MistakeT = MistakeTypes.None;
            eMGame.WinnerC.Player = PlayerTypes.None;
            eMGame.ZoneInfoC = default;
            eMGame.CellsC = default;

            eMGame.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.STRENGTH_WIND, StartValues.MAX_STREANGTH_WIND, StartValues.MIN_SNREANGTH_WIND);
            eMGame.WeatherE.SunC = new SunC(StartValues.SUN_SIDE);
            eMGame.WeatherE.CloudC = new CloudC(StartValues.START_CLOUD);


            eMGame.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                ClearAllEnvironmentS.Clear(cell_0);
                UnitSystems.ClearUnitS.Clear(cell_0);

                eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMGame.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                eMGame.PlayerInfoE(playerT).ToggleScene(newSceneT);


                if (_eMCommon.GameModeTC.GameMode == GameModes.TrainingOff) eMGame.LessonTC.LessonT = (LessonTypes)1;


                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMGame.PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }
            }


            switch (_eMCommon.GameModeTC.GameMode)
            {
                case GameModes.TrainingOff:
                    eMGame.CurPlayerITC.Player = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    eMGame.CurPlayerITC.Player = eMGame.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    eMGame.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    eMGame.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = eMGame.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (eMGame.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                eMGame.MountainC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    eMGame.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                eMGame.AdultForestC(cell_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            eMGame.RiverEs(cell_0).SetStart(DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            eMGame.RiverEs(cell_0).SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            eMGame.RiverEs(cell_0).SetStart(DirectTypes.Up);
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (eMGame.RiverEs(cell_0).HaveRive(dir))
                            {
                                var idx_next = eMGame.CellEs(cell_0).AroundCellE(dir).IdxC.Idx;

                                eMGame.RiverEs(idx_next).RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = eMGame.CellEs(cell_0).AroundCellE(dir).IdxC.Idx;

                            eMGame.RiverEs(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    new MountainThrowHillsUpdMS(cell_0, eMGame);
                }

            }


            if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
            {
                eMGame.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = eMGame.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        eMGame.MountainC(cell_0).Resources = 0;

                        TakeAdultForestResourcesS.Take(1f, cell_0);

                        UnitSystems.SetNewUnitS.Set(UnitTypes.King, PlayerTypes.Second, cell_0);
                    }


                    if (x == 8 && y == 3)
                    {
                        eMGame.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        eMGame.MountainC(cell_0).Resources = 0;

                        UnitSystems.SetNewUnitS.Set(UnitTypes.Pawn, PlayerTypes.Second, cell_0);
                        UnitSystems.SetExtraTWS.Set(UnityEngine.Random.Range(0f, 1f) >= 0.5f ? ToolWeaponTypes.Sword : ToolWeaponTypes.Shield, LevelTypes.Second, ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST, cell_0);
                    }
                }
            }



           MasterSystems.GetDataCellsS.Run();
        }

        public void Run()
        {
            if (_eMCommon.SceneTC.Scene != SceneTypes.Game) return;

            _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());
        }
    }
}