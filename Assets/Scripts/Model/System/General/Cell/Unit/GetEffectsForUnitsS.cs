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
                    _hasUnitKingEffectHereCs[cellIdxCurrent].Set(playerT, false);
                }
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!CellC(cellIdxCurrent).IsBorder)
                {
                    if (unitCs[cellIdxCurrent].UnitT == UnitTypes.King)
                    {
                        foreach (var cellIdxNext in IdxsAroundCellC(cellIdxCurrent).IdxCellsAroundArray)
                        {
                            _hasUnitKingEffectHereCs[cellIdxNext].Set(unitCs[cellIdxCurrent].PlayerT, true);
                        }
                    }
                }
            }
        }
    }
}