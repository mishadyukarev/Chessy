using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TryShiftUnitAIS_M : SystemModel
    {
        readonly Dictionary<byte, byte> _pointsCellsForShiftUnit = new Dictionary<byte, byte>();
        byte _theMostBigPointForShiftUnit;

        internal TryShiftUnitAIS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                _pointsCellsForShiftUnit.Add(cellIdxStart, default);
            }
        }

        internal void TryShift()
        {
            var playerBotT = PlayerTypes.Second;



            for (byte idxCellStart = 0; idxCellStart < StartValues.CELLS; idxCellStart++)
            {
                if (!eMG.CellE(idxCellStart).IsActiveParentSelf) continue;

                for (byte idxCellElse = 0; idxCellElse < StartValues.CELLS; idxCellElse++)
                {
                    _pointsCellsForShiftUnit[idxCellElse] = 0;
                }

                _theMostBigPointForShiftUnit = 0;

                if (eMG.UnitT(idxCellStart) == UnitTypes.Pawn && eMG.UnitPlayerT(idxCellStart) == playerBotT)
                {
                    foreach (var idxCellDirect in eMG.AroundCellsE(idxCellStart).CellsAround)
                    {
                        if (eMG.CellE(idxCellDirect).IsActiveParentSelf)
                        {
                            if (!eMG.UnitTC(idxCellDirect).HaveUnit && !eMG.MountainC(idxCellDirect).HaveAnyResources)
                            {
                                _pointsCellsForShiftUnit[idxCellDirect]++;
                                _theMostBigPointForShiftUnit++;

                                if (eMG.AdultForestC(idxCellDirect).HaveAnyResources)
                                {
                                    _pointsCellsForShiftUnit[idxCellDirect]++;
                                    _theMostBigPointForShiftUnit++;
                                }
                            }
                        }
                    }

                    var currentPoint = _theMostBigPointForShiftUnit;
                    var isShifted = false;

                    for (var i = 0; i < _theMostBigPointForShiftUnit; i++)
                    {
                        foreach (var item in _pointsCellsForShiftUnit)
                        {
                            var idxCell = item.Key;
                            var point = item.Value;

                            if (point == currentPoint)
                            {
                                if (Random.Range(0f, 1f) < 0.25f)
                                {
                                    if (eMG.CellsForShift(idxCellStart).Contains(idxCell))
                                    {
                                        if(eMG.UnitNeedStepsForShiftC(idxCellStart).NeedSteps(idxCell) <= eMG.StepUnit(idxCellStart))
                                        {
                                            sMG.UnitSs.ShiftOnOtherCellS.Shift(idxCellStart, idxCell);
                                            isShifted = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (isShifted) break;
                        currentPoint--;
                    }
                }
            }
        }
    }
}