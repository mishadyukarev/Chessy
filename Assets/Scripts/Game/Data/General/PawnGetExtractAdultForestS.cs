﻿using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnGetExtractAdultForestS : SystemAbstract, IEcsRunSystem
    {
        internal PawnGetExtractAdultForestS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                E.PawnExtractAdultForestE(idx_0).Resources = 0;

                if (E.AdultForestC(idx_0).HaveAnyResources)
                {
                    if (E.UnitTC(idx_0).Is(UnitTypes.Pawn) && E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) && !E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                    {
                        var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                        if (E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                        {
                            if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                extract *= 2;
                            }
                        }



                        if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (E.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                            {
                                extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                            }
                        }

                        if (E.AdultForestC(idx_0).Resources < extract) extract = E.AdultForestC(idx_0).Resources;

                        E.PawnExtractAdultForestE(idx_0).Resources = extract;
                    }
                }
            }
        }
    }
}