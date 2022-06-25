using Chessy.Model.Values;

namespace Chessy.Model
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetKingEffectsForUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.HasKingEffectHereC(cellIdxCurrent).Set(playerT, false);
                }
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (!_e.IsBorder(cellIdxCurrent))
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.King))
                    {
                        foreach (var cellIdxNext in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                        {
                            _e.HasKingEffectHereC(cellIdxNext).Set(_e.UnitPlayerT(cellIdxCurrent), true);
                        }
                    }
                }
            }
        }
    }
}