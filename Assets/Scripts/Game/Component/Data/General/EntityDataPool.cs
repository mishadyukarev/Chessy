using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityDataPool
    {
        readonly static Dictionary<CellTypes, EcsEntity[]> _cells;

        public static int AmountAllCells => CellValuesC.AMOUNT_ALL_CELLS;

        public static ref T GetCellC<T>(byte idx) where T : struct, ICell => ref _cells[CellTypes.Cell][idx].Get<T>();
        public static ref T GetUnitCellC<T>(byte idx) where T : struct, IUnitCell => ref _cells[CellTypes.Unit][idx].Get<T>();
        public static ref T GetBuildCellC<T>(byte idx) where T : struct, IBuildCell => ref _cells[CellTypes.Build][idx].Get<T>();
        public static ref T GetEnvCellC<T>(byte idx) where T : struct, IEnvCell => ref _cells[CellTypes.Env][idx].Get<T>();
        public static ref T GetTrailCellC<T>(byte idx) where T : struct, ITrailCell => ref _cells[CellTypes.Trail][idx].Get<T>();
        public static ref T GetElseCellC<T>(byte idx) where T : struct, IElseCell
        {
            if (!_cells[CellTypes.Else][idx].Has<T>()) throw new Exception();
            return ref _cells[CellTypes.Else][idx].Get<T>();
        }

        public static byte GetIdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells[CellTypes.Cell].Length; idx++)
            {
                if (GetCellC<XyC>(idx).Xy.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }
        public static List<byte> Idxs
        {
            get
            {
                var list = new List<byte>();
                foreach (var ents in _cells.Values)
                {
                    for (byte idx = 0; idx < ents.Length; idx++)
                    {
                        list.Add(idx);
                    }
                }
                return list;
            }
        }



        static EntityDataPool()
        {
            _cells = new Dictionary<CellTypes, EcsEntity[]>();

            for (var type = CellTypes.First; type < CellTypes.End; type++)
            {
                _cells.Add(type, new EcsEntity[CellValuesC.AMOUNT_ALL_CELLS]);
            }
        }
        public EntityDataPool(in EcsWorld curGameW, in bool[] isActiveCells, in int[] idCells)
        {
            byte idx = 0;

            for (idx = 0; idx < CellValuesC.AMOUNT_ALL_CELLS; idx++)
            {
                _cells[CellTypes.Unit][idx] = curGameW.NewEntity()
                    .Replace(new UnitC(UnitTypes.None, idx))
                    .Replace(new LevelC())
                    .Replace(new OwnerC())
                    .Replace(new VisibleC(true))

                    .Replace(new ConditionUnitC())
                    .Replace(new MoveInCondC(true))
                    .Replace(new UnitEffectsC(true))
                    .Replace(new StunC())

                    .Replace(new HpC())
                    .Replace(new DamageC())
                    .Replace(new StepC())
                    .Replace(new WaterC())

                    .Replace(new UniqAbilC(true))
                    .Replace(new CooldownUniqC(true))

                    .Replace(new ToolWeaponC())

                    .Replace(new CornerArcherC());


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


                _cells[CellTypes.Else][idx] = curGameW.NewEntity()
                    .Replace(new FireC())
                    .Replace(new CloudC())
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