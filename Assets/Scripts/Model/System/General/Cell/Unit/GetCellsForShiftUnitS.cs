using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellsForShiftUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.CellsForShift(cellIdxCurrent).Clear();

                for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    _e.UnitNeedStepsForShiftC(cellIdxCurrent).Set(idx, 0);

                if (!_e.IsBorder(cellIdxCurrent))
                {
                    if (!_e.StunUnitC(cellIdxCurrent).IsStunned && _e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_to = _e.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Tree))
                            {
                                needSteps = 1;
                            }
                            else
                            {
                                if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Undead))
                                {
                                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                    {
                                        needSteps /= 2;
                                    }
                                    else if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Snowy))
                                    {
                                        if (_e.FertilizeC(idx_to).HaveAnyResources)
                                        {
                                            needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                        }
                                    }


                                    if (_e.AdultForestC(idx_to).HaveAnyResources)
                                    {
                                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                                        {
                                            if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                            {
                                                needSteps += StepValues.ADULT_FOREST;

                                                if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                            }
                                        }
                                        else if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {
                                            needSteps /= 2;
                                        }
                                        else
                                        {
                                            needSteps += StepValues.ADULT_FOREST;

                                            if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else
                                    {
                                        if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {

                                        }
                                    }

                                    if (_e.HillC(idx_to).HaveAnyResources)
                                    {
                                        if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                        {
                                            needSteps += StepValues.HILL;
                                        }
                                    }
                                }
                            }



                            _e.UnitNeedStepsForShiftC(cellIdxCurrent).Set(idx_to, needSteps);

                            if (!_e.MountainC(idx_to).HaveAnyResources && !_e.UnitT(idx_to).HaveUnit())
                            {
                                if (needSteps <= _e.StepUnitC(cellIdxCurrent).Steps || _e.StepUnitC(cellIdxCurrent).Steps >= StepValues.MAX)
                                {
                                    _e.CellsForShift(cellIdxCurrent).Add(idx_to);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}