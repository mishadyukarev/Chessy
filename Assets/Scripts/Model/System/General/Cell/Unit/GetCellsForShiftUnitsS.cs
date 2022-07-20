using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Model.System
{
    sealed class GetCellsForShiftUnitsS : SystemModelAbstract
    {
        readonly float[][] _howManyDistanceNeed = new float[IndexCellsValues.CELLS][];
        readonly bool[][] _whereUnitCanShiftCs = new bool[IndexCellsValues.CELLS][];

        internal GetCellsForShiftUnitsS(in SystemsModel s, EntitiesModel e) : base(s, e)
        {
            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                _howManyDistanceNeed[cellIdx_0] = _e.HowManyDistanceNeedForShiftingUnitC(cellIdx_0).HowManyArray;
                _whereUnitCanShiftCs[cellIdx_0] = _e.WhereUnitCanShiftC(cellIdx_0).WhereArray;
            }
        }

        internal void Get()
        {
            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (_cellCs[cellIdxCurrent_0].IsBorder) continue;

                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    _whereUnitCanShiftCs[cellIdxCurrent_0][idxCell] = default;
                    _howManyDistanceNeed[cellIdxCurrent_0][idxCell] = default;
                }


                var curUnitC = _unitCs[cellIdxCurrent_0];

                if (!_e.UnitEffectsC(cellIdxCurrent_0).IsStunned &&  curUnitC.UnitT.HaveUnit())
                {
                    foreach (var toCellIdx in _e.IdxsCellsAround(cellIdxCurrent_0))
                    {
                        var dirT = _e.DirectionAround(cellIdxCurrent_0, toCellIdx);

                        var needDistance = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (_e.HillC(toCellIdx).HaveAnyResources) needDistance += StepValues.HILL;

                        if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                        {
                            if (!_e.HealthTrail(toCellIdx).IsAlive(dirT.Invert()))
                            {
                                needDistance += StepValues.ADULT_FOREST;
                            }
                        }

                        switch (curUnitC.UnitT)
                        {
                            case UnitTypes.King:
                                break;

                            case UnitTypes.Pawn:
                                if (_e.MainToolWeaponT(cellIdxCurrent_0).Is(ToolsWeaponsWarriorTypes.Staff))
                                {
                                    needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;

                                    if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                                    {
                                        if (!_e.HealthTrail(toCellIdx).IsAlive(dirT.Invert()))
                                        {
                                            needDistance -= StepValues.ADULT_FOREST / 2;
                                        }
                                    }

                                    if (_e.HillC(toCellIdx).HaveAnyResources)
                                    {
                                        needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                    }
                                }
                                else if (_e.MainToolWeaponT(cellIdxCurrent_0) == ToolsWeaponsWarriorTypes.BowCrossbow)
                                {
                                    needDistance -= StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                }
                                break;

                            case UnitTypes.Elfemale:
                                if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                                {
                                    if (!_e.HealthTrail(toCellIdx).IsAlive(dirT.Invert()))
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
           

                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent_0).IsShifting)
                        {
                            var toUnitT = _e.UnitT(toCellIdx);

                            if (!toUnitT.HaveUnit() || toUnitT.HaveUnit() && _e.ShiftingInfoForUnitC(toCellIdx).IsShifting)
                            {
                                if (!_e.MountainC(toCellIdx).HaveAnyResources)
                                {
                                    _whereUnitCanShiftCs[cellIdxCurrent_0][toCellIdx] = true;
                                }
                            }
                        }
                    } 
                }
            }
        }
    }
}