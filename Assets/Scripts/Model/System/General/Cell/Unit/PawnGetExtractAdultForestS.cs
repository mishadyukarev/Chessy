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

                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed
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

                        if (_e.AdultForestC(cellIdxCurrent).Resources < extract) extract = _e.AdultForestC(cellIdxCurrent).Resources;

                        _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractAdultForest = extract;
                    }
                }
            }
        }
    }
}