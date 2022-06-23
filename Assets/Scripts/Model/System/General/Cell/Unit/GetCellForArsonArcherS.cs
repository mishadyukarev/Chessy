﻿using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellForArsonArcher()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.UnitForArsonC(cellIdxCurrent).Clear();

                if (!_e.StunUnitC(cellIdxCurrent).IsStunned)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            if (!_e.HaveFire(idx_1))
                            {
                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    _e.UnitForArsonC(cellIdxCurrent).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}