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
                if (_eMG.CellE(idxCellStart).IsBorder) continue;

                for (byte idxCellElse = 0; idxCellElse < StartValues.CELLS; idxCellElse++)
                {
                    _pointsCellsForShiftUnit[idxCellElse] = 0;
                }

                _theMostBigPointForShiftUnit = 0;

                if (_eMG.UnitT(idxCellStart) == UnitTypes.Pawn && _eMG.UnitPlayerT(idxCellStart) == playerBotT)
                {
                    foreach (var idxCellDirect in _eMG.AroundCellsE(idxCellStart).CellsAround)
                    {
                        if (!_eMG.CellE(idxCellDirect).IsBorder)
                        {
                            if (!_eMG.UnitTC(idxCellDirect).HaveUnit && !_eMG.MountainC(idxCellDirect).HaveAnyResources)
                            {
                                _pointsCellsForShiftUnit[idxCellDirect]++;
                                _theMostBigPointForShiftUnit++;

                                if (_eMG.AdultForestC(idxCellDirect).HaveAnyResources)
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
                                    if (_eMG.CellsForShift(idxCellStart).Contains(idxCell))
                                    {
                                        if(_eMG.UnitNeedStepsForShiftC(idxCellStart).NeedSteps(idxCell) <= _eMG.StepUnit(idxCellStart))
                                        {
                                            _sMG.ShiftUnitOnOtherCellM(idxCellStart, idxCell);
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