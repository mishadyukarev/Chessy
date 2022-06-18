using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetAttackMeleeCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                if (!_eMG.StunUnitC(cellIdxCell).IsStunned)
                {
                    if (_eMG.UnitTC(cellIdxCell).HaveUnit && _eMG.UnitTC(cellIdxCell).IsMelee(_eMG.MainToolWeaponTC(cellIdxCell).ToolWeaponT) && !_eMG.UnitTC(cellIdxCell).IsAnimal)
                    {
                        DirectTypes dir_cur = default;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _eMG.AroundCellsE(cellIdxCell).AroundCellE(dirT).IdxC.Idx;

                            dir_cur += 1;

                            if (!_eMG.MountainC(idx_1).HaveAnyResources)
                            {
                                var haveMaxSteps = _eMG.StepUnitC(cellIdxCell).Steps >= StepValues.MAX;

                                if (_eMG.StepUnitC(cellIdxCell).Steps >= _eMG.UnitNeedStepsForShiftC(cellIdxCell).NeedSteps(idx_1) || haveMaxSteps)
                                {
                                    if (_eMG.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!_eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerTC(cellIdxCell).PlayerT))
                                        {
                                            if (_eMG.UnitTC(cellIdxCell).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                }
                                                else _eMG.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                            }
                                            else
                                            {
                                                _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
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
