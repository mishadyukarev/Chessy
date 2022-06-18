using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellsForShiftUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.CellsForShift(cellIdxCurrent).Clear();

                for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    _eMG.UnitNeedStepsForShiftC(cellIdxCurrent).Set(idx, 0);

                if (!_eMG.IsBorder(cellIdxCurrent))
                {
                    if (!_eMG.StunUnitC(cellIdxCurrent).IsStunned && _eMG.UnitTC(cellIdxCurrent).HaveUnit && !_eMG.UnitTC(cellIdxCurrent).IsAnimal)
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_to = _eMG.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Tree))
                            {
                                needSteps = 1;
                            }
                            else
                            {
                                if (!_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Undead))
                                {
                                    if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn) && _eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                    {
                                        needSteps /= 2;
                                    }
                                    else if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Snowy))
                                    {
                                        if (_eMG.FertilizeC(idx_to).HaveAnyResources)
                                        {
                                            needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                        }
                                    }


                                    if (_eMG.AdultForestC(idx_to).HaveAnyResources)
                                    {
                                        if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn))
                                        {
                                            if (!_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                            {
                                                needSteps += StepValues.ADULT_FOREST;

                                                if (_eMG.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                            }
                                        }
                                        else if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {
                                            needSteps /= 2;
                                        }
                                        else
                                        {
                                            needSteps += StepValues.ADULT_FOREST;

                                            if (_eMG.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else
                                    {
                                        if (!_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {

                                        }
                                    }

                                    if (_eMG.HillC(idx_to).HaveAnyResources)
                                    {
                                        if (!_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                                        {
                                            needSteps += StepValues.HILL;
                                        }
                                    }
                                }
                            }



                            _eMG.UnitNeedStepsForShiftC(cellIdxCurrent).Set(idx_to, needSteps);

                            if (!_eMG.MountainC(idx_to).HaveAnyResources && !_eMG.UnitTC(idx_to).HaveUnit)
                            {
                                if (needSteps <= _eMG.StepUnitC(cellIdxCurrent).Steps || _eMG.StepUnitC(cellIdxCurrent).Steps >= StepValues.MAX)
                                {
                                    _eMG.CellsForShift(cellIdxCurrent).Add(idx_to);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}