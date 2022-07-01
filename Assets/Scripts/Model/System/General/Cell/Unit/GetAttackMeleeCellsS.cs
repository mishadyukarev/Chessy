using Chessy.Model.Values;
namespace Chessy.Model
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetAttackMeleeCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                if (!_e.UnitEffectsC(cellIdxCell).IsStunned)
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
                                var haveMaxSteps = _e.EnergyUnitC(cellIdxCell).Energy >= StepValues.MAX;

                                if (_e.EnergyUnitC(cellIdxCell).Energy >= _e.HowManyEnergyNeedForShiftingUnitC(cellIdxCell).HowManyEnergyNeedForShiftingToHere(idx_1) || haveMaxSteps)
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
                                                    _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                                }
                                                else _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                            }
                                            else
                                            {
                                                _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
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
