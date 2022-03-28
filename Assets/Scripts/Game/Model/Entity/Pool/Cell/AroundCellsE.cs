using Chessy.Game.Entity;
using Chessy.Game.Entity.Model;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity
{
    public sealed class AroundCellsE
    {
        readonly CellE[] _aroundEs = new CellE[(byte)DirectTypes.End - 1];
        readonly Dictionary<byte, DirectTypes> _aroundDirs_0 = new Dictionary<byte, DirectTypes>();
        readonly Dictionary<byte, DirectTypes> _aroundDirs_1 = new Dictionary<byte, DirectTypes>();

        public CellE AroundCellE(in byte idx_array) => _aroundEs[idx_array];
        public CellE AroundCellE(in DirectTypes dirT) => _aroundEs[(byte)dirT - 1];
        public CellE[] AroundCellEs => (CellE[])_aroundEs.Clone();
        public DirectTypes Direct(in byte idx_cell)
        {
            if (_aroundDirs_0.ContainsKey(idx_cell)) return _aroundDirs_0[idx_cell];
            else if (_aroundDirs_1.ContainsKey(idx_cell)) return _aroundDirs_1[idx_cell];

            return DirectTypes.None;

        }
        public IdxCellC[] AroundCellIdxsC
        {
            get
            {
                var idxsC = new IdxCellC[_aroundEs.Length];
                var i = 0;
                foreach (var item in _aroundEs) idxsC[i] = _aroundEs[i++].IdxC;
                return idxsC;
            }
        }
        public byte[] IdxsAround
        {
            get
            {
                var idxsC = new byte[_aroundEs.Length];
                var i = 0;
                foreach (var item in _aroundEs) idxsC[i] = _aroundEs[i++].IdxC.Idx;
                return idxsC;
            }
        }
        public HashSet<byte> IdxsAroundHashSet
        {
            get
            {
                var hashSet = new HashSet<byte>();
                foreach (var item in _aroundEs) hashSet.Add(item.IdxC.Idx);
                return hashSet;
            }
        }

        internal AroundCellsE(in byte idx, in bool[] isActiveParents, in byte[] xy, in EntitiesModelGame eMGame)
        {
            if (isActiveParents[idx])
            {
                for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[EntitiesModelGame.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            throw new Exception();

                        case DirectTypes.Right:
                            xyDirect[EntitiesModelGame.X] = 1;
                            xyDirect[EntitiesModelGame.Y] = 0;
                            break;

                        case DirectTypes.Left:
                            xyDirect[EntitiesModelGame.X] = -1;
                            xyDirect[EntitiesModelGame.Y] = 0;
                            break;

                        case DirectTypes.Up:
                            xyDirect[EntitiesModelGame.X] = 0;
                            xyDirect[EntitiesModelGame.Y] = 1;
                            break;

                        case DirectTypes.Down:
                            xyDirect[EntitiesModelGame.X] = 0;
                            xyDirect[EntitiesModelGame.Y] = -1;
                            break;

                        case DirectTypes.UpRight:
                            xyDirect[EntitiesModelGame.X] = 1;
                            xyDirect[EntitiesModelGame.Y] = 1;
                            break;

                        case DirectTypes.UpLeft:
                            xyDirect[EntitiesModelGame.X] = -1;
                            xyDirect[EntitiesModelGame.Y] = 1;
                            break;

                        case DirectTypes.DownRight:
                            xyDirect[EntitiesModelGame.X] = 1;
                            xyDirect[EntitiesModelGame.Y] = -1;
                            break;

                        case DirectTypes.DownLeft:
                            xyDirect[EntitiesModelGame.X] = -1;
                            xyDirect[EntitiesModelGame.Y] = -1;
                            break;

                        default:
                            throw new Exception();
                    }


                    var x = (byte)(xy[EntitiesModelGame.X] + xyDirect[EntitiesModelGame.X]);
                    var y = (byte)(xy[EntitiesModelGame.Y] + xyDirect[EntitiesModelGame.Y]);

                    var xy_0 = new[] { x, y };

                    var cell_0 = eMGame.GetIdxCellByXy(xy_0);

                    _aroundDirs_0.Add(cell_0, dir);
                    _aroundEs[(byte)dir - 1] = new CellE(cell_0, xy_0, default);


                    if (isActiveParents[cell_0])
                    {
                        x = (byte)(xy_0[EntitiesModelGame.X] + xyDirect[EntitiesModelGame.X]);
                        y = (byte)(xy_0[EntitiesModelGame.Y] + xyDirect[EntitiesModelGame.Y]);

                        var xy_1 = new[] { x, y };

                        _aroundDirs_1.Add(eMGame.GetIdxCellByXy(xy_1), dir);
                    }
                }
            }
        }
    }
}