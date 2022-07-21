using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetTrailsVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _visibleTrailCs[cellIdxCurrent].Set(playerT, false);
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!_cellCs[cellIdxCurrent].IsBorder)
                {
                    if (_hpTrailCs[cellIdxCurrent].HaveAnyTrail)
                    {
                        if (_e.UnitT(cellIdxCurrent).HaveUnit())
                            _visibleTrailCs[cellIdxCurrent].Set(_unitCs[cellIdxCurrent].PlayerT, true);


                        foreach (var cellIdx1 in _e.IdxsCellsAround(cellIdxCurrent))
                        {
                            if (_e.UnitT(cellIdx1).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                            {
                                _visibleTrailCs[cellIdxCurrent].Set(_unitCs[cellIdx1].PlayerT, true);
                            }
                        }
                    }
                }
            }
        }
    }
}