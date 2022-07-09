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
                    for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = _e.GetIdxCellByDirect(cellIdxCurrent, dirT);


                        var needDistance = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (_e.HillC(idx_to).HaveAnyResources) needDistance += StepValues.HILL;

                        if (_e.AdultForestC(idx_to).HaveAnyResources)
                        {     
                            if (!_e.HealthTrail(idx_to).IsAlive(dirT.Invert()))
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

                                    if (_e.AdultForestC(idx_to).HaveAnyResources)
                                    {
                                        if (!_e.HealthTrail(idx_to).IsAlive(dirT.Invert()))
                                        {
                                            needDistance -= StepValues.ADULT_FOREST / 2;
                                        }
                                    }

                                    if (_e.HillC(idx_to).HaveAnyResources)
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
                                if (_e.AdultForestC(idx_to).HaveAnyResources)
                                {
                                    if (!_e.HealthTrail(idx_to).IsAlive(dirT.Invert()))
                                    {
                                        needDistance -= StepValues.ADULT_FOREST;                                 
                                    }
                                    needDistance -= StepValues.ADULT_FOREST / 2;
                                }
                                break;

                            case UnitTypes.Snowy:
                                if (_e.WaterOnCellC(idx_to).HaveAnyResources)
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


                        //if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Tree))
                        //{
                        //    needDistance = 1;
                        //}
                        //else
                        //{
                        //    if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Undead))
                        //    {
                        //        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                        //        {
                        //            needDistance /= 2;
                        //        }


                        //        if (_e.AdultForestC(idx_to).HaveAnyResources)
                        //        {
                        //            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                        //            {
                        //                if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                        //                {
                        //                    needDistance += StepValues.ADULT_FOREST;

                        //                    if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needDistance -= StepValues.BONUS_TRAIL;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                needDistance += StepValues.ADULT_FOREST;

                        //                if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needDistance -= StepValues.BONUS_TRAIL;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Elfemale))
                        //            {

                        //            }
                        //        }

                        //        if (_e.HillC(idx_to).HaveAnyResources)
                        //        {
                        //            if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                        //            {
                        //                needDistance += StepValues.HILL;
                        //            }
                        //        }
                        //    }
                        //}

                        _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).Set(idx_to, needDistance);

                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent).IsShiftingUnit)
                        {
                            if (!_e.UnitT(idx_to).HaveUnit() || _e.UnitT(idx_to).HaveUnit() && _e.ShiftingInfoForUnitC(idx_to).IsShiftingUnit)
                            {
                                if (!_e.MountainC(idx_to).HaveAnyResources)
                                {
                                    _e.WhereUnitCanShiftC(cellIdxCurrent).Set(idx_to, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}