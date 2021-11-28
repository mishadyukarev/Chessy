using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<CellTypes, EcsEntity[]> _cells;
        readonly static HashSet<byte> _idxs;

        public static ref T CellC<T>(in byte idx) where T : struct, ICell => ref _cells[CellTypes.Cell][idx].Get<T>();
        public static ref T UnitCellC<T>(in byte idx) where T : struct, IUnitCell => ref _cells[CellTypes.Unit][idx].Get<T>();
        public static ref T TWCellC<T>(in byte idx) where T : struct, ITWCellE => ref _cells[CellTypes.ToolWeapon][idx].Get<T>();
        public static ref T BuildCellC<T>(in byte idx) where T : struct, IBuildCell => ref _cells[CellTypes.Build][idx].Get<T>();
        public static ref T EnvCellC<T>(in byte idx) where T : struct, IEnvCell => ref _cells[CellTypes.Env][idx].Get<T>();
        public static ref T TrailCellC<T>(in byte idx) where T : struct, ITrailCell => ref _cells[CellTypes.Trail][idx].Get<T>();
        public static ref T FireCellC<T>(in byte idx) where T : struct, IFireCell => ref _cells[CellTypes.Fire][idx].Get<T>();
        public static ref T CloudCellC<T>(in byte idx) where T : struct, ICloudCell => ref _cells[CellTypes.Cloud][idx].Get<T>();
        public static ref T RiverCellC<T>(in byte idx) where T : struct, IRiverCell => ref _cells[CellTypes.River][idx].Get<T>();

        public static byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells[CellTypes.Cell].Length; idx++)
            {
                if (CellC<XyC>(idx).Xy.Compare(xy))
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
                _cells.Add(type, new EcsEntity[CellValuesC.AMOUNT_ALL_CELLS]);
            }

            _idxs = new HashSet<byte>();

            for (byte idx = 0; idx < CellValuesC.AMOUNT_ALL_CELLS; idx++)
            {
                _idxs.Add(idx);
            }
        }
        public EntityPool(in EcsWorld curGameW, in bool[] isActiveCells, in int[] idCells)
        {
            byte idx = 0;

            for (idx = 0; idx < CellValuesC.AMOUNT_ALL_CELLS; idx++)
            {
                _cells[CellTypes.Unit][idx] = curGameW.NewEntity()
                    .Replace(new UnitC(UnitTypes.None, idx))
                    .Replace(new LevelC())
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true))

                    .Replace(new ConditionC())
                    .Replace(new MoveInCondC(true))
                    .Replace(new UnitEffectsC(true))
                    .Replace(new StunC())

                    .Replace(new HpC())
                    .Replace(new DamageC())
                    .Replace(new StepC())
                    .Replace(new WaterC())

                    .Replace(new UniqAbilC(true))
                    .Replace(new CooldownUniqC(true))



                    .Replace(new CornerArcherC());


                _cells[CellTypes.ToolWeapon][idx] = curGameW.NewEntity()
                    .Replace(new ToolWeaponC())
                    .Replace(new ShieldC())
                    .Replace(new LevelC());


                _cells[CellTypes.Build][idx] = curGameW.NewEntity()
                    .Replace(new BuildC(BuildTypes.None, idx))
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true));


                _cells[CellTypes.Env][idx] = curGameW.NewEntity()
                    .Replace(new EnvC(new Dictionary<EnvTypes, bool>(), idx))
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

            for (byte x = 0; x < CellValuesC.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValuesC.CELL_COUNT_Y; y++)
                {
                    _cells[CellTypes.Cell][idx] = curGameW.NewEntity()
                        .Replace(new XyC(new byte[] { x, y }))
                        .Replace(new CellC(isActiveCells[idx], idCells[idx]));

                    ++idx;
                }
        }
    }
}