using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class FillingCells : IEcsInitSystem
    {
        private readonly EcsFilter<XyC> _xyCellFilter = default;
        private readonly EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;

        private readonly EcsFilter<CellC> _cellViewFilt = default;

        private readonly EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;
        private readonly EcsFilter<CloudC> _cellWeatherFilt = default;
        private readonly EcsFilter<RiverC> _cellRiverFilt = default;

        private readonly EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private readonly EcsFilter<UnitC, HpC, DamageC, StepC> _cellUnitStatsFilt = default;
        private readonly EcsFilter<UnitC, ConditionUnitC, ToolWeaponC, WaterUnitC> _cellUnitOtherFilt = default;

        public void Init()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in _xyCellFilter)
                {
                    var xy_0 = _xyCellFilter.Get1(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
                    ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);
                    ref var curWeatherDatCom = ref _cellWeatherFilt.Get1(idx_0);

                    if (_cellViewFilt.Get1(idx_0).IsActiveCell)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_MOUNTAIN_PERCENT)
                            {
                                env_0.Set(EnvTypes.Mountain);
                                WhereEnvC.Add(EnvTypes.Mountain, idx_0);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FOREST_PERCENT)
                                {
                                    env_0.Set(EnvTypes.AdultForest);
                                    envRes_0.SetNew(EnvTypes.AdultForest);
                                    WhereEnvC.Add(EnvTypes.AdultForest, idx_0);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_HILL_PERCENT)
                                {
                                    env_0.Set(EnvTypes.Hill);
                                    envRes_0.SetNew(EnvTypes.Hill);
                                    WhereEnvC.Add(EnvTypes.Hill, idx_0);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_FOREST_PERCENT)
                            {
                                env_0.Set(EnvTypes.AdultForest);
                                envRes_0.SetNew(EnvTypes.AdultForest);
                                WhereEnvC.Add(EnvTypes.AdultForest, idx_0);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FERTILIZER_PERCENT)
                                {
                                    env_0.Set(EnvTypes.Fertilizer);
                                    envRes_0.SetNew(EnvTypes.Fertilizer);
                                    WhereEnvC.Add(EnvTypes.Fertilizer, idx_0);
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            curWeatherDatCom.Have = true;
                            curWeatherDatCom.CloudWidth = CloudWidthTypes.OneBlock;
                            WhereCloudsC.Add(idx_0);
                        }


                        var riverType = RiverTypes.None;
                        var dirTypes = new List<DirectTypes>();
                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                            dirTypes.Add(DirectTypes.Right);
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                        }

                        if (riverType != default)
                        {
                            foreach (var dirType in dirTypes)
                            {
                                _cellRiverFilt.Get1(idx_0).Type = riverType;
                                _cellRiverFilt.Get1(idx_0).AddDir(dirType);

                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(idx_0).Xy, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).Type = RiverTypes.End;

                                if (dirType == DirectTypes.Up)
                                {
                                    _cellRiverFilt.Get1(idx_next).AddDir(DirectTypes.Down);
                                }
                                else if (dirType == DirectTypes.Right)
                                {
                                    _cellRiverFilt.Get1(idx_next).AddDir(DirectTypes.Left);
                                }
                            }


                            foreach (var dirType in corners)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(idx_0).Xy, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).Type = RiverTypes.Corner;
                            }

                        }
                    }
                }
            }


            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                foreach (byte idx_0 in _xyCellFilter)
                {
                    var curXyCell = _xyCellFilter.Get1(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var curEnvDatCom = ref _cellEnvFilter.Get1(idx_0);

                    ref var unit_0 = ref _cellUnitStatsFilt.Get1(idx_0);

                    ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                    ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                    ref var hpUnitC_0 = ref _cellUnitStatsFilt.Get2(idx_0);

                    ref var condUnit_0 = ref _cellUnitOtherFilt.Get2(idx_0);
                    ref var twUnit_0 = ref _cellUnitOtherFilt.Get3(idx_0);
                    ref var thirUnitC_0 = ref _cellUnitOtherFilt.Get4(idx_0);

                    ref var build_0 = ref _cellBuildFilter.Get1(idx_0);
                    ref var ownBuild_0 = ref _cellBuildFilter.Get2(idx_0);

                    if (x == 7 && y == 8)
                    {
                        if (curEnvDatCom.Have(EnvTypes.Mountain))
                        {
                            curEnvDatCom.Remove(EnvTypes.Mountain);
                            WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                        }
                        if (curEnvDatCom.Have(EnvTypes.AdultForest))
                        {
                            curEnvDatCom.Remove(EnvTypes.AdultForest);
                            WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);
                        }



                        unit_0.SetUnit(UnitTypes.King);
                        levUnit_0.SetLevel(LevelUnitTypes.First);
                        ownUnit_0.SetOwner(PlayerTypes.Second);
                        hpUnitC_0.SetMaxHp();
                        thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                        condUnit_0.Set(CondUnitTypes.Protected);
                        WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                    }

                    else if (x == 8 && y == 8)
                    {
                        if (curEnvDatCom.Have(EnvTypes.Mountain))
                        {
                            curEnvDatCom.Remove(EnvTypes.Mountain);
                            WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                        }
                        if (curEnvDatCom.Have(EnvTypes.AdultForest))
                        {
                            curEnvDatCom.Remove(EnvTypes.AdultForest);
                            WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);
                        }

                        build_0.SetNew(BuildTypes.City);
                        ownBuild_0.SetOwner(PlayerTypes.Second);
                        WhereBuildsC.Add(ownBuild_0.Owner, build_0.Type, idx_0);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        if (curEnvDatCom.Have(EnvTypes.Mountain))
                        {
                            curEnvDatCom.Remove(EnvTypes.Mountain);
                            WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                        }

                        unit_0.SetUnit(UnitTypes.Pawn);
                        levUnit_0.SetLevel(LevelUnitTypes.First);


                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            twUnit_0.ToolWeapType = ToolWeaponTypes.Sword;
                            twUnit_0.LevelTWType = LevelTWTypes.Iron;
                        }
                        else
                        {
                            twUnit_0.ToolWeapType = ToolWeaponTypes.Shield;
                            twUnit_0.LevelTWType = LevelTWTypes.Wood;
                            twUnit_0.AddShieldProtect(LevelTWTypes.Wood);
                        }
                        hpUnitC_0.SetMaxHp();
                        condUnit_0.Set(CondUnitTypes.Protected);
                        ownUnit_0.SetOwner(PlayerTypes.Second);
                        thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));

                        WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                    }
                }
            }
        }
    }
}