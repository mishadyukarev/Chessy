using Chessy.Game.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    public struct PawnGetExtractAdultForestS
    {
        public PawnGetExtractAdultForestS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.PawnExtractAdultForestE(idx_0).Resources = 0;

            if (e.AdultForestC(idx_0).HaveAnyResources)
            {
                if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) && !e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                    {
                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            extract *= 2;
                        }
                    }



                    if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                    {
                        if (e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                        {
                            extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (e.AdultForestC(idx_0).Resources < extract) extract = e.AdultForestC(idx_0).Resources;

                    e.PawnExtractAdultForestE(idx_0).Resources = extract;
                }
            }
        }
    }
}