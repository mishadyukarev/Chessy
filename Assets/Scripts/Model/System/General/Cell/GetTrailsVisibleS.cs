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
                        if (_unitCs[cellIdxCurrent].HaveUnit)
                            _visibleTrailCs[cellIdxCurrent].Set(_unitCs[cellIdxCurrent].PlayerT, true);


                        foreach (var cellIdx1 in _idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                        {
                            if (_unitCs[cellIdx1].HaveUnit && !_unitCs[cellIdxCurrent].UnitT.IsAnimal())
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