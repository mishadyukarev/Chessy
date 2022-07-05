using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void PawnGetExtractAdultForest()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractAdultForest = 0;

                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                        && !_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.BowCrossbow, ToolsWeaponsWarriorTypes.Staff))
                    {
                        var extract = ExtractPawnValues.EXTRACT_PAWM_ADULT_FOREST;

                        //if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitT.Is(UnitTypes.Elfemale))
                        //{
                        //    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        //    {
                        //        extract *= ExtractPawnForestValues.ELFEMALE_PAWN_ADULT_FOREST;
                        //    }
                        //}



                        if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Axe))
                        {
                            if (_e.MainTWLevelT(cellIdxCurrent).Is(LevelTypes.Second))
                            {
                                extract *= ExtractPawnValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                            }
                        }

                        if (_e.AdultForestC(cellIdxCurrent).Resources < extract) extract = _e.AdultForestC(cellIdxCurrent).Resources;

                        _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractAdultForest = extract;
                    }
                }
            }
        }
    }
}