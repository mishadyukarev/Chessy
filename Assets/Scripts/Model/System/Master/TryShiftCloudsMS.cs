using Chessy.Model.Entity;
using Chessy.Model.Values;
using System.Collections.Generic;
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
                if (_cellCs[cell_0].IsBorder) continue;
                if (!_cloudCs[cell_0].IsCenter) continue;


                var cell_1 = _shiftCloudCs[cell_0].WhereNeedShiftIdxCell;
                var directXy_1 = _xyCellsCs[cell_1].Xy;


                var isInSquareNextCell = directXy_1[0] >= 4 && directXy_1[0] <= 11 && directXy_1[1] >= 3 && directXy_1[1] <= 7;

                if (isInSquareNextCell)
                {
                    var adding = Time.deltaTime * _windC.Speed * 0.25f;

                    _shiftCloudCs[cell_0].Distance += adding;

                    if (_shiftCloudCs[cell_0].Distance >= 1)
                    {
                        var savedIdxsAround = new List<CloudOnCellE>();

                        foreach (var aroundCell_0 in _idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                        {
                            savedIdxsAround.Add(_cloudOnCellEs[aroundCell_0]);
                            _cloudOnCellEs[aroundCell_0].Dispose();
                        }

                        ShiftCloud(cell_0, cell_1);
                        _shiftCloudCs[cell_1].WhereNeedShiftIdxCell = _cellsByDirectAroundC[cell_1].Get(_windC.DirectT);


                        byte idx = 0;
                        foreach (var aroundCell_0 in _idxsAroundCellCs[cell_1].IdxCellsAroundArray)
                        {
                            _cloudOnCellEs[aroundCell_0].Clone(savedIdxsAround[idx]);

                            _cloudWhereViewDataCs[savedIdxsAround[idx].WhereSkinAndWhereDataInfoC.ViewIdxCell].DataIdxCell = idx;

                            idx++;
                        }
                    }


                    //var pos_0 = _possitionCellCs[cell_0].Position;
                    //var pos_1 = _possitionCellCs[cell_1].Position;

                    //var t = _shiftCloudCs[cell_0].Distance;

                    //_e.CloudPossitionC(_cloudWhereViewDataCs[cell_0].ViewIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);

                    //foreach (var aroundCell_0 in _idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                    //{
                    //    pos_0 = _possitionCellCs[aroundCell_0].Position;
                    //    pos_1 = _possitionCellCs[_cellsByDirectAroundC[aroundCell_0].Get(_windC.DirectT)].Position;

                    //    _e.CloudPossitionC(_cloudWhereViewDataCs[aroundCell_0].ViewIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);
                    //}
                }
                else
                {
                    _windC.DirectT = (DirectTypes)Random.Range(1, (byte)DirectTypes.End);


                    _shiftCloudCs[cell_0].WhereNeedShiftIdxCell = _cellsByDirectAroundC[cell_0].Get(_windC.DirectT);
                }
            }
        }

        void ShiftCloud(in byte cellIdx_0, in byte cellIdx_1)
        {
            var dataIdx_0 = _cloudWhereViewDataCs[cellIdx_0].DataIdxCell;
            var dataIdx_1 = _cloudWhereViewDataCs[cellIdx_1].DataIdxCell;

            var viewIdx_0 = _cloudWhereViewDataCs[cellIdx_0].ViewIdxCell;
            var viewIdx_1 = _cloudWhereViewDataCs[cellIdx_1].ViewIdxCell;


            _cloudOnCellEs[cellIdx_1].Clone(_cloudOnCellEs[cellIdx_0]);
            _cloudOnCellEs[cellIdx_0].Dispose();


            _cloudWhereViewDataCs[cellIdx_0].DataIdxCell = dataIdx_0;
            _cloudWhereViewDataCs[cellIdx_1].DataIdxCell = dataIdx_1;


            _cloudWhereViewDataCs[viewIdx_1].DataIdxCell = cellIdx_1;



            _shiftCloudCs[cellIdx_1].Dispose();
        }
    }
}