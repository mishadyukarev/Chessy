using Chessy.Model.Enum;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetVisibleUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.UnitVisibleC(cellIdxCurrent).Set(playerT, true);
                }

                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {

                    if (_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var cell_1 = _e.GetIdxCellByDirect(cellIdxCurrent, DistanceFromCellTypes.First, dirT);

                                if (_e.UnitT(cell_1).HaveUnit())
                                {
                                    if (_e.UnitPlayerT(cell_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (_e.UnitPlayerT(cell_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        _e.UnitVisibleC(cellIdxCurrent).Set(PlayerTypes.First, isVisForFirst);
                        _e.UnitVisibleC(cellIdxCurrent).Set(PlayerTypes.Second, isVisForSecond);
                    }

                    else
                    {
                        if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in _e.IdxsCellsAround(cellIdxCurrent, DistanceFromCellTypes.First))
                            {
                                if (_e.UnitT(idx_1).HaveUnit())
                                {
                                    if (!_e.UnitT(idx_1).IsAnimal())
                                    {
                                        if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCurrent)))
                                        {
                                            isVisibledNextPlayer = true;
                                        }
                                    }
                                }
                            }

                            _e.UnitVisibleC(cellIdxCurrent).Set(_e.UnitPlayerT(cellIdxCurrent).NextPlayer(), isVisibledNextPlayer);
                        }
                    }
                }
            }
        }
    }
}