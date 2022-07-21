﻿using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed class GetAttackMeleeCellsS : SystemModelAbstract
    {
        internal GetAttackMeleeCellsS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {

        }

        internal void Get()
        {
            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (_cellCs[currentCellIdx_0].IsBorder) continue;


                if (!_effectsUnitCs[currentCellIdx_0].IsStunned)
                {
                    var curUnitMainC_0 = _unitCs[currentCellIdx_0];
                    var curUnitT_0 = curUnitMainC_0.UnitT;
                    var curUnitPlayerT_0 = curUnitMainC_0.PlayerT;

                    if (curUnitT_0.HaveUnit() && curUnitT_0.IsMelee(_mainTWC[currentCellIdx_0].ToolWeaponT) && !curUnitT_0.IsAnimal() && !_shiftingUnitCs[currentCellIdx_0].IsShifting)
                    {
                        DirectTypes directT_1 = default;

                        foreach (var idx_1 in _e.IdxsCellsAround(currentCellIdx_0))
                        {
                            directT_1 = _e.CellAroundC(currentCellIdx_0, idx_1).DirectT;

                            var curUnitMain_1 = _unitCs[idx_1];
                            var curUnitT_1 = curUnitMain_1.UnitT;
                            var curUnitPlayerT_1 = curUnitMain_1.PlayerT;

                            if (!_e.MountainC(idx_1).HaveAnyResources)
                            {
                                if (curUnitT_1.HaveUnit() && !_shiftingUnitCs[idx_1].IsShifting)
                                {
                                    if (curUnitPlayerT_1 != curUnitPlayerT_0)
                                    {
                                        if (curUnitT_0 == UnitTypes.Pawn)
                                        {
                                            if (directT_1 == DirectTypes.Left || directT_1 == DirectTypes.Right
                                           || directT_1 == DirectTypes.Up || directT_1 == DirectTypes.Down)
                                            {
                                                _whereSimpleAttackCs[currentCellIdx_0].Set(idx_1, true);
                                            }
                                            else _whereUniqueAttackCs[currentCellIdx_0].Set(idx_1, true);
                                        }
                                        else
                                        {
                                            _whereSimpleAttackCs[currentCellIdx_0].Set(idx_1, true);
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
