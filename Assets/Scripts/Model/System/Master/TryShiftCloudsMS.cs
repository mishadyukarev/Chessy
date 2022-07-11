using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System.Collections.Generic;
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
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (_e.IsBorder(cell_0)) continue;
                if (!_e.IsCenterCloud(cell_0)) continue;


                var cell_1 = _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell;
                var directXy_1 = _e.XyCell(cell_1);


                var isInSquareNextCell = directXy_1[0] >= 4 && directXy_1[0] <= 11 && directXy_1[1] >= 3 && directXy_1[1] <= 7;

                if (isInSquareNextCell)
                {
                    var adding = Time.deltaTime * _e.WindC.Speed * 0.25f;

                    _e.CloudShiftingC(cell_0).Distance += adding;

                    if (_e.CloudShiftingC(cell_0).Distance >= 1)
                    {  
                        var savedIdxsAround = new List<CloudOnCellE>();

                        foreach (var aroundCell_0 in _e.IdxsCellsAround(cell_0, DistanceFromCellTypes.First))
                        {
                            savedIdxsAround.Add(_e.CloudOnCellE(aroundCell_0));
                            _e.CloudOnCellE(aroundCell_0).Dispose();
                        }

                        ShiftCloud(cell_0, cell_1);
                        _e.CloudShiftingC(cell_1).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cell_1, DistanceFromCellTypes.First, _e.DirectWindT);


                        byte idx = 0;
                        foreach (var aroundCell_0 in _e.IdxsCellsAround(cell_1, DistanceFromCellTypes.First))
                        {
                            _e.CloudOnCellE(aroundCell_0) = savedIdxsAround[idx];

                            _e.CloudWhereViewDataOnCellC(savedIdxsAround[idx].WhereSkinAndWhereDataInfoC.ViewIdxCell).DataIdxCell = idx;

                            idx++;
                        }
                    }


                    var pos_0 = _e.CellPossitionC(cell_0).Position;
                    var pos_1 = _e.CellPossitionC(cell_1).Position;

                    var t = _e.CloudShiftingC(cell_0).Distance;

                    _e.CloudPossitionC(_e.CloudWhereViewDataOnCellC(cell_0).ViewIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);

                    foreach (var aroundCell_0 in _e.IdxsCellsAround(cell_0, DistanceFromCellTypes.First))
                    {
                        pos_0 = _e.CellPossitionC(aroundCell_0).Position;
                        pos_1 = _e.CellPossitionC(_e.GetIdxCellByDirect(aroundCell_0, DistanceFromCellTypes.First, _e.DirectWindT)).Position;

                        _e.CloudPossitionC(_e.CloudWhereViewDataOnCellC(aroundCell_0).ViewIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);
                    }
                }
                else
                {
                    _e.DirectWindT = (DirectTypes)Random.Range(1, (byte)DirectTypes.End);


                    _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cell_0, DistanceFromCellTypes.First, _e.DirectWindT);


                    //_e.CloudShiftingC(cellIdx_0).Dispose();
                    ////ShiftCloud(cellIdx_0, cellIdx_1);

                    //if (_e.WindC.DirectT == DirectTypes.Right || _e.WindC.DirectT == DirectTypes.Up || _e.WindC.DirectT == DirectTypes.RightDown)
                    //{
                    //    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    //    {
                    //        if (_e.HaveCloud(cell_0))
                    //        {
                    //            if (_e.CloudShiftingC(cell_0).IsShifting)
                    //            {
                    //                var idx_1 = _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell;
                    //                var xy_1 = _e.XyCell(idx_1);

                    //                isInSquareNextCell = xy_1[0] >= 3 && xy_1[0] <= 12 && xy_1[1] >= 2 && xy_1[1] <= 8;

                    //                if (!isInSquareNextCell)
                    //                {
                    //                    _e.CloudShiftingC(cell_0).Dispose();
                    //                }
                    //            }
                    //        }
                    //    }


                    //    for (var i = 0; i < 20; i++)
                    //    {
                    //        for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    //        {
                    //            if (_e.IsBorder(cell_0)) continue;

                    //            if (_e.HaveCloud(cell_0))
                    //            {
                    //                if (_e.CloudShiftingC(cell_0).IsShifting)
                    //                {
                    //                    var cell_1 = _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell;

                    //                    if (!_e.HaveCloud(cell_1))
                    //                    {
                    //                        ShiftCloud(cell_0, cell_1);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}


                    //_e.WindC.DirectT = _e.WindC.DirectT.Invert();// (DirectTypes)UnityEngine.Random.Range(1, (byte)DirectTypes.End);

                    //for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    //{
                    //    if (_e.IsBorder(cell_0)) continue;

                    //    if (_e.HaveCloud(cell_0))
                    //    {
                    //        _e.CloudShiftingC(cell_0).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cell_0, DistanceFromCellTypes.First, _e.DirectWindT);
                    //    }
                    //}

                    //break;
                }
            }
        }

        void ShiftCloud(in byte cellIdx_0, in byte cellIdx_1)
        {
            var dataIdx_0 = _e.CloudWhereViewDataOnCellC(cellIdx_0).DataIdxCell;
            var dataIdx_1 = _e.CloudWhereViewDataOnCellC(cellIdx_1).DataIdxCell;

            var viewIdx_0 = _e.CloudWhereViewDataOnCellC(cellIdx_0).ViewIdxCell;
            var viewIdx_1 = _e.CloudWhereViewDataOnCellC(cellIdx_1).ViewIdxCell;


            _e.CloudOnCellE(cellIdx_1) = _e.CloudOnCellE(cellIdx_0);
            _e.CloudOnCellE(cellIdx_0) = default;


            _e.CloudWhereViewDataOnCellC(cellIdx_0).DataIdxCell = dataIdx_0;
            _e.CloudWhereViewDataOnCellC(cellIdx_1).DataIdxCell = dataIdx_1;


            _e.CloudWhereViewDataOnCellC(viewIdx_1).DataIdxCell = cellIdx_1;



            _e.CloudShiftingC(cellIdx_1) = default;
        }

        void SetPossitionCloud(in byte cellIdx_0, in byte cellIdx_1)
        {
            
        }
    }
}