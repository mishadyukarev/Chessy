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
        //readonly HowManyDistanceNeedForShiftingUnitC[] _howManyDistanceNeed = new HowManyDistanceNeedForShiftingUnitC[IndexCellsValues.CELLS];
        //readonly WhereUnitCanShiftC[] _whereUnitCanShiftCs = new WhereUnitCanShiftC[IndexCellsValues.CELLS];

        internal GetCellsForShiftUnitsS(in SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            //for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            //{
            //    //_howManyDistanceNeed[cellIdx] = e.HowManyDistanceNeedForShiftingUnitC(cellIdx);
            //    //_whereUnitCanShiftCs[cellIdx] = e.WhereUnitCanShiftC(cellIdx);
            //}
        }

        internal void Get()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;


                var curWhereUnitCanShift = _e.WhereUnitCanShiftC(cellIdxCurrent);
                var curHowManyDistanceNeed = _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent);


                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    curWhereUnitCanShift.Set(idxCell, false);
                    curHowManyDistanceNeed.Set(idxCell, 0);
                }


                var curUnitT = _e.UnitT(cellIdxCurrent);

                if (!_e.UnitEffectsC(cellIdxCurrent).IsStunned &&  curUnitT.HaveUnit())
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

                        switch (curUnitT)
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
                                needDistance += StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL * 2;
                                break;

                            case UnitTypes.Wolf:
                                break;

                            default: throw new Exception();
                        }

                        curHowManyDistanceNeed.Set(toCellIdx, needDistance);



                        //var vv = _howManyDistanceNeed[cellIdxCurrent].HowMany(toCellIdx);

                        //var v = _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).HowMany(toCellIdx);

                        //if (true)
                        //{
                            
                        //}

                        

                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent).IsShifting)
                        {
                            var toUnitT = _e.UnitT(toCellIdx);

                            if (!toUnitT.HaveUnit() || toUnitT.HaveUnit() && _e.ShiftingInfoForUnitC(toCellIdx).IsShifting)
                            {
                                if (!_e.MountainC(toCellIdx).HaveAnyResources)
                                {
                                    _e.WhereUnitCanShiftC(cellIdxCurrent).Set(toCellIdx, true);
                                    //_whereUnitCanShiftCs[cellIdxCurrent].Set(toCellIdx, true);
                                }
                            }
                        }
                    } 
                }
            }
        }
    }
}