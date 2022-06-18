using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetTrailsVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (!_eMG.IsBorder(cellIdxCurrent))
                {
                    for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                    {
                        _eMG.TrailVisibleC(cellIdxCurrent).Set(PlayerTypes.First, false);
                        _eMG.TrailVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, false);

                        if (_eMG.UnitTC(cellIdxCurrent).HaveUnit) _eMG.TrailVisibleC(cellIdxCurrent).Set(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT, true);


                        for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                        {
                            var idx_1 = _eMG.AroundCellsE(cellIdxCurrent).IdxCell(dir);

                            if (_eMG.UnitTC(idx_1).HaveUnit && !_eMG.UnitTC(cellIdxCurrent).IsAnimal)
                            {
                                _eMG.TrailVisibleC(cellIdxCurrent).Set(_eMG.UnitPlayerTC(idx_1).PlayerT, true);
                            }
                        }
                    }
                }
            }
        }
    }
}