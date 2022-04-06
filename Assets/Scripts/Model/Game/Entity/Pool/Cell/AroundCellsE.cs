using System;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity
{
    public readonly struct AroundCellsE
    {
        readonly CellAroundE[] _aroundEs;

        readonly Dictionary<byte, DirectTypes> _aroundDirs_0;
        readonly Dictionary<byte, DirectTypes> _aroundDirs_1;

        public CellAroundE AroundCellE(in DirectTypes dirT) => _aroundEs[(byte)dirT];
        public byte[] CellsAround
        {
            get
            {
                var cells = new byte[_aroundEs.Length];
                for (var i = 1; i < _aroundEs.Length; i++)
                {
                    cells[i] = _aroundEs[i].IdxC.Idx;
                }
                return cells;
            }
        }
        public byte IdxCell(in DirectTypes dirT) => AroundCellE(dirT).IdxC.Idx;
        public byte[] XyCell(in DirectTypes dirT) => AroundCellE(dirT).XyC.Xy;

        public DirectTypes Direct(in byte cell_0)
        {
            if (_aroundDirs_0.ContainsKey(cell_0)) return _aroundDirs_0[cell_0];
            else if (_aroundDirs_1.ContainsKey(cell_0)) return _aroundDirs_1[cell_0];

            return DirectTypes.None;
        }

        internal AroundCellsE(in byte idx, in bool[] isActiveParents, in EntitiesModelGame eMGame, params byte[] xy)
        {
            _aroundDirs_0 = new Dictionary<byte, DirectTypes>();
            _aroundDirs_1 = new Dictionary<byte, DirectTypes>();

            _aroundEs = new CellAroundE[(byte)DirectTypes.End];

            if (isActiveParents[idx])
            {
                for (var dir = (DirectTypes)0; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[EntitiesModelGame.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            xyDirect[EntitiesModelGame.X] = 0;
                            xyDirect[EntitiesModelGame.Y] = 0;
                            break;

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
                    _aroundEs[(byte)dir] = new CellAroundE(cell_0, xy_0);


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