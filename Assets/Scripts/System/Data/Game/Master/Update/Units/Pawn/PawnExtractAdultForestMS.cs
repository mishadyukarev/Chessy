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
                if (Es.EnvAdultForestE(idx_0).CanExtractPawn(UnitEs(idx_0)))
                {
                    Es.EnvAdultForestE(idx_0).ExtractPawn(UnitEs(idx_0), Es.InventorResourcesEs);

                    if (Es.EnvAdultForestE(idx_0).HaveEnvironment)
                    {
                        if (Es.BuildE(idx_0).Is(BuildingTypes.Camp) || !Es.BuildE(idx_0).BuildTC.Have)
                        {
                            Es.BuildE(idx_0).SetNew(BuildingTypes.Woodcutter, Es.UnitE(idx_0).Owner);
                        }

                        else if (!Es.BuildE(idx_0).Is(BuildingTypes.Woodcutter))
                        {
                            Es.UnitE(idx_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        Es.BuildE(idx_0).Destroy(Es);
                        Es.EnvAdultForestE(idx_0).Destroy(TrailEs(idx_0).Trails);

                        Es.EnvYoungForestE(idx_0).SetRandomResources();
                    }
                }
            }
        }
    }
}