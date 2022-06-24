using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetFarmExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.BuildingExtractionC(cellIdxCurrent).HowManyFarmCanExtractFood = 0;

                if (_e.IsBuildingOnCell(cellIdxCurrent, BuildingTypes.Farm))
                {
                    if (_e.FertilizeC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = EnvironmentValues.FARM_EXTRACT;

                        //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                        //{
                        //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                        //}

                        if (_e.FertilizeC(cellIdxCurrent).Resources < extract) extract = _e.FertilizeC(cellIdxCurrent).Resources;

                        _e.BuildingExtractionC(cellIdxCurrent).HowManyFarmCanExtractFood = extract;
                    }
                }
            }
        }
    }
}