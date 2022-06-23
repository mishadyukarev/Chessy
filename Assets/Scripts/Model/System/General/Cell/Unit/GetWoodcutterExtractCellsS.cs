using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        public void GetWoodcutterExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.WoodcutterExtractC(cellIdxCurrent).Resources = 0;

                if (_e.IsBuildingOnCell(cellIdxCurrent, BuildingTypes.Woodcutter))
                {
                    var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                    //}


                    if (_e.AdultForestC(cellIdxCurrent).Resources < extract) extract = _e.AdultForestC(cellIdxCurrent).Resources;


                    _e.WoodcutterExtractC(cellIdxCurrent).Resources = extract;
                }
            }
        }
    }
}