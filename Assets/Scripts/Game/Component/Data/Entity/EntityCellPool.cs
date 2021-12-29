using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityCellPool
    {
        readonly static Dictionary<CellEntTypes, EcsEntity[]> _cells;
        readonly static Dictionary<DirectTypes, EcsEntity[]> _trails;

        readonly static HashSet<byte> _idxs;


        public static ref T Cell<T>(in byte idx) where T : struct, ICell => ref _cells[CellEntTypes.Cell][idx].Get<T>();
        public static ref T Unit<T>(in byte idx) where T : struct, IUnitCell => ref _cells[CellEntTypes.Unit][idx].Get<T>();
        public static ref T UnitTW<T>(in byte idx) where T : struct, ITWCellE => ref _cells[CellEntTypes.Unit_ToolWeapon][idx].Get<T>();
        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _cells[CellEntTypes.Build][idx].Get<T>();
        public static ref T Environment<T>(in byte idx) where T : struct, IEnvCell => ref _cells[CellEntTypes.Env][idx].Get<T>();
        public static ref T Fire<T>(in byte idx) where T : struct, IFireCell => ref _cells[CellEntTypes.Fire][idx].Get<T>();
        public static ref T Cloud<T>(in byte idx) where T : struct, ICloudCell => ref _cells[CellEntTypes.Cloud][idx].Get<T>();
        public static ref T River<T>(in byte idx) where T : struct, IRiverCell => ref _cells[CellEntTypes.River][idx].Get<T>();

        public static ref T Trail<T>(in byte idx, in DirectTypes dir = default) where T : struct, ITrailCell => ref _trails[dir][idx].Get<T>();



        public static byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells[CellEntTypes.Cell].Length; idx++)
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

        struct WorldEcs
        {
            public struct Entity
            {

            }

            readonly struct ComponentPool<T>
            {
                static readonly Dictionary<int, T> _components;

                static ComponentPool()
                {
                    _components = new Dictionary<int, T>();
                }

                public void Set(in int idx, in T component) => _components[idx] = component;
                public T Get(in int idx) => _components[idx];
            }

            public Entity NewEntity<T>(params T[] s) where T : struct
            {
                Entity ent;




                return ent;
            }
        }

        static EntityCellPool()
        {
            _cells = new Dictionary<CellEntTypes, EcsEntity[]>();
            _trails = new Dictionary<DirectTypes, EcsEntity[]>();
            _idxs = new HashSet<byte>();

            for (var type = CellEntTypes.First; type < CellEntTypes.End; type++)
            {
                _cells.Add(type, new EcsEntity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _idxs.Add(idx);
            }

            for (var dir = DirectTypes.None; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new EcsEntity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public EntityCellPool(in EcsWorld curGameW, in bool[] isActiveCells, in int[] idCells, in string nameBackground)
        {



            new WindC(DirectTypes.Right);


            BuildsUpgC.Start();
            UnitUpgC.StartGame();
            new UnitAvailPickUpgC(true);
            new BuildAvailPickUpgC(new Dictionary<string, bool>());
            new WaterAvailPickUpgC(new Dictionary<PlayerTypes, bool>());


            new SetUnitCellsC(true);
            new ArsonCellsC(true);
            new AttackCellsC(true);
            new CellsGiveTWC(true);

            new WhereEnvC(true);
            new WhereUnitsC(true);
            new WhereBuildsC(true);

            new InvUnitsC(true);
            new InvResC(true);
            new InvTWC(true);

            new BackgroundC(nameBackground);


            new WhoseMoveC(true);
            new ScoutHeroCooldownC(true);
            new CellClickC(default);


            new PlyerWinnerC(default);
            new ReadyC(new Dictionary<PlayerTypes, bool>());
            new MotionsC(0);
            new MistakeC(new Dictionary<ResTypes, int>());

            new HintC(new Dictionary<VideoClipTypes, bool>());
            new PickUpgC(new Dictionary<PlayerTypes, bool>());
            new GetterUnitsC(new Dictionary<UnitTypes, bool>());
            new EnvInfoC();
            new BuildAbilC(true);
            new FriendC(GameModesCom.IsGameMode(GameModes.WithFriendOff));


            byte idx = 0;

            var envValues = new EnvironmentValues();

            for (idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _cells[CellEntTypes.Unit][idx] = curGameW.NewEntity()
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

                    .Replace(new DamageUnitC(idx, new DamageUnitValues()))

                    .Replace(new HpUnitWC(idx))
                    .Replace(new HpC())

                    .Replace(new StepUnitWC(idx, new StepUnitValues()))
                    .Replace(new StepC())

                    .Replace(new WaterUnitC(idx))
                    .Replace(new WaterC());


                _cells[CellEntTypes.Unit_ToolWeapon][idx] = curGameW.NewEntity()
                    .Replace(new UnitTWCellC(idx))
                    .Replace(new ToolWeaponC())
                    .Replace(new LevelC())

                    .Replace(new ShieldC(idx))
                    .Replace(new ProtectionC());



                _cells[CellEntTypes.Build][idx] = curGameW.NewEntity()
                    .Replace(new BuildCellC(idx))
                    .Replace(new BuildC(BuildTypes.None))
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true));


                _cells[CellEntTypes.Env][idx] = curGameW.NewEntity()
                    .Replace(new EnvCellC(idx))
                    .Replace(new EnvC(new Dictionary<EnvTypes, bool>()))
                    .Replace(new EnvResC(envValues));


                _cells[CellEntTypes.Fire][idx] = curGameW.NewEntity()
                    .Replace(new HaveEffectC());


                _cells[CellEntTypes.Cloud][idx] = curGameW.NewEntity()
                    .Replace(new CloudC());


                _cells[CellEntTypes.River][idx] = curGameW.NewEntity()
                    .Replace(new RiverC(true));


                for (var dir = DirectTypes.None; dir < DirectTypes.End; dir++)
                {
                    _trails[dir][idx] = curGameW.NewEntity()
                    .Replace(new TrailC(idx))
                    .Replace(new VisibleC(true))
                    .Replace(new HpC());
                }
            }


            idx = 0;

            for (byte x = 0; x < CellValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellValues.Y_AMOUNT; y++)
                {
                    _cells[CellEntTypes.Cell][idx] = curGameW.NewEntity()
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
                            if (random <= envValues.StartPercent(EnvTypes.Mountain))
                            {
                                envCell_0.SetNew(EnvTypes.Mountain);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                                {
                                    envCell_0.SetNew(EnvTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Hill))
                                {
                                    envCell_0.SetNew(EnvTypes.Hill);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                            {
                                envCell_0.SetNew(EnvTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Fertilizer))
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
                InvResC.Set(ResTypes.Food, PlayerTypes.Second, 999999);

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