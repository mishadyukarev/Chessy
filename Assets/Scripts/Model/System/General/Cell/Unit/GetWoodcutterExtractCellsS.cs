using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        public void GetWoodcutterExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_cellCs[cellIdxCurrent].IsBorder) continue;

                GetWoodcutterExtractCells(cellIdxCurrent);
            }
        }

        public void GetWoodcutterExtractCells(in byte cellIdx)
        {
            _extractionBuildingCs[cellIdx].HowManyWoodcutterCanExtractWood = 0;

            if (_buildingCs[cellIdx].BuildingT == BuildingTypes.Woodcutter)
            {
                var extract = ValuesChessy.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (_environmentCs[cellIdx].Resources(EnvironmentTypes.AdultForest) < extract) extract = _environmentCs[cellIdx].Resources(EnvironmentTypes.AdultForest);


                _extractionBuildingCs[cellIdx].HowManyWoodcutterCanExtractWood = extract;
            }
        }
    }
}