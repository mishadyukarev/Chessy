using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct CellEs
    {
        readonly CellPlayerPoolEs[] _forPlayerEs;

        readonly CellE[] _aroundEs;
        readonly Dictionary<byte, DirectTypes> _aroundDirs_0;
        readonly Dictionary<byte, DirectTypes> _aroundDirs_1;

        readonly HealthC[] _trailHealthCs;

        public bool IsActiveParentSelf;

        public readonly CellE CellE;
        public CellUnitEs UnitEs;
        public CellBuildingEs BuildEs;
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


        public ref HealthC TrailHealthC(in DirectTypes dir) => ref _trailHealthCs[(byte)dir - 1];
        public ref CellPlayerPoolEs Player(in PlayerTypes player) => ref _forPlayerEs[(byte)player];

        internal CellEs(in bool[] isActiveParents, in int idCell, byte[] xy, in byte idx, in EntitiesModel e) : this()
        {
            IsActiveParentSelf = isActiveParents[idx];

            if (IsActiveParentSelf)
            {
                _aroundEs = new CellE[(byte)DirectTypes.End - 1];
                _aroundDirs_0 = new Dictionary<byte, DirectTypes>();
                _aroundDirs_1 = new Dictionary<byte, DirectTypes>();

                for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[EntitiesModel.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            throw new Exception();

                        case DirectTypes.Right:
                            xyDirect[EntitiesModel.X] = 1;
                            xyDirect[EntitiesModel.Y] = 0;
                            break;

                        case DirectTypes.Left:
                            xyDirect[EntitiesModel.X] = -1;
                            xyDirect[EntitiesModel.Y] = 0;
                            break;

                        case DirectTypes.Up:
                            xyDirect[EntitiesModel.X] = 0;
                            xyDirect[EntitiesModel.Y] = 1;
                            break;

                        case DirectTypes.Down:
                            xyDirect[EntitiesModel.X] = 0;
                            xyDirect[EntitiesModel.Y] = -1;
                            break;

                        case DirectTypes.UpRight:
                            xyDirect[EntitiesModel.X] = 1;
                            xyDirect[EntitiesModel.Y] = 1;
                            break;

                        case DirectTypes.UpLeft:
                            xyDirect[EntitiesModel.X] = -1;
                            xyDirect[EntitiesModel.Y] = 1;
                            break;

                        case DirectTypes.DownRight:
                            xyDirect[EntitiesModel.X] = 1;
                            xyDirect[EntitiesModel.Y] = -1;
                            break;

                        case DirectTypes.DownLeft:
                            xyDirect[EntitiesModel.X] = -1;
                            xyDirect[EntitiesModel.Y] = -1;
                            break;

                        default:
                            throw new Exception();
                    }


                    var x = (byte)(xy[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                    var y = (byte)(xy[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

                    var xy_0 = new[] { x, y };

                    var idx_0 = e.GetIdxCellByXy(xy_0);

                    _aroundDirs_0.Add(idx_0, dir);
                    _aroundEs[(byte)dir - 1] = new CellE(idx_0, xy_0, default);


                    if (isActiveParents[idx_0])
                    {
                        x = (byte)(xy_0[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                        y = (byte)(xy_0[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

                        var xy_1 = new[] { x, y };

                        _aroundDirs_1.Add(e.GetIdxCellByXy(xy_1), dir);
                    }
                }

                //IdxsAround = new IdxsArrayC(idxsAround);
            }

            _forPlayerEs = new CellPlayerPoolEs[(byte)PlayerTypes.End];

            _trailHealthCs = new HealthC[(byte)DirectTypes.End - 1];

            CellE = new CellE(idx, xy, idCell);
            BuildEs = new CellBuildingEs((byte)PlayerTypes.End);
            UnitEs = new CellUnitEs(default);
            RiverEs = new CellRiverE(default);
        }
    }
}