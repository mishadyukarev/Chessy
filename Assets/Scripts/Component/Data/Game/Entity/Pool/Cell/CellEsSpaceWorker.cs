using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEsSpaceWorker
    {
        readonly CellEs[] _cellEs;

        public CellEs CellEs(in byte idx) => _cellEs[idx];

        readonly HashSet<byte> _idxs;

        readonly Dictionary<DirectTypes, sbyte[]> _directs;

        const byte XY_FOR_ARRAY = 2;
        const byte X = 0;
        const byte Y = 1;

        public byte GetIdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cellEs.Length; idx++)
            {
                if (_cellEs[idx].CellE.XyC.Xy.Compare(xy))
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

        public CellEsSpaceWorker(in CellEs[] cellEs)
        {
            _cellEs = cellEs;

            _idxs = new HashSet<byte>();
            for (byte idx = 0; idx < cellEs.Length; idx++) _idxs.Add(idx);

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
            foreach (var item in GetXyAround(CellEs(idx_start).CellE.XyC.Xy)) list.Add(GetIdxCell(item));
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
            TryGetXyAround(CellEs(idx_start).CellE.XyC.Xy, out var dirs);

            directs = new Dictionary<DirectTypes, byte>();
            foreach (var item in dirs) directs.Add(item.Key, GetIdxCell(item.Value));
        }

        public bool TryGetDirect(byte[] xy_from, byte[] xy_to, out DirectTypes direct)
        {
            TryGetXyAround(xy_from, out var xy_around);

            foreach (var item_1 in xy_around)
            {
                if (item_1.Value.Compare(xy_to))
                {
                    direct = item_1.Key;
                    return true;
                }
            }

            direct = DirectTypes.None;
            return false;
        }
        public bool TryGetDirect(byte idx_from, byte idx_to, out DirectTypes dir) => TryGetDirect(CellEs(idx_from).CellE.XyC.Xy, CellEs(idx_to).CellE.XyC.Xy, out dir);
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
            return GetIdxCell(GetXyCellByDirect(CellEs(idx).CellE.XyC.Xy, dir));
        }
    }
}