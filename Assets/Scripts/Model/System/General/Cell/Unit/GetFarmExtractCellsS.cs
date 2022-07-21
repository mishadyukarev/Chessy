using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetFarmExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood = 0;

                if (_buildingCs[cellIdxCurrent].BuildingT == BuildingTypes.Farm)
                {
                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer))
                    {
                        var extract = ValuesChessy.FARM_EXTRACT;

                        //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                        //{
                        //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                        //}

                        if (_environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Fertilizer) < extract) extract = _environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Fertilizer);

                        _extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood = extract;
                    }
                }
            }
        }
    }
}