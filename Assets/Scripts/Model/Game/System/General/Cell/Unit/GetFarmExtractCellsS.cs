using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetFarmExtractCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.FarmExtractFertilizeC(cellIdxCurrent).Resources = 0;

                if (_eMG.BuildingTC(cellIdxCurrent).Is(BuildingTypes.Farm))
                {
                    if (_eMG.FertilizeC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = EnvironmentValues.FARM_EXTRACT;

                        //if (E.BuildingsInfo(E.BuildEs(Idx)).HaveCenterUpgrade)
                        //{
                        //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                        //}

                        if (_eMG.FertilizeC(cellIdxCurrent).Resources < extract) extract = _eMG.FertilizeC(cellIdxCurrent).Resources;

                        _eMG.FarmExtractFertilizeC(cellIdxCurrent).Resources = extract;
                    }
                }
            }
        }
    }
}