using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void PawnGetExtractAdultForest()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.PawnExtractAdultForestC(cellIdxCurrent).Resources = 0;

                if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn) && _eMG.UnitConditionTC(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                        && !_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                    {
                        var extract = ExtractPawnValues.EXTRACT_PAWM_ADULT_FOREST;

                        //if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                        //{
                        //    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        //    {
                        //        extract *= ExtractPawnForestValues.ELFEMALE_PAWN_ADULT_FOREST;
                        //    }
                        //}



                        if (_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Axe))
                        {
                            if (_eMG.MainTWLevelTC(cellIdxCurrent).Is(LevelTypes.Second))
                            {
                                extract *= ExtractPawnValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                            }
                        }

                        if (_eMG.AdultForestC(cellIdxCurrent).Resources < extract) extract = _eMG.AdultForestC(cellIdxCurrent).Resources;

                        _eMG.PawnExtractAdultForestC(cellIdxCurrent).Resources = extract;
                    }
                }
            }
        }
    }
}