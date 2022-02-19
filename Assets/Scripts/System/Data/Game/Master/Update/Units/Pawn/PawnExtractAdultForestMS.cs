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
                if (Es.UnitEs(idx_0).ExtractPawnE.CanExtract)
                {
                    Es.AdultForestC(idx_0).Resources -= Es.UnitEs(idx_0).ExtractPawnE.ResourcesC.Resources;
                    Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).ResourcesC(ResourceTypes.Wood).Resources += Es.UnitExtractPawnE(idx_0).ResourcesC.Resources;

                    //Es.AdultForestE(idx_0).ExtractPawn(Es.UnitE(idx_0), Es.InventorResourcesEs);

                    //var extract = AmountExtractPawn(unitE);

                    //invResEs.Resource(Resource, unitE.PlayerTC.Player).ResourceC.Add(extract);
                    //Take(extract);


                    if (Es.AdultForestC(idx_0).HaveAny)
                    {
                        if (Es.BuildTC(idx_0).Is(BuildingTypes.Camp) || !Es.BuildTC(idx_0).HaveBuilding)
                        {
                            Es.BuildE(idx_0).Set(BuildingTypes.Woodcutter, LevelTypes.First, 1, Es.UnitPlayerTC(idx_0).Player);
                        }

                        else if (!Es.BuildTC(idx_0).Is(BuildingTypes.Woodcutter))
                        {
                            Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        Es.BuildTC(idx_0).Build = BuildingTypes.None;

                        //Es.AdultForestC(idx_0).Destroy(Es.TrailEs(idx_0).Trails);

                        //Es.YoungForestC(idx_0).SetRandomResources();
                    }
                }
            }
        }
    }
}