using Chessy.Model.Enum;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetKingEffectsForUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.HasKingEffectHereC(cellIdxCurrent).Set(playerT, false);
                }
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!_cellCs[cellIdxCurrent].IsBorder)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.King))
                    {
                        foreach (var cellIdxNext in _e.IdxsCellsAround(cellIdxCurrent))
                        {
                            _e.HasKingEffectHereC(cellIdxNext).Set(_e.UnitPlayerT(cellIdxCurrent), true);
                        }
                    }
                }
            }
        }
    }
}