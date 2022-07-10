﻿using Chessy.Model.Enum;
using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetAttackMeleeCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < IndexCellsValues.CELLS; cellIdxCell++)
            {
                if (_e.IsBorder(cellIdxCell)) continue;

                if (!_e.UnitEffectsC(cellIdxCell).IsStunned)
                {
                    if (_e.UnitT(cellIdxCell).HaveUnit() && _e.UnitT(cellIdxCell).IsMelee(_e.MainToolWeaponT(cellIdxCell)) && !_e.UnitT(cellIdxCell).IsAnimal() && !_e.ShiftingInfoForUnitC(cellIdxCell).IsShifting)
                    {
                        DirectTypes dir_cur = default;

                        foreach (var idx_1 in _e.IdxsCellsAround(cellIdxCell, DistanceFromCellTypes.First))
                        {
                            dir_cur += 1;

                            if (!_e.MountainC(idx_1).HaveAnyResources)
                            {
                                if (_e.UnitT(idx_1).HaveUnit() && !_e.ShiftingInfoForUnitC(idx_1).IsShifting)
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
