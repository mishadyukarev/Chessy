using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetFarmExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood = 0;

                if (buildingCs[cellIdxCurrent].BuildingT == BuildingTypes.Farm)
                {
                    if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer))
                    {
                        double extract = ValuesChessy.FARM_EXTRACT;

                        //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                        //{
                        //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                        //}

                        if (environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Fertilizer) < extract) extract = environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Fertilizer);

                        extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood = extract;
                    }
                }
            }
        }
    }
}