using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class PawnGetExtractAdultForestS : SystemModel
    {
        internal PawnGetExtractAdultForestS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.PawnExtractAdultForestC(cell_0).Resources = 0;

            if (eMG.AdultForestC(cell_0).HaveAnyResources)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && !eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = ExtractPawnForestValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                    {
                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            extract *= ExtractPawnForestValues.ELFEMALE_PAWN_ADULT_FOREST;
                        }
                    }



                    if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                    {
                        if (eMG.MainTWLevelTC(cell_0).Is(LevelTypes.Second))
                        {
                            extract *= ExtractPawnForestValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (eMG.AdultForestC(cell_0).Resources < extract) extract = eMG.AdultForestC(cell_0).Resources;

                    eMG.PawnExtractAdultForestC(cell_0).Resources = extract;
                }
            }
        }
    }
}