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

                if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && _extraTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Pick && _unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                {
                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (_environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Hill) < extract) extract = _environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Hill);


                        _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractHill = extract;

                    }
                }
            }
        }
    }
}