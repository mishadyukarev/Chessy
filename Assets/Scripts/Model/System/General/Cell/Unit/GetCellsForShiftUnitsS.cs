using Chessy.Model.Entity;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed class GetCellsForShiftUnitsS : SystemModelAbstract
    {
        internal GetCellsForShiftUnitsS(in SystemsModel s, EntitiesModel e) : base(s, e)
        {
        }

        internal void Get()
        {
            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (CellC(cellIdxCurrent_0).IsBorder) continue;

                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    WhereUnitCanShiftC(cellIdxCurrent_0).WhereArray[idxCell] = default;
                    _howManyDistanceNeedForShiftingUnitCs[cellIdxCurrent_0].HowManyArray[idxCell] = default;
                }


                var curUnitC = unitCs[cellIdxCurrent_0];

                if (!_effectsUnitCs[cellIdxCurrent_0].IsStunned && curUnitC.UnitT.HaveUnit())
                {
                    foreach (var toCellIdx in IdxsAroundCellC(cellIdxCurrent_0).IdxCellsAroundArray)
                    {
                        var dirT = cellAroundCs[cellIdxCurrent_0, toCellIdx].DirectT;

                        var needDistance = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Hill)) needDistance += StepValues.HILL;

                        if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            if (!hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
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

                                    if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                                    {
                                        if (!hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
                                        {
                                            needDistance -= StepValues.ADULT_FOREST / 2;
                                        }
                                    }

                                    if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Hill))
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
                                if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    if (!hpTrailCs[toCellIdx].IsAlive(dirT.Invert()))
                                    {
                                        needDistance -= StepValues.ADULT_FOREST;
                                    }
                                    needDistance -= StepValues.ADULT_FOREST / 2;
                                }
                                break;

                            case UnitTypes.Snowy:
                                if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Fertilizer))
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

                        _howManyDistanceNeedForShiftingUnitCs[cellIdxCurrent_0].HowManyArray[toCellIdx] = needDistance;


                        if (!shiftingUnitCs[cellIdxCurrent_0].IsShifting)
                        {
                            var toUnitT = unitCs[toCellIdx].UnitT;

                            if (!toUnitT.HaveUnit() || toUnitT.HaveUnit() && shiftingUnitCs[toCellIdx].IsShifting)
                            {
                                if (!environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Mountain))
                                {
                                    WhereUnitCanShiftC(cellIdxCurrent_0).WhereArray[toCellIdx] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}