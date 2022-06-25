using Chessy.Model.Extensions;
using Chessy.Model.Values;

namespace Chessy.Model
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetBuildingVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.BuildingOnCellT(cellIdxCurrent).HaveBuilding())
                {
                    _e.BuildingVisibleC(cellIdxCurrent).Set(_e.BuildingPlayerT(cellIdxCurrent), true);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            if (_e.UnitT(idx_1).HaveUnit())
                            {
                                if (!_e.UnitPlayerT(idx_1).Is(_e.BuildingPlayerT(cellIdxCurrent)))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        _e.BuildingVisibleC(cellIdxCurrent).Set(_e.BuildingPlayerT(cellIdxCurrent).NextPlayer(), isVisibledNextPlayer);
                    }
                    else _e.BuildingVisibleC(cellIdxCurrent).Set(_e.BuildingPlayerT(cellIdxCurrent).NextPlayer(), true);


                    _e.BuildingVisibleC(cellIdxCurrent).Set(PlayerTypes.First, true);
                    _e.BuildingVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, true);
                }
            }
        }
    }
}