using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void PawnGetExtractAdultForest()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractAdultForest = 0;

                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && _unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed
                        && !_mainTWC[cellIdxCurrent].ToolWeaponT.Is(ToolsWeaponsWarriorTypes.BowCrossbow, ToolsWeaponsWarriorTypes.Staff))
                    {
                        var extract = ExtractPawnValues.EXTRACT_PAWM_ADULT_FOREST;

                        //if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitT.Is(UnitTypes.Elfemale))
                        //{
                        //    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        //    {
                        //        extract *= ExtractPawnForestValues.ELFEMALE_PAWN_ADULT_FOREST;
                        //    }
                        //}



                        if (_mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Axe)
                        {
                            if (_mainTWC[cellIdxCurrent].LevelT == LevelTypes.Second)
                            {
                                extract *= ExtractPawnValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                            }
                        }

                        if (_environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.AdultForest) < extract) extract = _environmentCs[cellIdxCurrent].Resources(EnvironmentTypes.AdultForest);

                        _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractAdultForest = extract;
                    }
                }
            }
        }
    }
}