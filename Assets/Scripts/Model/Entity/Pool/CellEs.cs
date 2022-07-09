using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.Entity
{
    public struct CellEs
    {
        readonly CellAroundE[] _aroundEs;
        public readonly CellE CellE;
        public UnitE UnitE;
        public BuildingE BuildingE;
        public EnvironmentE EnvironmentE;
        public EffectE EffectE;
        public RiverE RiverE;
        public TrailE TrailE;
        public CloudOnCellE CloudE;

        public ref CellAroundE AroundCellE(in byte cellIdx) => ref _aroundEs[cellIdx];

        internal CellEs(in DataFromViewC dataFromViewC, in int idCell, in byte idxCell, in EntitiesModel e, params byte[] xyCell) : this()
        {
            CellE = new CellE(dataFromViewC, idxCell, idCell, xyCell);
            UnitE = new UnitE(default);
            BuildingE = new BuildingE(default);
            RiverE = new RiverE(new bool[(byte)DirectTypes.End]);
            TrailE = new TrailE(default);


            _aroundEs = new CellAroundE[IndexCellsValues.CELLS];

            if (!dataFromViewC.IsBorder(idxCell))
            {
                for (var startDirectionT = (DirectTypes)0; startDirectionT < DirectTypes.End; startDirectionT++)
                {
                    var xyDirect = new short[EntitiesModel.XY_FOR_ARRAY];

                    switch (startDirectionT)
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
                        x = (byte)(xy_0[EntitiesModel.X] + xyDirect[EntitiesModel.X]);
                        y = (byte)(xy_0[EntitiesModel.Y] + xyDirect[EntitiesModel.Y]);

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
            CloudE = default;
        }
    }
}