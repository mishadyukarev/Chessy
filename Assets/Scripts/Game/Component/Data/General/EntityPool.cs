﻿using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<CellTypes, EcsEntity[]> _cells;
        readonly static HashSet<byte> _idxs;

        public static ref T Cell<T>(in byte idx) where T : struct, ICell => ref _cells[CellTypes.Cell][idx].Get<T>();
        public static ref T Unit<T>(in byte idx) where T : struct, IUnitCell => ref _cells[CellTypes.Unit][idx].Get<T>();
        public static ref T UnitTW<T>(in byte idx) where T : struct, ITWCellE => ref _cells[CellTypes.Unit_ToolWeapon][idx].Get<T>();

        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _cells[CellTypes.Build][idx].Get<T>();
        public static ref T Environment<T>(in byte idx) where T : struct, IEnvCell => ref _cells[CellTypes.Env][idx].Get<T>();
        public static ref T Trail<T>(in byte idx) where T : struct, ITrailCell => ref _cells[CellTypes.Trail][idx].Get<T>();
        public static ref T Fire<T>(in byte idx) where T : struct, IFireCell => ref _cells[CellTypes.Fire][idx].Get<T>();
        public static ref T Cloud<T>(in byte idx) where T : struct, ICloudCell => ref _cells[CellTypes.Cloud][idx].Get<T>();
        public static ref T River<T>(in byte idx) where T : struct, IRiverCell => ref _cells[CellTypes.River][idx].Get<T>();

        public static byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells[CellTypes.Cell].Length; idx++)
            {
                if (Cell<XyC>(idx).Xy.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }
        public static HashSet<byte> Idxs
        {
            get
            {
                var hash = new HashSet<byte>();
                foreach (var item in _idxs) hash.Add(item);
                return hash;
            }
        }



        static EntityPool()
        {
            _cells = new Dictionary<CellTypes, EcsEntity[]>();

            for (var type = CellTypes.First; type < CellTypes.End; type++)
            {
                _cells.Add(type, new EcsEntity[CellValues.AMOUNT_ALL_CELLS]);
            }

            _idxs = new HashSet<byte>();

            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                _idxs.Add(idx);
            }
        }
        public EntityPool(in EcsWorld curGameW, in bool[] isActiveCells, in int[] idCells)
        {
            byte idx = 0;

            for (idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                _cells[CellTypes.Unit][idx] = curGameW.NewEntity()
                    .Replace(new UnitCellWC(idx))
                    .Replace(new UnitC(UnitTypes.None))
                    .Replace(new LevelC())
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true))

                    .Replace(new EffectsC(true))
                    .Replace(new StunC())
                    .Replace(new ConditionC())
                    .Replace(new MoveInCondC(true))

                    .Replace(new UniqAbilC(true))
                    .Replace(new CooldownUniqC(true))
                    .Replace(new CornerArcherC())

                    .Replace(new DamageUnitC(idx))

                    .Replace(new HpUnitWC(idx))
                    .Replace(new HpC())

                    .Replace(new StepUnitWC(idx))
                    .Replace(new StepC())

                    .Replace(new WaterUnitC(idx))
                    .Replace(new WaterC());


                _cells[CellTypes.Unit_ToolWeapon][idx] = curGameW.NewEntity()
                    .Replace(new UnitTWCellC(idx))
                    .Replace(new ToolWeaponC())
                    .Replace(new LevelC())

                    .Replace(new ShieldC(idx))
                    .Replace(new ProtectionC());



                _cells[CellTypes.Build][idx] = curGameW.NewEntity()
                    .Replace(new BuildCellC(idx))
                    .Replace(new BuildC(BuildTypes.None))
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true));


                _cells[CellTypes.Env][idx] = curGameW.NewEntity()
                    .Replace(new EnvCellC(idx))
                    .Replace(new EnvC(new Dictionary<EnvTypes, bool>()))
                    .Replace(new EnvResC(true));


                _cells[CellTypes.Trail][idx] = curGameW.NewEntity()
                    .Replace(new TrailC(new Dictionary<DirectTypes, int>()))
                    .Replace(new VisibleC(true));


                _cells[CellTypes.Fire][idx] = curGameW.NewEntity()
                    .Replace(new FireC());


                _cells[CellTypes.Cloud][idx] = curGameW.NewEntity()
                    .Replace(new CloudC());


                _cells[CellTypes.River][idx] = curGameW.NewEntity()
                    .Replace(new RiverC(true));
            }


            idx = 0;

            for (byte x = 0; x < CellValues.CellCount(XyzTypes.X); x++)
                for (byte y = 0; y < CellValues.CellCount(XyzTypes.Y); y++)
                {
                    _cells[CellTypes.Cell][idx] = curGameW.NewEntity()
                        .Replace(new XyC(new byte[] { x, y }))
                        .Replace(new CellC(isActiveCells[idx], idCells[idx]));

                    ++idx;
                }


            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var envCell_0 = ref Environment<EnvCellC>(idx_0);
                    ref var envRes_0 = ref Environment<EnvResC>(idx_0);
                    ref var cloud_0 = ref Cloud<CloudC>(idx_0);

                    if (Cell<CellC>(idx_0).IsActiveCell)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvValuesC.StartPercent(EnvTypes.Mountain))
                            {
                                envCell_0.SetNew(EnvTypes.Mountain);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvValuesC.StartPercent(EnvTypes.AdultForest))
                                {
                                    envCell_0.SetNew(EnvTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvValuesC.StartPercent(EnvTypes.Hill))
                                {
                                    envCell_0.SetNew(EnvTypes.Hill);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvValuesC.StartPercent(EnvTypes.AdultForest))
                            {
                                envCell_0.SetNew(EnvTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvValuesC.StartPercent(EnvTypes.Fertilizer))
                                {
                                    envCell_0.SetNew(EnvTypes.Fertilizer);
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
                                var idx_1 = IdxCell(item.Value);
                                WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref River<RiverC>(idx_0);


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
                            var xy_next = CellSpaceC.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            River<RiverC>(idx_next).SetEnd(dir.Invert());
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceC.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            River<RiverC>(idx_next).SetCorner();
                        }
                    }
                }
            }

            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                foreach (byte idx_0 in Idxs)
                {
                    var curXyCell = Cell<XyC>(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var envCell_0 = ref Environment<EnvCellC>(idx_0);

                    ref var unit_0 = ref Unit<UnitC>(idx_0);
                    ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                    ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                    ref var hpUnitCell_0 = ref Unit<HpUnitWC>(idx_0);
                    ref var hp_0 = ref Unit<HpC>(idx_0);
                    ref var condUnit_0 = ref Unit<ConditionC>(idx_0);
                    ref var waterUnit_0 = ref Unit<WaterC>(idx_0);


                    ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                    ref var twLevel_0 = ref UnitTW<LevelC>(idx_0);
                    ref var protShiel_0 = ref UnitTW<ProtectionC>(idx_0);

                    ref var build_0 = ref Build<BuildC>(idx_0);
                    ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        envCell_0.Remove(EnvTypes.Mountain);
                        envCell_0.Remove(EnvTypes.AdultForest);

                        Unit<UnitCellWC>(idx_0).SetNew(UnitTypes.King, LevelTypes.First, PlayerTypes.Second);

                        hpUnitCell_0.SetMax();
                        Unit<WaterUnitC>(idx_0).SetMaxWater();
                        condUnit_0.Set(CondUnitTypes.Protected);
                    }

                    else if (x == 8 && y == 8)
                    {
                        envCell_0.Remove(EnvTypes.Mountain);
                        envCell_0.Remove(EnvTypes.AdultForest);


                        Build<BuildCellC>(idx_0).SetNew(BuildTypes.City, PlayerTypes.Second);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        envCell_0.Remove(EnvTypes.Mountain);

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            UnitTW<UnitTWCellC>(idx_0).SetNew(TWTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            UnitTW<UnitTWCellC>(idx_0).SetNew(TWTypes.Shield, LevelTypes.First);
                        }

                        Unit<UnitCellWC>(idx_0).SetNew(UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second);

                        hpUnitCell_0.SetMax();
                        condUnit_0.Set(CondUnitTypes.Protected);
                        Unit<WaterUnitC>(idx_0).SetMaxWater();
                    }
                }
            }

        }
    }
}