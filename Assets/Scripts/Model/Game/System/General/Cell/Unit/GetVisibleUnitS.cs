using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetVisibleUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _eMG.UnitVisibleC(cellIdxCurrent).Set(playerT, true);
                }

                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {

                    if (_eMG.UnitTC(cellIdxCurrent).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var cell_1 = _eMG.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                                if (_eMG.UnitTC(cell_1).HaveUnit)
                                {
                                    if (_eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (_eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        _eMG.UnitVisibleC(cellIdxCurrent).Set(PlayerTypes.First, isVisForFirst);
                        _eMG.UnitVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, isVisForSecond);
                    }

                    else
                    {
                        if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in _eMG.AroundCellsE(cellIdxCurrent).CellsAround)
                            {
                                if (_eMG.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!_eMG.UnitTC(idx_1).IsAnimal)
                                    {
                                        if (!_eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerT(cellIdxCurrent)))
                                        {
                                            isVisibledNextPlayer = true;
                                        }
                                    }
                                }
                            }

                            _eMG.UnitVisibleC(cellIdxCurrent).Set(_eMG.UnitPlayerT(cellIdxCurrent).NextPlayer(), isVisibledNextPlayer);
                        }
                    }
                }
            }
        }
    }
}