using ECS;
using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityCellPool
    {
        readonly static Dictionary<CellEntTypes, Entity[]> _cells;

        readonly static Dictionary<PlayerTypes, Entity[]> _unitEnts;
        readonly static Dictionary<UniqueAbilityTypes, Entity[]> _uniqueAbilEnts;
        readonly static Dictionary<UnitStatTypes, Entity[]> _unitStatEnts;

        readonly static Dictionary<PlayerTypes, Entity[]> _buildEnts;
        readonly static Dictionary<EnvTypes, Entity[]> _envEnts;
        readonly static Dictionary<DirectTypes, Entity[]> _trails;

        readonly static HashSet<byte> _idxs;


        public static ref C Cell<C>(in byte idx) where C : struct, ICell => ref _cells[CellEntTypes.Cell][idx].Get<C>();

        public static ref C Unit<C>(in byte idx) where C : struct, IUnitCellE => ref _cells[CellEntTypes.Unit][idx].Get<C>();
        public static ref C Unit<C>(in PlayerTypes player, in byte idx) where C : struct, IUnitPlayerCellE => ref _unitEnts[player][idx].Get<C>();
        public static ref C Unit<C>(in UniqueAbilityTypes uniq, in byte idx) where C : struct, IUnitUniqueCellE => ref _uniqueAbilEnts[uniq][idx].Get<C>();
        public static ref C Unit<C>(in UnitStatTypes stat, in byte idx) where C : struct, IUnitStatCellE => ref _unitStatEnts[stat][idx].Get<C>();
        public static ref T UnitTW<T>(in byte idx) where T : struct, ITWCellE => ref _cells[CellEntTypes.Unit_ToolWeapon][idx].Get<T>();
        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _cells[CellEntTypes.Build][idx].Get<T>();
        public static ref T Build<T>(in PlayerTypes player, in byte idx) where T : struct, IBuildPlayerCellE => ref _buildEnts[player][idx].Get<T>();
        public static ref T Environment<T>(in EnvTypes env, in byte idx) where T : struct, IEnvCell => ref _envEnts[env][idx].Get<T>();
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
        public static HashSet<UniqueAbilityTypes> Unique 
        {
            get
            {
                var hash = new HashSet<UniqueAbilityTypes>();
                foreach (var item in _uniqueAbilEnts) hash.Add(item.Key);
                return hash;
            }
        }
        public static HashSet<UnitStatTypes> Stats
        {
            get
            {
                var hash = new HashSet<UnitStatTypes>();
                foreach (var item in _unitStatEnts) hash.Add(item.Key);
                return hash;
            }
        }
        public static HashSet<EnvTypes> Enviroments
        {
            get
            {
                var hash = new HashSet<EnvTypes>();
                foreach (var item in _envEnts) hash.Add(item.Key);
                return hash;
            }
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

        static EntityCellPool()
        {
            _cells = new Dictionary<CellEntTypes, Entity[]>();
            _unitEnts = new Dictionary<PlayerTypes, Entity[]>();
            _uniqueAbilEnts = new Dictionary<UniqueAbilityTypes, Entity[]>();
            _unitStatEnts = new Dictionary<UnitStatTypes, Entity[]>();
            _buildEnts = new Dictionary<PlayerTypes, Entity[]>();
            _trails = new Dictionary<DirectTypes, Entity[]>();
            _envEnts = new Dictionary<EnvTypes, Entity[]>();
            _idxs = new HashSet<byte>();

            for (var type = CellEntTypes.First; type < CellEntTypes.End; type++)
            {
                _cells.Add(type, new Entity[CellValues.ALL_CELLS_AMOUNT]);   
            }

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _idxs.Add(idx);
            }

            for (var dir = DirectTypes.None; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _unitEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
                _buildEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (var uniqAbil = UniqueAbilityTypes.First; uniqAbil < UniqueAbilityTypes.End; uniqAbil++)
            {
                _uniqueAbilEnts.Add(uniqAbil, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
            {
                _unitStatEnts.Add(unitStat, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envEnts.Add(env, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public EntityCellPool(in WorldEcs gameW, in bool[] isActiveCells, in int[] idCells)
        {
            byte idx = 0;

            var envValues = new EnvironmentValues();

            for (idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _cells[CellEntTypes.Unit][idx] = gameW.NewEntity()
                    .Add(new UnitCellEC(idx))

                    .Add(new UnitC())
                    .Add(new LevelC())
                    .Add(new OwnerC())
                    .Add(new StunC())
                    .Add(new ConditionC())
                    .Add(new MoveInCondC(true))
                    .Add(new UniqAbilC(true))
                    .Add(new CornerArcherC())
                    .Add(new HpC())
                    .Add(new StepC())
                    .Add(new WaterC());


                _cells[CellEntTypes.Unit_ToolWeapon][idx] = gameW.NewEntity()
                    .Add(new UnitTWCellEC(idx))

                    .Add(new ToolWeaponC())
                    .Add(new LevelC())
                    .Add(new ShieldC(idx))
                    .Add(new ProtectionC());



                _cells[CellEntTypes.Build][idx] = gameW.NewEntity()
                    .Add(new BuildCellEC(idx))
                    .Add(new BuildC())
                    .Add(new OwnerC());


                _cells[CellEntTypes.Fire][idx] = gameW.NewEntity()
                    .Add(new HaveEffectC());


                _cells[CellEntTypes.Cloud][idx] = gameW.NewEntity()
                    .Add(new HaveEffectC());


                _cells[CellEntTypes.River][idx] = gameW.NewEntity()
                    .Add(new RiverC(true));


                for (var dir = DirectTypes.None; dir < DirectTypes.End; dir++)
                {
                    _trails[dir][idx] = gameW.NewEntity()
                    .Add(new TrailCellEC(idx))
                    .Add(new VisibleC(true))
                    .Add(new HpC());
                }

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _unitEnts[player][idx] = gameW.NewEntity()
                        .Add(new VisibledC());

                    _buildEnts[player][idx] = gameW.NewEntity()
                        .Add(new VisibledC());
                }

                for (var uniqAbil = UniqueAbilityTypes.First; uniqAbil < UniqueAbilityTypes.End; uniqAbil++)
                {
                    _uniqueAbilEnts[uniqAbil][idx] = gameW.NewEntity()
                        .Add(new CooldownC());
                }

                for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
                {
                    _unitStatEnts[unitStat][idx] = gameW.NewEntity()
                        .Add(new HaveEffectC());
                }

                for (var env = EnvTypes.First; env < EnvTypes.End; env++)
                {
                    _envEnts[env][idx] = gameW.NewEntity()
                        .Add(new EnvCellEC(idx, env))
                        .Add(new HaveEnvironmentC())
                        .Add(new ResourcesC());
                }
            }


            idx = 0;

            for (byte x = 0; x < CellValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellValues.Y_AMOUNT; y++)
                {
                    _cells[CellEntTypes.Cell][idx] = gameW.NewEntity()
                        .Add(new XyC(new byte[] { x, y }))
                        .Add(new CellC(isActiveCells[idx], idCells[idx]));

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

                    ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);

                    if (Cell<CellC>(idx_0).IsActiveCell)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercent(EnvTypes.Mountain))
                            {
                                Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).SetNew();
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                                {
                                    Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Hill))
                                {
                                    Environment<EnvCellEC>(EnvTypes.Hill, idx_0).SetNew();
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                            {
                                Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Fertilizer))
                                {
                                    Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).SetNew();
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

                    ref var unit_0 = ref Unit<UnitC>(idx_0);
                    ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                    ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

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
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second));

                        condUnit_0.Set(CondUnitTypes.Protected);
                    }

                    else if (x == 8 && y == 8)
                    {
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();


                        Build<BuildCellEC>(idx_0).SetNew(BuildTypes.City, PlayerTypes.Second);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            UnitTW<UnitTWCellEC>(idx_0).SetNew(TWTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            UnitTW<UnitTWCellEC>(idx_0).SetNew(TWTypes.Shield, LevelTypes.First);
                        }

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second));
                        condUnit_0.Set(CondUnitTypes.Protected);
                    }
                }
            }

        }
    }
    public interface IUnitCellE { }
    public interface IUnitPlayerCellE { }
    public interface IUnitUniqueCellE { }
    public interface IUnitStatCellE { }
    public interface IBuildPlayerCellE { }
}