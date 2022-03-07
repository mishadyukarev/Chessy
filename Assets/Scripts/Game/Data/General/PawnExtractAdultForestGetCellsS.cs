namespace Chessy.Game.Systems.Model
{
    sealed class PawnExtractAdultForestGetCellsS : SystemAbstract, IEcsRunSystem
    {
        internal PawnExtractAdultForestGetCellsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.PawnExtractAdultForestE(idx_0).Resources = 0;

                if (E.AdultForestC(idx_0).HaveAnyResources)
                {
                    if (E.UnitTC(idx_0).Is(UnitTypes.Pawn) && E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) && !E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                    {
                        var extract = Environment_Values.EXTRACT_PAWM_ADULT_FOREST;

                        if (E.PlayerE(E.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
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
                                extract *= Environment_Values.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
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