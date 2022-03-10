using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnGetExtractAdultForestS : CellSystem, IEcsRunSystem
    {
        internal PawnGetExtractAdultForestS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.PawnExtractAdultForestE(Idx).Resources = 0;

            if (E.AdultForestC(Idx).HaveAnyResources)
            {
                if (E.UnitTC(Idx).Is(UnitTypes.Pawn) && E.UnitConditionTC(Idx).Is(ConditionUnitTypes.Relaxed) && !E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (E.PlayerInfoE(E.UnitPlayerTC(Idx).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                    {
                        if (E.UnitTC(Idx).Is(UnitTypes.Pawn))
                        {
                            extract *= 2;
                        }
                    }



                    if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Axe))
                    {
                        if (E.UnitMainTWLevelTC(Idx).Is(LevelTypes.Second))
                        {
                            extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (E.AdultForestC(Idx).Resources < extract) extract = E.AdultForestC(Idx).Resources;

                    E.PawnExtractAdultForestE(Idx).Resources = extract;
                }
            }
        }
    }
}