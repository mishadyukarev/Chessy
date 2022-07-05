using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetFarmExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _e.BuildingExtractionC(cellIdxCurrent).HowManyFarmCanExtractFood = 0;

                if (_e.IsBuildingOnCell(cellIdxCurrent, BuildingTypes.Farm))
                {
                    if (_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = ValuesChessy.FARM_EXTRACT;

                        //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                        //{
                        //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                        //}

                        if (_e.WaterOnCellC(cellIdxCurrent).Resources < extract) extract = _e.WaterOnCellC(cellIdxCurrent).Resources;

                        _e.BuildingExtractionC(cellIdxCurrent).HowManyFarmCanExtractFood = extract;
                    }
                }
            }
        }
    }
}