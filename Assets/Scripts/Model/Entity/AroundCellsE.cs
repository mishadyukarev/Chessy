﻿using System;
using System.Collections.Generic;

namespace Chessy.Model.Model.Entity
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
        public byte[] XyCell(in DirectTypes dirT) => AroundCellE(dirT).XyC.Xy();

        public DirectTypes Direct(in byte cell_0)
        {
            if (_aroundDirs_0.ContainsKey(cell_0)) return _aroundDirs_0[cell_0];
            else if (_aroundDirs_1.ContainsKey(cell_0)) return _aroundDirs_1[cell_0];

            return DirectTypes.None;
        }

        internal AroundCellsE(in byte idxCell, in DataFromViewC dataFromViewC, in EntitiesModel eMGame, params byte[] xyCell)
        {
            _aroundDirs_0 = new Dictionary<byte, DirectTypes>();
            _aroundDirs_1 = new Dictionary<byte, DirectTypes>();

            _aroundEs = new CellAroundE[(byte)DirectTypes.End];

            if (!dataFromViewC.IsBorder(idxCell))
            {
                for (var dir = (DirectTypes)0; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[EntitiesModel.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            xyDirect[EntitiesModel.X] = 0;
                            xyDirect[EntitiesModel.Y] = 0;
                            break;

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

                        case DirectTypes.LeftUp:
                            xyDirect[EntitiesModel.X] = -1;
                            xyDirect[EntitiesModel.Y] = 1;
                            break;

                        case DirectTypes.RightDown:
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


                    var x = (byte)(xyCell[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                    var y = (byte)(xyCell[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

                    var xy_0 = new[] { x, y };

                    var idxCellStart = eMGame.GetIdxCellByXy(xy_0);

                    _aroundDirs_0.Add(idxCellStart, dir);
                    _aroundEs[(byte)dir] = new CellAroundE(idxCellStart, xy_0);


                    if (!dataFromViewC.IsBorder(idxCellStart))
                    {
                        x = (byte)(xy_0[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                        y = (byte)(xy_0[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

                        var xy_1 = new[] { x, y };

                        _aroundDirs_1.Add(eMGame.GetIdxCellByXy(xy_1), dir);
                    }
                }
            }
        }
    }
}