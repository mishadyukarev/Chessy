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


                if (_e.AdultForestC(cellIdx).Resources < extract) extract = _e.AdultForestC(cellIdx).Resources;


                _extractionBuildingCs[cellIdx].HowManyWoodcutterCanExtractWood = extract;
            }
        }
    }
}