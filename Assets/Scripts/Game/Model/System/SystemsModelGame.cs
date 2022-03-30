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
    public sealed class SystemsModelGame : IToggleScene, IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesModelGame _eMG;


        readonly List<IEcsRunSystem> _runs;

        internal readonly MistakeS MistakeS;
        internal readonly TakeAdultForestResourcesS TakeAdultForestResourcesS;


        internal readonly SetNewUnitOnCellS SetNewUnitS;
        internal readonly ShiftUnitS ShiftUnitS;
        internal readonly BuildS BuildS;
        internal readonly ClearAllEnvironmentS ClearAllEnvironmentS;
        internal readonly ClearUnitS ClearUnitS;
        internal readonly SetEffectsUnitS SetEffectsS;
        internal readonly SetMainUnitS SetMainS;
        internal readonly SetStatsUnitS SetStatsS;
        internal readonly SetMainToolWeaponUnitS SetMainTWS;
        internal readonly SetExtraToolWeaponS SetExtraTWS;
        internal readonly SetLastDiedS SetLastDiedS;
        internal readonly AttackShieldS AttackShieldS;
        internal readonly SetUnitS SetUnitS;
        internal readonly AttackUnitS AttackUnitS;
        internal readonly GetAttackMeleeCellsS GetAttackMeleeCellsS;
        internal readonly GetAbilityUnitS GetAbilityUnitS;
        internal readonly PawnGetExtractAdultForestS PawnGetExtractAdultForestS;
        internal readonly PawnExtractHillS PawnExtractHillS;
        internal readonly GetVisibleUnitS GetVisibleS;
        internal readonly GetCellForArsonArcherS GetCellForArsonArcherS;
        internal readonly GetCellsForAttackArcherS GetCellsForAttackArcherS;
        internal readonly GetCellsForShiftUnitS GetCellsForShiftUnitS;
        internal readonly GetEffectsForUnitsS GetEffectsForUnitsS;
        internal readonly GetDamageUnitsS GetDamageUnitsS;
        internal readonly KillUnitS KillUnitS;
        internal readonly GetBuildingVisibleS GetBuildingVisibleS;
        internal readonly GetWoodcutterExtractCellsS GetWoodcutterExtractCellsS;
        internal readonly AttackBuildingS DestroyBuildingS;
        internal readonly GetFarmExtractCellsS GetFarmExtractCellsS;
        internal readonly GetTrailsVisibleS GetTrailsVisibleS;
        internal readonly ClearBuildingS ClearBuildingS;

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
        internal readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        internal readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        internal readonly FirePawnS_M FirePawnS_M;
        internal readonly PutOutFirePawnS_M PutOutFirePawnS_M;
        internal readonly ChangeCornerArcherS_M ChangeCornerArcherS_M;
        internal readonly StunElfemaleS_M StunElfemaleS_M;
        internal readonly FireArcherS_M FireArcherS_M;
        internal readonly GrowAdultForestS_M GrowAdultForestS_M;
        internal readonly DestroyBuildingS_M DestroyBuildingS_M;
        internal readonly ChangeDirectionWindMS ChangeDirectionWindS_M;

        public readonly SystemsModelGameForUI ForUISystems;

        public SystemsModelGame(in EntitiesModelCommon eMCommon, in EntitiesModelGame eMGame)
        {
            _eMCommon = eMCommon;
            _eMG = eMGame;

            _runs = new List<IEcsRunSystem>()
            {
                new InputS(this, eMGame),
                new RayS(this, eMGame),
                new SelectorS(eMCommon, this, eMGame),
            };

            MistakeS = new MistakeS(this, eMGame);
            TakeAdultForestResourcesS = new TakeAdultForestResourcesS(this, eMGame);

            SetNewUnitS = new SetNewUnitOnCellS(this, eMGame);
            ShiftUnitS = new ShiftUnitS(this, eMGame);
            BuildS = new BuildS(this, eMGame);
            ClearAllEnvironmentS = new ClearAllEnvironmentS(this, eMGame);
            ClearUnitS = new ClearUnitS(this, eMGame);
            SetEffectsS = new SetEffectsUnitS(this, eMGame);
            SetMainS = new SetMainUnitS(this, eMGame);
            SetStatsS = new SetStatsUnitS(this, eMGame);
            SetMainTWS = new SetMainToolWeaponUnitS(this, eMGame);
            SetExtraTWS = new SetExtraToolWeaponS(this, eMGame);
            SetLastDiedS = new SetLastDiedS(this, eMGame);
            SetUnitS = new SetUnitS(this, eMGame);
            AttackShieldS = new AttackShieldS(this, eMGame);
            GetAttackMeleeCellsS = new GetAttackMeleeCellsS(this, eMGame);
            GetAbilityUnitS = new GetAbilityUnitS(this, eMGame);
            PawnGetExtractAdultForestS = new PawnGetExtractAdultForestS(this, eMGame);
            PawnExtractHillS = new PawnExtractHillS(this, eMGame);
            GetVisibleS = new GetVisibleUnitS(this, eMGame);
            GetBuildingVisibleS = new GetBuildingVisibleS(this, eMGame);
            GetCellForArsonArcherS = new GetCellForArsonArcherS(this, eMGame);
            GetCellsForAttackArcherS = new GetCellsForAttackArcherS(this, eMGame);
            GetCellsForShiftUnitS = new GetCellsForShiftUnitS(this, eMGame);
            GetEffectsForUnitsS = new GetEffectsForUnitsS(this, eMGame);
            GetDamageUnitsS = new GetDamageUnitsS(this, eMGame);
            GetTrailsVisibleS = new GetTrailsVisibleS(this, eMGame);
            GetWoodcutterExtractCellsS = new GetWoodcutterExtractCellsS(this, eMGame);
            GetFarmExtractCellsS = new GetFarmExtractCellsS(this, eMGame);
            KillUnitS = new KillUnitS(this, eMGame);
            AttackUnitS = new AttackUnitS(this, eMGame);
            ClearBuildingS = new ClearBuildingS(this, eMGame);

            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(this, eMGame);
            BuyS_M = new BuyS_M(this, eMGame);
            MeltS_M = new MeltS_M(this, eMGame);
            BuyBuildingS_M = new BuyBuildingS_M(this, eMGame);
            GetHeroS_M = new GetHeroS_M(this, eMGame);
            ReadyS_M = new ReadyS_M(this, eMGame);
            UpdateS_M = new UpdateS_M(this, eMGame);
            DonerS_M = new DonerS_M(this, eMGame);
            GetDataCellsS = new GetDataCellsS_M(this, eMGame);
            AttackUnit_M = new AttackUnit_M(this, eMGame);
            SetUnitS_M = new SetUnitS_M(this, eMGame);
            ShiftUnitS_M = new ShiftUnitS_M(this, eMGame);
            SeedPawnS_M = new SeedPawnS_M(this, eMGame);
            BuildFarmS_M = new BuildFarmS_M(this, eMGame);
            SetConditionUnitS_M = new SetConditionUnitS_M(this, eMGame);
            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(this, eMGame);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(this, eMGame);
            FirePawnS_M = new FirePawnS_M(this, eMGame);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(this, eMGame);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(this, eMGame);
            StunElfemaleS_M = new StunElfemaleS_M(this, eMGame);
            FireArcherS_M = new FireArcherS_M(this, eMGame);
            GrowAdultForestS_M = new GrowAdultForestS_M(this, eMGame);
            DestroyBuildingS = new AttackBuildingS(this, eMGame);
            DestroyBuildingS_M = new DestroyBuildingS_M(this, eMGame);
            ChangeDirectionWindS_M = new ChangeDirectionWindMS(this, eMGame);

            ForUISystems = new SystemsModelGameForUI(this, eMGame);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;


            _eMG.NeedUpdateView = true;

            _eMG.ZoneInfoC.IsActiveFriend = _eMCommon.GameModeTC.Is(GameModes.WithFriendOff);
            _eMG.WhoseMove.Player = StartValues.WHOSE_MOVE;
            _eMG.CellClickTC.Click = StartValues.CELL_CLICK;
            _eMG.IsSelectedCity = false;
            _eMG.HaveTreeUnit = false;
            _eMG.MistakeC.MistakeT = MistakeTypes.None;
            _eMG.WinnerC.Player = PlayerTypes.None;
            _eMG.ZoneInfoC = default;
            _eMG.CellsC = default;

            _eMG.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.STRENGTH_WIND, StartValues.MAX_STREANGTH_WIND, StartValues.MIN_SNREANGTH_WIND);
            _eMG.WeatherE.SunC = new SunC(StartValues.SUN_SIDE);
            _eMG.WeatherE.CloudC = new CloudC(StartValues.START_CLOUD);


            _eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);






            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                ClearAllEnvironmentS.Clear(cell_0);
                ClearUnitS.Clear(cell_0);

                _eMG.BuildingTC(cell_0).Building = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    _eMG.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).ToggleScene(newSceneT);


                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }

                if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
                {
                    if (playerT == PlayerTypes.First)
                    {
                        _eMG.ResourcesC(playerT, ResourceTypes.Food).Resources = 3;
                        _eMG.ResourcesC(playerT, ResourceTypes.Wood).Resources = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
                    }

                    _eMG.LessonTC.LessonT = (LessonTypes)1;
                }
            }


            switch (_eMCommon.GameModeTC.GameMode)
            {
                case GameModes.TrainingOff:
                    _eMG.CurPlayerITC.Player = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    _eMG.CurPlayerITC.Player = _eMG.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    _eMG.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    _eMG.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = _eMG.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    _eMG.HaveFire(cell_0) = false;


                    if (_eMG.CellEs(cell_0).IsActiveParentSelf)
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
                            _eMG.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            _eMG.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);

                            _eMG.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            _eMG.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                            _eMG.RiverEs(cell_0).HaveRive(DirectTypes.Right) = true;
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            _eMG.RiverEs(cell_0).RiverTC.River = RiverTypes.Start;
                            _eMG.RiverEs(cell_0).HaveRive(DirectTypes.Up) = true;
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (_eMG.RiverEs(cell_0).HaveRive(dir))
                            {
                                var idx_next = _eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                                _eMG.RiverEs(idx_next).RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                            _eMG.RiverEs(idx_next).RiverTC.River = RiverTypes.Corner;
                        }


                        if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _eMG.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
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
                }


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    if (_eMG.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (_eMG.MountainC(cell_0).HaveAnyResources)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                                if (UnityEngine.Random.Range(0f, 1f) <= 0.5)
                                {
                                    if (!_eMG.MountainC(_eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx).HaveAnyResources && !_eMG.BuildingTC(idx_1).HaveBuilding)
                                    {
                                        _eMG.HillC(idx_1).Resources = UnityEngine.Random.Range(0f, 1f);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
            {
                _eMG.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    var xy_0 = _eMG.CellEs(cell_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        _eMG.MountainC(cell_0).Resources = 0;

                        TakeAdultForestResourcesS.Take(1f, cell_0);

                        SetNewUnitS.Set(UnitTypes.King, PlayerTypes.Second, cell_0);
                    }


                    if (x == 8 && y == 3)
                    {
                        _eMG.AdultForestC(cell_0).Resources = 1;
                    }

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        _eMG.MountainC(cell_0).Resources = 0;

                        SetNewUnitS.Set(UnitTypes.Pawn, PlayerTypes.Second, cell_0);

                        var needSword = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
                        SetExtraTWS.Set(needSword ? ToolWeaponTypes.Sword : ToolWeaponTypes.Shield, needSword ? LevelTypes.Second : LevelTypes.First, ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST, cell_0);
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