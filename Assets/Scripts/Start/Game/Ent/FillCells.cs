using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class FillCells : IEcsInitSystem
    {
        private readonly EcsFilter<EnvC, EnvResC> _envF = default;

        private readonly EcsFilter<CloudC> _cloudF = default;
        private readonly EcsFilter<RiverC> _riverF = default;

        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC, DamageC, StepC, WaterC> _statUnitF = default;
        private readonly EcsFilter<ToolWeaponC> _twUnitF = default;
        private readonly EcsFilter<ConditionUnitC> _effUnitF = default;

        public void Init()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int random;


                foreach (byte idx_0 in EntityPool.Idxs)
                {
                    var xy_0 = EntityPool.CellC<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var env_0 = ref _envF.Get1(idx_0);
                    ref var envRes_0 = ref _envF.Get2(idx_0);
                    ref var cloud_0 = ref _cloudF.Get1(idx_0);

                    if (EntityPool.CellC<CellC>(idx_0).IsActiveCell)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_MOUNTAIN_PERCENT)
                            {
                                env_0.SetNew(EnvTypes.Mountain);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FOREST_PERCENT)
                                {
                                    env_0.SetNew(EnvTypes.AdultForest);
                                    envRes_0.SetNew(EnvTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_HILL_PERCENT)
                                {
                                    env_0.SetNew(EnvTypes.Hill);
                                    envRes_0.SetNew(EnvTypes.Hill);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_FOREST_PERCENT)
                            {
                                env_0.SetNew(EnvTypes.AdultForest);
                                envRes_0.SetNew(EnvTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FERTILIZER_PERCENT)
                                {
                                    env_0.SetNew(EnvTypes.Fertilizer);
                                    envRes_0.SetNew(EnvTypes.Fertilizer);
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            cloud_0.Have = true;
                            CloudCenterC.Idx = idx_0;

                            CellSpaceC.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = EntityPool.IdxCell(item.Value);
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
                            var xy_next = CellSpaceC.GetXyCellByDirect(EntityPool.CellC<XyC>(idx_0).Xy, dir);
                            var idx_next = EntityPool.IdxCell(xy_next);

                            _riverF.Get1(idx_next).SetEnd(dir.Invert());
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceC.GetXyCellByDirect(EntityPool.CellC<XyC>(idx_0).Xy, dir);
                            var idx_next = EntityPool.IdxCell(xy_next);

                            _riverF.Get1(idx_next).SetCorner();
                        }
                    }
                }
            }


            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                foreach (byte idx_0 in EntityPool.Idxs)
                {
                    var curXyCell = EntityPool.CellC<XyC>(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var env_0 = ref _envF.Get1(idx_0);

                    ref var unit_0 = ref EntityPool.UnitCellC<UnitC>(idx_0);

                    ref var levUnit_0 = ref _unitF.Get2(idx_0);
                    ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                    ref var hpUnitC_0 = ref _statUnitF.Get1(idx_0);

                    ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                    ref var twUnit_0 = ref _twUnitF.Get1(idx_0);
                    ref var thirUnitC_0 = ref _statUnitF.Get4(idx_0);

                    ref var build_0 = ref EntityPool.BuildCellC<BuildC>(idx_0);
                    ref var ownBuild_0 = ref EntityPool.BuildCellC<OwnerC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        if (env_0.Have(EnvTypes.Mountain))
                        {
                            env_0.Remove(EnvTypes.Mountain);
                        }
                        if (env_0.Have(EnvTypes.AdultForest))
                        {
                            env_0.Remove(EnvTypes.AdultForest);
                        }



                        levUnit_0.SetLevel(LevelTypes.First);
                        ownUnit_0.SetOwner(PlayerTypes.Second);
                        unit_0.SetNew(UnitTypes.King, levUnit_0.Level, ownUnit_0.Owner);

                        hpUnitC_0.SetMaxHp();
                        thirUnitC_0.SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));
                        condUnit_0.Set(CondUnitTypes.Protected);
                    }

                    else if (x == 8 && y == 8)
                    {
                        if (env_0.Have(EnvTypes.Mountain))
                        {
                            env_0.Remove(EnvTypes.Mountain);
                        }
                        if (env_0.Have(EnvTypes.AdultForest))
                        {
                            env_0.Remove(EnvTypes.AdultForest);
                        }


                        ownBuild_0.SetOwner(PlayerTypes.Second);
                        build_0.SetNew(BuildTypes.City, ownBuild_0.Owner);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        if (env_0.Have(EnvTypes.Mountain))
                        {
                            env_0.Remove(EnvTypes.Mountain);
                        }

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            twUnit_0.TW = TWTypes.Sword;
                            twUnit_0.Level = LevelTypes.Second;
                        }
                        else
                        {
                            twUnit_0.TW = TWTypes.Shield;
                            twUnit_0.Level = LevelTypes.First;
                            twUnit_0.SetShieldProtect(LevelTypes.First);
                        }

                        levUnit_0.SetLevel(LevelTypes.First);
                        ownUnit_0.SetOwner(PlayerTypes.Second);
                        unit_0.SetNew(UnitTypes.Pawn, levUnit_0.Level, ownUnit_0.Owner);

                        hpUnitC_0.SetMaxHp();
                        condUnit_0.Set(CondUnitTypes.Protected);
                        thirUnitC_0.SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));
                    }
                }
            }
        }
    }
}