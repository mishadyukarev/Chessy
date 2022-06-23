using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetEffectsForUnits()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.HaveKingEffect(cellIdxCurrent) = false;

                if (!_e.IsBorder(cellIdxCurrent))
                {
                    foreach (var idx_1 in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_e.UnitT(idx_1).Is(UnitTypes.King))
                        {
                            if (_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCurrent)))
                            {
                                _e.PlayerInfoE(_e.UnitPlayerT(idx_1)).WhereKingEffects.Add(cellIdxCurrent);
                                _e.HaveKingEffect(cellIdxCurrent) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}