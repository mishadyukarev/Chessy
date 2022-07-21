using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetBuildingVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_buildingCs[cellIdxCurrent].HaveBuilding)
                {
                    _visibleBuildingCs[cellIdxCurrent].Set(_buildingCs[cellIdxCurrent].PlayerT, true);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.GetIdxCellByDirectAround(cellIdxCurrent, dirT);

                            if (_e.UnitT(idx_1).HaveUnit())
                            {
                                if (_unitCs[idx_1].PlayerT != _buildingCs[cellIdxCurrent].PlayerT)
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        _visibleBuildingCs[cellIdxCurrent].Set(_buildingCs[cellIdxCurrent].PlayerT.NextPlayer(), isVisibledNextPlayer);
                    }
                    else _visibleBuildingCs[cellIdxCurrent].Set(_buildingCs[cellIdxCurrent].PlayerT.NextPlayer(), true);


                    _visibleBuildingCs[cellIdxCurrent].Set(PlayerTypes.First, true);
                    _visibleBuildingCs[cellIdxCurrent].Set(PlayerTypes.Second, true);
                }
            }
        }
    }
}