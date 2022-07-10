using Chessy.Model.Enum;
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
                    _e.TrailVisibleC(cellIdxCurrent).Set(playerT, false);
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!_e.IsBorder(cellIdxCurrent))
                {
                    if (_e.HealthTrail(cellIdxCurrent).HaveAnyTrail)
                    {
                        if (_e.UnitT(cellIdxCurrent).HaveUnit())
                            _e.TrailVisibleC(cellIdxCurrent).Set(_e.UnitPlayerT(cellIdxCurrent), true);


                        foreach (var cellIdx1 in _e.IdxsCellsAround(cellIdxCurrent, DistanceFromCellTypes.First))
                        {
                            if (_e.UnitT(cellIdx1).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                            {
                                _e.TrailVisibleC(cellIdxCurrent).Set(_e.UnitPlayerT(cellIdx1), true);
                            }
                        }
                    }
                }
            }
        }
    }
}