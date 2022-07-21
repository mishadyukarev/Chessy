using Chessy.Model.Entity;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed class GetCellsForShiftUnitsS : SystemModelAbstract
    {
        readonly float[][] _howManyDistanceNeed = new float[IndexCellsValues.CELLS][];

        internal GetCellsForShiftUnitsS(in SystemsModel s, EntitiesModel e) : base(s, e)
        {
            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                _howManyDistanceNeed[cellIdx_0] = _howManyDistanceNeedForShiftingUnitCs[cellIdx_0].HowManyArray;
            }
        }

        internal void Get()
        {
            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (_cellCs[cellIdxCurrent_0].IsBorder) continue;

                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    _whereUnitCanShiftCs[cellIdxCurrent_0].Set(idxCell, default);
                    _howManyDistanceNeed[cellIdxCurrent_0][idxCell] = default;
                }


                var curUnitC = _unitCs[cellIdxCurrent_0];

                if (!_effectsUnitCs[cellIdxCurrent_0].IsStunned && curUnitC.UnitT.HaveUnit())
                {
                    foreach (var toCellIdx in _e.IdxsCellsAround(cellIdxCurrent_0))
                    {
                        var dirT = _e.CellAroundC(cellIdxCurrent_0, toCellIdx).DirectT;

                        var needDistance = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (_e.HillC(toCellIdx).HaveAnyResources) needDistance += StepValues.HILL;

                        if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                        {
                            if (!_hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
                            {
                                needDistance += StepValues.ADULT_FOREST;
                            }
                        }

                        switch (curUnitC.UnitT)
                        {
                            case UnitTypes.King:
                                break;

                            case UnitTypes.Pawn:
                                if (_mainTWC[cellIdxCurrent_0].ToolWeaponT == ToolsWeaponsWarriorTypes.Staff)
                                {
                                    needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;

                                    if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                                    {
                                        if (!_hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
                                        {
                                            needDistance -= StepValues.ADULT_FOREST / 2;
                                        }
                                    }

                                    if (_e.HillC(toCellIdx).HaveAnyResources)
                                    {
                                        needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                    }
                                }
                                else if (_mainTWC[cellIdxCurrent_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                                {
                                    needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                }
                                break;

                            case UnitTypes.Elfemale:
                                if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                                {
                                    if (!_hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
                                    {
                                        needDistance -= StepValues.ADULT_FOREST;
                                    }
                                    needDistance -= StepValues.ADULT_FOREST / 2;
                                }
                                break;

                            case UnitTypes.Snowy:
                                if (_e.WaterOnCellC(toCellIdx).HaveAnyResources)
                                {
                                    needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                }
                                break;

                            case UnitTypes.Undead:
                                break;

                            case UnitTypes.Hell:
                                break;

                            case UnitTypes.Skeleton:
                                break;

                            case UnitTypes.Tree:
                                needDistance += StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL * 2;
                                break;

                            case UnitTypes.Wolf:
                                break;

                            default: throw new Exception();
                        }

                        _howManyDistanceNeed[cellIdxCurrent_0][toCellIdx] = needDistance;


                        if (!_shiftingUnitCs[cellIdxCurrent_0].IsShifting)
                        {
                            var toUnitT = _e.UnitT(toCellIdx);

                            if (!toUnitT.HaveUnit() || toUnitT.HaveUnit() && _shiftingUnitCs[toCellIdx].IsShifting)
                            {
                                if (!_e.MountainC(toCellIdx).HaveAnyResources)
                                {
                                    _whereUnitCanShiftCs[cellIdxCurrent_0].Set(toCellIdx, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}