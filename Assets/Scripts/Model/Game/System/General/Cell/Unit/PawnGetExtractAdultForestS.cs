using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class PawnGetExtractAdultForestS : SystemModelGameAbs
    {
        internal PawnGetExtractAdultForestS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.PawnExtractAdultForestE(cell_0).Resources = 0;

            if (eMG.AdultForestC(cell_0).HaveAnyResources)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && !eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).MyHeroTC.Is(UnitTypes.Elfemale))
                    {
                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            extract *= 2;
                        }
                    }



                    if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                    {
                        if (eMG.UnitMainTWLevelTC(cell_0).Is(LevelTypes.Second))
                        {
                            extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (eMG.AdultForestC(cell_0).Resources < extract) extract = eMG.AdultForestC(cell_0).Resources;

                    eMG.PawnExtractAdultForestE(cell_0).Resources = extract;
                }
            }
        }
    }
}