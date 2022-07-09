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
                if (!_e.IsBorder(cellIdxCurrent))
                {
                    for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                    {
                        _e.TrailVisibleC(cellIdxCurrent).Set(PlayerTypes.First, false);
                        _e.TrailVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, false);

                        if (_e.UnitT(cellIdxCurrent).HaveUnit()) _e.TrailVisibleC(cellIdxCurrent).Set(_e.UnitPlayerT(cellIdxCurrent), true);


                        for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                        {
                            var idx_1 = _e.GetIdxCellByDirect(cellIdxCurrent, DistanceFromCellTypes.First, dir);

                            if (_e.UnitT(idx_1).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                            {
                                _e.TrailVisibleC(cellIdxCurrent).Set(_e.UnitPlayerT(idx_1), true);
                            }
                        }
                    }
                }
            }
        }
    }
}