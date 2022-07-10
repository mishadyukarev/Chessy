using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using UnityEditor;
using UnityEngine;

namespace Chessy.Model.System
{
    sealed class TryShiftCloudsMS : SystemModelAbstract
    {
        internal TryShiftCloudsMS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void TryShift()
        {
            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                if (_e.IsBorder(cellIdx_0)) continue;
                if (!_e.HaveCloud(cellIdx_0)) continue;


                var cellIdx_1 = _e.CloudShiftingC(cellIdx_0).WhereNeedShiftIdxCell;
                var directXy = _e.XyCell(cellIdx_1);


                var isInSquareNextCell = directXy[0] >= 3 && directXy[0] <= 12 && directXy[1] >= 2 && directXy[1] <= 8;

                if (isInSquareNextCell)
                {
                    _e.CloudShiftingC(cellIdx_0).Distance += Time.deltaTime * _e.WindC.Speed * 0.25f;

                    if (_e.CloudShiftingC(cellIdx_0).Distance >= 1)
                    {
                        if (!_e.CloudC(cellIdx_1).HaveCloud)
                        {

                            var dataIdx_0 = _e.CloudWhereViewDataOnCell(cellIdx_0).DataIdxCell;
                            var dataIdx_1 = _e.CloudWhereViewDataOnCell(cellIdx_1).DataIdxCell;

                            var viewIdx_0 = _e.CloudWhereViewDataOnCell(cellIdx_0).ViewIdxCell;
                            var viewIdx_1 = _e.CloudWhereViewDataOnCell(cellIdx_1).ViewIdxCell;


                            _e.CloudOnCellE(cellIdx_1) = _e.CloudOnCellE(cellIdx_0);
                            _e.CloudOnCellE(cellIdx_0) = default;


                            _e.CloudWhereViewDataOnCell(cellIdx_0).DataIdxCell = dataIdx_0;
                            _e.CloudWhereViewDataOnCell(cellIdx_1).DataIdxCell = dataIdx_1;


                            _e.CloudWhereViewDataOnCell(viewIdx_1).DataIdxCell = cellIdx_1;



                            _e.CloudShiftingC(cellIdx_1).Distance = default;
                            _e.CloudShiftingC(cellIdx_1).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cellIdx_1, DistanceFromCellTypes.First, _e.DirectWindT);
                        }
                    }
                }
                else
                {
                    _e.CloudShiftingC(cellIdx_0).Distance = default;
                    _e.CloudShiftingC(cellIdx_0).WhereNeedShiftIdxCell = default;

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_e.HaveCloud(cell_0))
                        {
                            var _1 = _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell;

                            var dataIdx_0 = _e.CloudWhereViewDataOnCell(cell_0).DataIdxCell;
                            var dataIdx_1 = _e.CloudWhereViewDataOnCell(_1).DataIdxCell;

                            var viewIdx_0 = _e.CloudWhereViewDataOnCell(cell_0).ViewIdxCell;
                            var viewIdx_1 = _e.CloudWhereViewDataOnCell(_1).ViewIdxCell;


                            _e.CloudOnCellE(_1) = _e.CloudOnCellE(cell_0);
                            _e.CloudOnCellE(cell_0) = default;


                            _e.CloudWhereViewDataOnCell(cell_0).DataIdxCell = dataIdx_0;
                            _e.CloudWhereViewDataOnCell(_1).DataIdxCell = dataIdx_1;


                            _e.CloudWhereViewDataOnCell(viewIdx_1).DataIdxCell = _1;



                            _e.CloudShiftingC(_1).Distance = default;
                            _e.CloudShiftingC(_1).WhereNeedShiftIdxCell = default;
                        }
                    }

                    _e.WindC.DirectT = (DirectTypes)UnityEngine.Random.Range(1, (byte)DirectTypes.End);

                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (_e.IsBorder(cellIdx)) continue;

                        if (_e.CloudC(cellIdx).HaveCloud)
                        {
                            _e.CloudShiftingC(cellIdx).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cellIdx, DistanceFromCellTypes.First, _e.DirectWindT);
                            _e.CloudShiftingC(cellIdx).Distance = 0;
                        }
                    }
                }

                var pos_0 = _e.CellPossitionC(cellIdx_0).Position;
                var pos_1 = _e.CellPossitionC(cellIdx_1).Position;

                var t = _e.CloudShiftingC(cellIdx_0).Distance;

                _e.CloudPossitionC(_e.CloudWhereViewDataOnCell(cellIdx_0).ViewIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);
            }


            //if (needChangeDirect)
            //{
            //    //for (var i = 0; i < 99; i++)
            //    //{
            //    //    v();

            //    //    var needExit = true;

            //    //    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            //    //    {
            //    //        if (_e.IsBorder(cellIdx)) continue;
            //    //        if (!_e.HaveCloud(cellIdx)) continue;

            //    //        if (_e.CloudShiftingC(cellIdx).IsShifting) needExit = false;
            //    //    }
            //    //    if (needExit) break;
            //    //}

                
            //}

            //void v(byte b)
            //{
                
            //}
        }
    }
}