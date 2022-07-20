using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.Entity
{
    public sealed class CellEs
    {
        readonly CellAroundE[] _aroundEs;
        public readonly CellE CellE;

        public readonly UnitE UnitE = new();
        public readonly BuildingE BuildingE = new();
        public readonly EnvironmentE EnvironmentE = new();
        public readonly EffectE EffectE = new();
        public readonly RiverE RiverE = new();
        public readonly TrailE TrailE = new();
        public readonly CloudOnCellE CloudE = new();

        public CellAroundE AroundCellE(in byte cellIdx) => _aroundEs[cellIdx];

        internal CellEs(in DataFromViewC dataFromViewC, in int idCell, in byte idxCell, in EntitiesModel e, params byte[] xyCell)
        {
            CellE = new CellE(dataFromViewC, idxCell, idCell, xyCell);


            _aroundEs = new CellAroundE[IndexCellsValues.CELLS];

            if (!dataFromViewC.IsBorder(idxCell))
            {
                for (var startDirectionT = (DirectTypes)0; startDirectionT < DirectTypes.End; startDirectionT++)
                {
                    var xyDirect = new short[IndexCellsValues.XY_FOR_ARRAY];

                    switch (startDirectionT)
                    {
                        case DirectTypes.None:
                            xyDirect[IndexCellsValues.X] = 0;
                            xyDirect[IndexCellsValues.Y] = 0;
                            break;

                        case DirectTypes.Right:
                            xyDirect[IndexCellsValues.X] = 1;
                            xyDirect[IndexCellsValues.Y] = 0;
                            break;

                        case DirectTypes.Left:
                            xyDirect[IndexCellsValues.X] = -1;
                            xyDirect[IndexCellsValues.Y] = 0;
                            break;

                        case DirectTypes.Up:
                            xyDirect[IndexCellsValues.X] = 0;
                            xyDirect[IndexCellsValues.Y] = 1;
                            break;

                        case DirectTypes.Down:
                            xyDirect[IndexCellsValues.X] = 0;
                            xyDirect[IndexCellsValues.Y] = -1;
                            break;

                        case DirectTypes.UpRight:
                            xyDirect[IndexCellsValues.X] = 1;
                            xyDirect[IndexCellsValues.Y] = 1;
                            break;

                        case DirectTypes.LeftUp:
                            xyDirect[IndexCellsValues.X] = -1;
                            xyDirect[IndexCellsValues.Y] = 1;
                            break;

                        case DirectTypes.RightDown:
                            xyDirect[IndexCellsValues.X] = 1;
                            xyDirect[IndexCellsValues.Y] = -1;
                            break;

                        case DirectTypes.DownLeft:
                            xyDirect[IndexCellsValues.X] = -1;
                            xyDirect[IndexCellsValues.Y] = -1;
                            break;

                        default:
                            throw new Exception();
                    }


                    var x = (byte)(xyCell[IndexCellsValues.X] + xyDirect[IndexCellsValues.X]);
                    var y = (byte)(xyCell[IndexCellsValues.Y] + xyDirect[IndexCellsValues.Y]);

                    var xy_0 = new[] { x, y };

                    var idxCellStart = e.GetIdxCellByXy(xy_0);

                    _aroundEs[idxCellStart] = new CellAroundE(idxCellStart, xy_0, startDirectionT, DistanceFromCellTypes.First);

                    //if (startDirectionT == DirectTypes.None)
                    //{
                    //    _aroundEs[idxCellStart] = new CellAroundE(idxCellStart, xy_0, startDirectionT, DistanceFromCellTypes.None);
                    //}
                    //else
                    //{
                        
                    //}


                    if (!dataFromViewC.IsBorder(idxCellStart))
                    {
                        x = (byte)(xy_0[IndexCellsValues.X] + xyDirect[IndexCellsValues.X]);
                        y = (byte)(xy_0[IndexCellsValues.Y] + xyDirect[IndexCellsValues.Y]);

                        var nextXy = new[] { x, y };

                        var nextIdxCell = e.GetIdxCellByXy(nextXy);

                        _aroundEs[nextIdxCell] = new CellAroundE(nextIdxCell, nextXy, startDirectionT, DistanceFromCellTypes.Second);
                    }
                }
            }
        }

        internal void Dispose()
        {
            EnvironmentE.Dispose();
            TrailE.Dispose();
            UnitE.Dispose();
            BuildingE.Dispose();
            CloudE.Dispose();
        }
    }
}