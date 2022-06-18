using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetBuildingVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.BuildingTC(cellIdxCurrent).HaveBuilding)
                {
                    _eMG.BuildingVisibleC(cellIdxCurrent).Set(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT, true);

                    if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _eMG.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            if (_eMG.UnitTC(idx_1).HaveUnit)
                            {
                                if (!_eMG.UnitPlayerTC(idx_1).Is(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        _eMG.BuildingVisibleC(cellIdxCurrent).Set(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT.NextPlayer(), isVisibledNextPlayer);
                    }
                    else _eMG.BuildingVisibleC(cellIdxCurrent).Set(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT.NextPlayer(), true);


                    _eMG.BuildingVisibleC(cellIdxCurrent).Set(PlayerTypes.First, true);
                    _eMG.BuildingVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, true);
                }
            }
        }
    }
}