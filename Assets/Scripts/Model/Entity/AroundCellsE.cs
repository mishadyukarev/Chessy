using Chessy.Model.Component;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.Entity
{
    public readonly struct AroundCellsE
    {
        readonly CellAroundE[] _aroundEs;
        readonly DirectTypes[] _aroundDirs;

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



        public DirectTypes Direct(in byte cell_0) => _aroundDirs[cell_0];

        internal AroundCellsE(in byte idxCell, in DataFromViewC dataFromViewC, in EntitiesModel eMGame, params byte[] xyCell)
        {
            _aroundDirs = new DirectTypes[IndexCellsValues.CELLS];

            _aroundEs = new CellAroundE[(byte)DirectTypes.End];

            if (!dataFromViewC.IsBorder(idxCell))
            {
                for (var dirT = (DirectTypes)0; dirT < DirectTypes.End; dirT++)
                {
                    var xyDirect = new short[EntitiesModel.XY_FOR_ARRAY];

                    switch (dirT)
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

                    _aroundEs[(byte)dirT] = new CellAroundE(idxCellStart, xy_0);
                    _aroundDirs[idxCellStart] = dirT;


                    if (!dataFromViewC.IsBorder(idxCellStart))
                    {
                        x = (byte)(xy_0[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                        y = (byte)(xy_0[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

                        var xy_1 = new[] { x, y };

                        _aroundDirs[eMGame.GetIdxCellByXy(xy_1)] = dirT;
                    }
                }
            }
        }
    }
}