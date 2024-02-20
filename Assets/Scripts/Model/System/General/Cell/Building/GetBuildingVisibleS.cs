using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetBuildingVisible()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (buildingCs[cellIdxCurrent].HaveBuilding)
                {
                    visibleBuildingCs[cellIdxCurrent].Set(buildingCs[cellIdxCurrent].PlayerT, true);

                    if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = cellsByDirectAroundC[cellIdxCurrent].Get(dirT);

                            if (unitCs[idx_1].HaveUnit)
                            {
                                if (unitCs[idx_1].PlayerT != buildingCs[cellIdxCurrent].PlayerT)
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        visibleBuildingCs[cellIdxCurrent].Set(buildingCs[cellIdxCurrent].PlayerT.NextPlayer(), isVisibledNextPlayer);
                    }
                    else visibleBuildingCs[cellIdxCurrent].Set(buildingCs[cellIdxCurrent].PlayerT.NextPlayer(), true);


                    visibleBuildingCs[cellIdxCurrent].Set(PlayerTypes.First, true);
                    visibleBuildingCs[cellIdxCurrent].Set(PlayerTypes.Second, true);
                }
            }
        }
    }
}