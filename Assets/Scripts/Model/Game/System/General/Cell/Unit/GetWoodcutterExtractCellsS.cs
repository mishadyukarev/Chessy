using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        public void GetWoodcutterExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.WoodcutterExtractC(cellIdxCurrent).Resources = 0;

                if (_eMG.BuildingTC(cellIdxCurrent).Is(BuildingTypes.Woodcutter))
                {
                    var extract = EnvironmentValues.WOODCUTTER_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.WOODCUTTER_CENTER_UPGRADE;
                    //}


                    if (_eMG.AdultForestC(cellIdxCurrent).Resources < extract) extract = _eMG.AdultForestC(cellIdxCurrent).Resources;


                    _eMG.WoodcutterExtractC(cellIdxCurrent).Resources = extract;
                }
            }
        }
    }
}