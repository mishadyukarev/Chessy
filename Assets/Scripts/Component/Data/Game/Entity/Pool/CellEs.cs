using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEs
    {
        readonly CellParenE[] _parentEs;
        readonly CellE[] _cells;
        readonly HashSet<byte> _idxs;

        readonly Dictionary<DirectTypes, sbyte[]> _directs;

        const byte XY_FOR_ARRAY = 2;
        const byte X = 0;
        const byte Y = 1;


        public CellParenE ParentE(in byte idx) => _parentEs[idx];
        public CellE CellE(in byte idx) => _cells[idx];

        public byte GetIdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                if (CellE(idx).XyC.Xy.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }
        public HashSet<byte> Idxs
        {
            get
            {
                var hash = new HashSet<byte>();
                foreach (var item in _idxs) hash.Add(item);
                return hash;
            }
        }
        public byte Count => (byte)_idxs.Count;


        #region Pools

        public readonly CellUnitEs UnitEs;
        public readonly CellBuildEs BuildEs;
        public readonly CellEnvironmentEs EnvironmentEs;
        public readonly CellTrailEs TrailEs;
        public readonly CellFireEs FireEs;
        public readonly CellRiverEs RiverEs;

        #endregion


        public CellEs(in EcsWorld gameW, in bool[] isActiveParentCells, in int[] idCells)
        {
            _parentEs = new CellParenE[isActiveParentCells.Length];
            _cells = new CellE[isActiveParentCells.Length];

            _idxs = new HashSet<byte>();

            byte idx = 0;
            for (idx = 0; idx < _parentEs.Length; idx++) _idxs.Add(idx);
            idx = 0;

            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
                {
                    _parentEs[idx] = new CellParenE(gameW, isActiveParentCells[idx]);
                    _cells[idx] = new CellE(gameW, new byte[] { x, y }, idCells[idx]);

                    ++idx;
                }

            BuildEs = new CellBuildEs(gameW);
            TrailEs = new CellTrailEs(gameW);
            UnitEs = new CellUnitEs(gameW);
            EnvironmentEs = new CellEnvironmentEs(gameW);
            FireEs = new CellFireEs(gameW);
            RiverEs = new CellRiverEs(gameW);



            _directs = new Dictionary<DirectTypes, sbyte[]>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                var xyDirect = new sbyte[XY_FOR_ARRAY];

                switch (dir)
                {
                    case DirectTypes.None:
                        throw new Exception();

                    case DirectTypes.Right:
                        xyDirect[X] = 1;
                        xyDirect[Y] = 0;
                        break;

                    case DirectTypes.Left:
                        xyDirect[X] = -1;
                        xyDirect[Y] = 0;
                        break;

                    case DirectTypes.Up:
                        xyDirect[X] = 0;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.Down:
                        xyDirect[X] = 0;
                        xyDirect[Y] = -1;
                        break;

                    case DirectTypes.UpRight:
                        xyDirect[X] = 1;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.UpLeft:
                        xyDirect[X] = -1;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.DownRight:
                        xyDirect[X] = 1;
                        xyDirect[Y] = -1;
                        break;

                    case DirectTypes.DownLeft:
                        xyDirect[X] = -1;
                        xyDirect[Y] = -1;
                        break;

                    default:
                        throw new Exception();
                }

                _directs.Add(dir, xyDirect);
            }
        }


        public List<byte[]> GetXyAround(in byte[] xy_start)
        {
            var xyAvail = new List<byte[]>();
            var xyResult = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                var xyDirectCell = _directs[dir];

                xyResult[X] = (byte)(xy_start[X] + xyDirectCell[X]);
                xyResult[Y] = (byte)(xy_start[Y] + xyDirectCell[Y]);

                xyAvail.Add((byte[])xyResult.Clone());
            }

            return xyAvail;
        }
        public List<byte> GetIdxsAround(in byte idx_start)
        {
            var list = new List<byte>();
            foreach (var item in GetXyAround(CellE(idx_start).XyC.Xy)) list.Add(GetIdxCell(item));
            return list;
        }


        public void TryGetXyAround(byte[] xy_start, out Dictionary<DirectTypes, byte[]> directs)
        {
            directs = new Dictionary<DirectTypes, byte[]>();
            var xyResult = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                var xyDirect = _directs[dir];

                xyResult[X] = (byte)(xy_start[X] + xyDirect[X]);
                xyResult[Y] = (byte)(xy_start[Y] + xyDirect[Y]);

                directs.Add(dir, (byte[])xyResult.Clone());
            }
        }
        public void TryGetIdxAround(in byte idx_start, out Dictionary<DirectTypes, byte> directs)
        {
            TryGetXyAround(CellE(idx_start).XyC.Xy, out var dirs);

            directs = new Dictionary<DirectTypes, byte>();
            foreach (var item in dirs) directs.Add(item.Key, GetIdxCell(item.Value));
        }

        public DirectTypes GetDirect(byte[] xy_from, byte[] xy_to)
        {
            TryGetXyAround(xy_from, out var xy_around);

            foreach (var item_1 in xy_around)
            {
                if (item_1.Value.Compare(xy_to)) return item_1.Key;
            }

            return DirectTypes.None;
        }
        public DirectTypes GetDirect(byte idx_from, byte idx_to) => GetDirect(CellE(idx_from).XyC.Xy, CellE(idx_to).XyC.Xy);

        public byte[] GetXyCellByDirect(byte[] xyStart, DirectTypes directType)
        {
            var xyResult = new byte[XY_FOR_ARRAY];

            var xyDirect = _directs[directType];

            xyResult[X] = (byte)(xyStart[X] + xyDirect[X]);
            xyResult[Y] = (byte)(xyStart[Y] + xyDirect[Y]);

            return xyResult;
        }
        public byte GetIdxCellByDirect(in byte idx, in DirectTypes dir)
        {
            return GetIdxCell(GetXyCellByDirect(CellE(idx).XyC.Xy, dir));
        }
    }
}