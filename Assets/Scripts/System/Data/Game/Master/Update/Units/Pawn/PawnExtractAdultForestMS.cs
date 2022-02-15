namespace Game.Game
{
    sealed class PawnExtractAdultForestMS : SystemAbstract, IEcsRunSystem
    {
        public PawnExtractAdultForestMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.AdultForestE(idx_0).CanExtractPawn)
                {
                    Es.AdultForestE(idx_0).ExtractPawn(Es.UnitE(idx_0), Es.InventorResourcesEs);

                    if (Es.AdultForestE(idx_0).HaveEnvironment)
                    {
                        if (Es.BuildingE(idx_0).Is(BuildingTypes.Camp) || !Es.BuildingE(idx_0).HaveBuilding)
                        {
                            Es.BuildingE(idx_0).SetNew(BuildingTypes.Woodcutter, Es.UnitPlayerTC(idx_0).Player);
                        }

                        else if (!Es.BuildingE(idx_0).Is(BuildingTypes.Woodcutter))
                        {
                            Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        Es.BuildingE(idx_0).Destroy(Es);
                        Es.AdultForestE(idx_0).Destroy(Es.TrailEs(idx_0).Trails);

                        Es.YoungForestE(idx_0).SetRandomResources();
                    }
                }
            }
        }
    }
}