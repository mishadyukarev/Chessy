namespace Game.Game
{
    sealed class PawnExtractAdultForestMS : SystemAbstract, IEcsRunSystem
    {
        public PawnExtractAdultForestMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.PawnExtractAdultForestE(idx_0).HaveAny)
                {
                    var extract = E.PawnExtractAdultForestE(idx_0).Resources;

                    E.AdultForestC(idx_0).Resources -= extract;
                    E.PlayerE(E.UnitPlayerTC(idx_0).Player).ResourcesC(ResourceTypes.Wood).Resources += extract;


                    if (E.AdultForestC(idx_0).HaveAny)
                    {
                        if (E.BuildTC(idx_0).Is(BuildingTypes.Camp) || !E.BuildTC(idx_0).HaveBuilding)
                        {
                            E.BuildE(idx_0).Set(BuildingTypes.Woodcutter, LevelTypes.First, 1, E.UnitPlayerTC(idx_0).Player);
                        }

                        else if (!E.BuildTC(idx_0).Is(BuildingTypes.Woodcutter))
                        {
                            E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        E.BuildTC(idx_0).Build = BuildingTypes.None;

                        E.YoungForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                    }
                }
                else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed)
                    && E.UnitHpC(idx_0).Health >= CellUnitStatHp_Values.MAX_HP)
                {
                    E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                }
            }
        }
    }
}