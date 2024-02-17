using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetTrailsVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    visibleTrailCs[cellIdxCurrent].Set(playerT, false);
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!CellC(cellIdxCurrent).IsBorder)
                {
                    if (hpTrailCs[cellIdxCurrent].HaveAnyTrail)
                    {
                        if (unitCs[cellIdxCurrent].HaveUnit)
                            visibleTrailCs[cellIdxCurrent].Set(unitCs[cellIdxCurrent].PlayerT, true);


                        foreach (var cellIdx1 in IdxsAroundCellC(cellIdxCurrent).IdxCellsAroundArray)
                        {
                            if (unitCs[cellIdx1].HaveUnit && !unitCs[cellIdxCurrent].UnitT.IsAnimal())
                            {
                                visibleTrailCs[cellIdxCurrent].Set(unitCs[cellIdx1].PlayerT, true);
                            }
                        }
                    }
                }
            }
        }
    }
}