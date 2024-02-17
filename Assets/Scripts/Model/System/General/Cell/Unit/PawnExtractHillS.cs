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

                if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && _extraTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Pick && unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                {
                    if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        double extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Hill) < extract) extract = environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.Hill);


                        _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractHill = (float)extract;

                    }
                }
            }
        }
    }
}