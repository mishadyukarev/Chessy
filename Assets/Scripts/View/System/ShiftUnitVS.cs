using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using Photon.Pun;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class ShiftUnitVS : SystemViewAbstract
    {
        readonly Transform[] _unitTrans = new Transform[IndexCellsValues.CELLS];

        Vector3 _unitPossition;

        internal ShiftUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                _unitTrans[cellIdx_0] = eV.CellEs(cellIdx_0).UnitEs.ParentTC.Transform;
            }
        }

        internal override void Sync()
        {


            for (byte cellCurrent_0 = 0; cellCurrent_0 < IndexCellsValues.CELLS; cellCurrent_0++)
            {
                if (cellCs[cellCurrent_0].IsBorder) continue;

                var unitWhereViewDataC_0 = unitWhereViewDataCs[cellCurrent_0];
                if (!unitWhereViewDataC_0.HaveDataReference) continue;


                var unitTransView_0 = _unitTrans[cellCurrent_0];

                var cellIdx_1 = unitWhereViewDataC_0.DataIdxCellP;
                var shiftUnitC_1 = shiftingUnitCs[cellIdx_1];
                var isShifting_1 = shiftUnitC_1.IsShifting;


                if (isShifting_1)
                {
                    var possitionStart_0 = positionCellCs[cellIdx_1].PositionP;

                    var whereNeedShiftIdxCell_2 = shiftUnitC_1.WhereNeedShiftIdxCellP;

                    var possitionEnd_2 = positionCellCs[whereNeedShiftIdxCell_2].PositionP;

                    var t = shiftUnitC_1.DistanceP / _howManyDistanceNeedForShiftingUnitCs[cellIdx_1].HowMany(whereNeedShiftIdxCell_2);

                    _unitPossition = Vector3.Lerp(possitionStart_0, possitionEnd_2, t);

                }
                else
                {
                    _unitPossition = positionCellCs[cellIdx_1].PositionP;
                }



                if (_unitPossition.magnitude > 0)
                {
                    var t = Time.deltaTime * 7f;
                    if (!PhotonNetwork.IsMasterClient) t /= 1.5f;
                    if (t > 1) t = 1;

                    unitTransView_0.position = Vector3.Lerp(unitTransView_0.position, _unitPossition, t);
                }
            }
        }
    }
}