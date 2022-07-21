﻿using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetKingEffectsForUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _hasUnitKingEffectHereCs[cellIdxCurrent].Set(playerT, false);
                }
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!_cellCs[cellIdxCurrent].IsBorder)
                {
                    if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.King)
                    {
                        foreach (var cellIdxNext in _idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                        {
                            _hasUnitKingEffectHereCs[cellIdxNext].Set(_unitCs[cellIdxCurrent].PlayerT, true);
                        }
                    }
                }
            }
        }
    }
}