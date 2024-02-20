using Chessy.Model.Entity;
using Chessy.Model.Values;
using UnityEngine;

namespace Chessy.Model.System
{
    sealed class TryExecuteShiftingUnitS : SystemModelAbstract
    {
        internal TryExecuteShiftingUnitS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void TryExecute()
        {
            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (cellCs[currentCellIdx_0].IsBorder) continue;


                var shiftUnit_0 = shiftingUnitCs[currentCellIdx_0];
                var neededDistanceForShiftingUnitC_0 = _howManyDistanceNeedForShiftingUnitCs[currentCellIdx_0];

                var whereNeedShiftIdxCell_1 = shiftUnit_0.WhereNeedShiftIdxCell;
                var unitC_1 = unitCs[whereNeedShiftIdxCell_1];
                var unitShiftC_1 = UnitShiftC(whereNeedShiftIdxCell_1);


                if (whereNeedShiftIdxCell_1 != 0)
                {
                    if (shiftUnit_0.NeedReturnBack)
                    {
                        shiftUnit_0.Distance -= Time.deltaTime;

                        if (shiftUnit_0.Distance <= 0)
                        {
                            shiftUnit_0.Dispose();

                            s.GetDataCellsS.GetDataCells();
                        }

                        else
                        {
                            if (shiftUnit_0.Distance / neededDistanceForShiftingUnitC_0.HowMany(whereNeedShiftIdxCell_1) >= 0.3)
                            {
                                if (!unitC_1.HaveUnit)
                                {
                                    shiftUnit_0.NeedReturnBack = false;
                                }
                            }
                        }
                    }

                    else
                    {
                        if (unitC_1.HaveUnit && !unitShiftC_1.IsShifting)
                        {
                            shiftUnit_0.NeedReturnBack = true;
                        }
                        else
                        {
                            shiftUnit_0.Distance += Time.deltaTime;

                            if (shiftUnit_0.Distance >= neededDistanceForShiftingUnitC_0.HowMany(whereNeedShiftIdxCell_1))
                            {
                                if (!unitC_1.HaveUnit)
                                {
                                    s.ShiftUnitOnOtherCellM(currentCellIdx_0, whereNeedShiftIdxCell_1);

                                    shiftUnit_0.Dispose();

                                    s.GetDataCellsS.GetDataCells();
                                }

                                else
                                {
                                    shiftUnit_0.NeedReturnBack = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}