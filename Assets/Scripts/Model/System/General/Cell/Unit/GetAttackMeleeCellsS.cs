using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetAttackMeleeCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                if (!_e.StunUnitC(cellIdxCell).IsStunned)
                {
                    if (_e.UnitT(cellIdxCell).HaveUnit() && _e.UnitT(cellIdxCell).IsMelee(_e.MainToolWeaponT(cellIdxCell)) && !_e.UnitT(cellIdxCell).IsAnimal())
                    {
                        DirectTypes dir_cur = default;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.AroundCellsE(cellIdxCell).AroundCellE(dirT).IdxC.Idx;

                            dir_cur += 1;

                            if (!_e.MountainC(idx_1).HaveAnyResources)
                            {
                                var haveMaxSteps = _e.StepUnitC(cellIdxCell).Steps >= StepValues.MAX;

                                if (_e.StepUnitC(cellIdxCell).Steps >= _e.UnitNeedStepsForShiftC(cellIdxCell).NeedSteps(idx_1) || haveMaxSteps)
                                {
                                    if (_e.UnitT(idx_1).HaveUnit())
                                    {
                                        if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCell)))
                                        {
                                            if (_e.UnitT(cellIdxCell).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    _e.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                }
                                                else _e.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                            }
                                            else
                                            {
                                                _e.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
