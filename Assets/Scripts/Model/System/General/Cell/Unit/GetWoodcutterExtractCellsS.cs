using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        public void GetWoodcutterExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;

                GetWoodcutterExtractCells(cellIdxCurrent);
            }
        }

        public void GetWoodcutterExtractCells(in byte cellIdx)
        {
            _e.BuildingExtractionC(cellIdx).HowManyWoodcutterCanExtractWood = 0;

            if (_e.IsBuildingOnCell(cellIdx, BuildingTypes.Woodcutter))
            {
                var extract = ValuesChessy.WOODCUTTER_EXTRACT;

                //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                //{
                //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                //}


                if (_e.AdultForestC(cellIdx).Resources < extract) extract = _e.AdultForestC(cellIdx).Resources;


                _e.BuildingExtractionC(cellIdx).HowManyWoodcutterCanExtractWood = extract;
            }
        }
    }
}