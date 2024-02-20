using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        public void GetWoodcutterExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (cellCs[cellIdxCurrent].IsBorder) continue;

                GetWoodcutterExtractCells(cellIdxCurrent);
            }
        }

        public void GetWoodcutterExtractCells(in byte cellIdx)
        {
            extractionBuildingCs[cellIdx].HowManyWoodcutterCanExtractWood = 0;

            if (buildingCs[cellIdx].BuildingT == BuildingTypes.Woodcutter)
            {
                double extract = ValuesChessy.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (environmentCs[cellIdx].Resources(EnvironmentTypes.AdultForest) < extract) extract = environmentCs[cellIdx].Resources(EnvironmentTypes.AdultForest);


                extractionBuildingCs[cellIdx].HowManyWoodcutterCanExtractWood = extract;
            }
        }
    }
}