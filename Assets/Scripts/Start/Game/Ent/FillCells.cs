using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class FillCells : IEcsInitSystem
    {
        private readonly EcsFilter<XyC> _xyF = default;
        private readonly EcsFilter<EnvC, EnvResC> _envF = default;

        private readonly EcsFilter<CellC> _cellVF = default;

        private readonly EcsFilter<BuildC, OwnerC> _buildF = default;
        private readonly EcsFilter<CloudC> _cloudF = default;
        private readonly EcsFilter<RiverC> _riverF = default;

        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitMainF = default;
        private readonly EcsFilter<HpC, DamageC, StepC, WaterUnitC> _statUnitF = default;
        private readonly EcsFilter<ToolWeaponC> _twUnitF = default;
        private readonly EcsFilter<ConditionUnitC> _effUnitF = default;

        public void Init()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in _xyF)
                {
                    var xy_0 = _xyF.Get1(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var env_0 = ref _envF.Get1(idx_0);
                    ref var envRes_0 = ref _envF.Get2(idx_0);
                    ref var cloud_0 = ref _cloudF.Get1(idx_0);

                    if (_cellVF.Get1(idx_0).IsActiveCell)
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
                            cloud_0.Have = true;
                            CloudCenterC.Idx = idx_0;

                            CellSpace.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = _xyF.GetIdxCell(item.Value);
                                WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref _riverF.Get1(idx_0);


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            river_0.SetStart(DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            river_0.SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            river_0.SetStart(DirectTypes.Up);
                        }


                        foreach (var dir in river_0.Directs)
                        {
                            var xy_next = CellSpace.GetXyCellByDirect(_xyF.Get1(idx_0).Xy, dir);
                            var idx_next = _xyF.GetIdxCell(xy_next);

                            _riverF.Get1(idx_next).SetEnd(dir.Invert());
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpace.GetXyCellByDirect(_xyF.Get1(idx_0).Xy, dir);
                            var idx_next = _xyF.GetIdxCell(xy_next);

                            _riverF.Get1(idx_next).SetCorner();
                        }
                    }
                }
            }


            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                foreach (byte idx_0 in _xyF)
                {
                    var curXyCell = _xyF.Get1(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var curEnvDatCom = ref _envF.Get1(idx_0);

                    ref var unit_0 = ref _unitMainF.Get1(idx_0);

                    ref var levUnit_0 = ref _unitMainF.Get2(idx_0);
                    ref var ownUnit_0 = ref _unitMainF.Get3(idx_0);

                    ref var hpUnitC_0 = ref _statUnitF.Get1(idx_0);

                    ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                    ref var twUnit_0 = ref _twUnitF.Get1(idx_0);
                    ref var thirUnitC_0 = ref _statUnitF.Get4(idx_0);

                    ref var build_0 = ref _buildF.Get1(idx_0);
                    ref var ownBuild_0 = ref _buildF.Get2(idx_0);

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



                        unit_0.Set(UnitTypes.King);
                        levUnit_0.SetLevel(LevelUnitTypes.First);
                        ownUnit_0.SetOwner(PlayerTypes.Second);
                        hpUnitC_0.SetMaxHp();
                        thirUnitC_0.SetMaxWater(UnitWaterUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit));
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
                        WhereBuildsC.Add(ownBuild_0.Owner, build_0.Build, idx_0);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        if (curEnvDatCom.Have(EnvTypes.Mountain))
                        {
                            curEnvDatCom.Remove(EnvTypes.Mountain);
                            WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                        }

                        unit_0.Set(UnitTypes.Pawn);
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
                        thirUnitC_0.SetMaxWater(UnitWaterUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit));

                        WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                    }
                }
            }
        }
    }
}