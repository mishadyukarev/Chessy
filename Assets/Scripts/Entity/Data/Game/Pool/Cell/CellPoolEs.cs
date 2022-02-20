using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellPoolEs
    {
        readonly CellPlayerPoolEs[] _forPlayerEs;

        readonly CellE[] _aroundEs;
        readonly Dictionary<byte, DirectTypes> _aroundDirs_0;
        readonly Dictionary<byte, DirectTypes> _aroundDirs_1;

        readonly HealthC[] _trailHealthCs;

        public bool IsActiveParentSelf;

        public readonly CellE CellE;
        public CellUnitPoolEs UnitEs;
        public CellBuildingPoolEs BuildE;
        public CellEnvironmentEs EnvironmentEs;
        public CellEffectE EffectEs;
        public CellRiverE RiverEs;


        public CellE AroundCellE(in byte idx_array) => _aroundEs[idx_array];
        public CellE AroundCellE(in DirectTypes dirT) => _aroundEs[(byte)dirT - 1];
        public CellE[] AroundCellEs => (CellE[])_aroundEs.Clone();
        public DirectTypes Direct(in byte idx_cell)
        {
            return _aroundDirs_0.ContainsKey(idx_cell) ? _aroundDirs_0[idx_cell] : _aroundDirs_1[idx_cell];
        }
        public IdxC[] AroundCellIdxsC
        {
            get
            {
                var idxsC = new IdxC[_aroundEs.Length];
                var i = 0;
                foreach (var item in _aroundEs) idxsC[i] = _aroundEs[i++].IdxC;
                return idxsC;
            }
        }
        public byte[] Idxs
        {
            get
            {
                var idxsC = new byte[_aroundEs.Length];
                var i = 0;
                foreach (var item in _aroundEs) idxsC[i] = _aroundEs[i++].IdxC.Idx;
                return idxsC;
            }
        }


        public ref HealthC TrailHealthC(in DirectTypes dir) => ref _trailHealthCs[(byte)dir - 1];
        public ref CellPlayerPoolEs Player(in PlayerTypes player) => ref _forPlayerEs[(byte)player];

        internal CellPoolEs(in bool[] isActiveParents, in int idCell, byte[] xy, in byte idx, in Entities e) : this()
        {
            IsActiveParentSelf = isActiveParents[idx];

            if (IsActiveParentSelf)
            {
                _aroundEs = new CellE[(byte)DirectTypes.End - 1];
                _aroundDirs_0 = new Dictionary<byte, DirectTypes>();
                _aroundDirs_1 = new Dictionary<byte, DirectTypes>();

                for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[Entities.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            throw new Exception();

                        case DirectTypes.Right:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = 0;
                            break;

                        case DirectTypes.Left:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = 0;
                            break;

                        case DirectTypes.Up:
                            xyDirect[Entities.X] = 0;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.Down:
                            xyDirect[Entities.X] = 0;
                            xyDirect[Entities.Y] = -1;
                            break;

                        case DirectTypes.UpRight:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.UpLeft:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.DownRight:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = -1;
                            break;

                        case DirectTypes.DownLeft:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = -1;
                            break;

                        default:
                            throw new Exception();
                    }


                    var x = (byte)(xy[Entities.X] + xyDirect[Entities.X]);
                    var y = (byte)(xy[Entities.Y] + xyDirect[Entities.Y]);

                    var xy_0 = new[] { x, y };

                    var idx_0 = e.GetIdxCellByXy(xy_0);

                    _aroundDirs_0.Add(idx_0, dir);
                    _aroundEs[(byte)dir - 1] = new CellE(idx_0, xy_0, default);


                    if (isActiveParents[idx_0])
                    {
                        x = (byte)(xy_0[Entities.X] + xyDirect[Entities.X]);
                        y = (byte)(xy_0[Entities.Y] + xyDirect[Entities.Y]);

                        var xy_1 = new[] { x, y };

                        _aroundDirs_1.Add(e.GetIdxCellByXy(xy_1), dir);
                    }
                }

                //IdxsAround = new IdxsArrayC(idxsAround);
            }

            _forPlayerEs = new CellPlayerPoolEs[(byte)PlayerTypes.End];

            _trailHealthCs = new HealthC[(byte)DirectTypes.End - 1];

            CellE = new CellE(idx, xy, idCell);
            BuildE = new CellBuildingPoolEs((byte)PlayerTypes.End);
            UnitEs = new CellUnitPoolEs(default);
            RiverEs = new CellRiverE(default);
        }
    }
}