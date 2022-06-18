using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetEffectsForUnits()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.HaveKingEffect(cellIdxCurrent) = false;

                if (!_eMG.IsBorder(cellIdxCurrent))
                {
                    foreach (var idx_1 in _eMG.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_eMG.UnitTC(idx_1).Is(UnitTypes.King))
                        {
                            if (_eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT))
                            {
                                _eMG.PlayerInfoE(_eMG.UnitPlayerTC(idx_1).PlayerT).WhereKingEffects.Add(cellIdxCurrent);
                                _eMG.HaveKingEffect(cellIdxCurrent) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}