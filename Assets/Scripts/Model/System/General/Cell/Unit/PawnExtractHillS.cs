using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetPawnExtractHill()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractHill = 0;

                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _extraTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Pick && _unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                {
                    if (_e.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (_e.HillC(cellIdxCurrent).Resources < extract) extract = _e.HillC(cellIdxCurrent).Resources;


                        _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractHill = extract;

                    }
                }
            }
        }
    }
}