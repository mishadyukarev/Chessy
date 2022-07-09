using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetCellsForShiftUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;


                

                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    _e.WhereUnitCanShiftC(cellIdxCurrent).Set(idxCell, false);
                    _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).Set(idxCell, 0);
                }

                if (!_e.UnitEffectsC(cellIdxCurrent).IsStunned && _e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    foreach (var toCellIdx in _e.IdxsCellsAround(cellIdxCurrent, DistanceFromCellTypes.First))
                    {
                        var dirT = _e.DirectionAround(cellIdxCurrent, toCellIdx);

                        var needDistance = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (_e.HillC(toCellIdx).HaveAnyResources) needDistance += StepValues.HILL;

                        if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                        {
                            if (!_e.HealthTrail(toCellIdx).IsAlive(dirT.Invert()))
                            {
                                needDistance += StepValues.ADULT_FOREST;
                            }
                        }

                        switch (_e.UnitT(cellIdxCurrent))
                        {
                            case UnitTypes.King:
                                break;

                            case UnitTypes.Pawn:
                                if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
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
                                else if (_e.MainToolWeaponT(cellIdxCurrent) == ToolsWeaponsWarriorTypes.BowCrossbow)
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
                                break;

                            case UnitTypes.Wolf:
                                break;

                            default: throw new Exception();
                        }

                        _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).Set(toCellIdx, needDistance);

                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent).IsShiftingUnit)
                        {
                            if (!_e.UnitT(toCellIdx).HaveUnit() || _e.UnitT(toCellIdx).HaveUnit() && _e.ShiftingInfoForUnitC(toCellIdx).IsShiftingUnit)
                            {
                                if (!_e.MountainC(toCellIdx).HaveAnyResources)
                                {
                                    _e.WhereUnitCanShiftC(cellIdxCurrent).Set(toCellIdx, true);
                                }
                            }
                        }
                    }


                        
                    
                }
            }
        }
    }
}