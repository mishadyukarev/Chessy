using Chessy.Game.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    public struct PawnGetExtractAdultForestS
    {
        public PawnGetExtractAdultForestS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.PawnExtractAdultForestE(cell_0).Resources = 0;

            if (e.AdultForestC(cell_0).HaveAnyResources)
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed) && !e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Elfemale))
                    {
                        if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            extract *= 2;
                        }
                    }



                    if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                    {
                        if (e.UnitMainTWLevelTC(cell_0).Is(LevelTypes.Second))
                        {
                            extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (e.AdultForestC(cell_0).Resources < extract) extract = e.AdultForestC(cell_0).Resources;

                    e.PawnExtractAdultForestE(cell_0).Resources = extract;
                }
            }
        }
    }
}