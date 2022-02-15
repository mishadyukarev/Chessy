namespace Game.Game
{
    sealed class UpdExtractFarmMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractFarmMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildingE(idx_0).CanExtractFertilizer(Es.EnvironmentEs(idx_0)))
                {
                    var extract = Es.FertilizeE(idx_0).AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));

                    Es.InventorResourcesEs.Resource(Es.FertilizeE(idx_0).Resource, Es.BuildingE(idx_0).Owner).ResourceC.Add(extract);

                    Es.FertilizeE(idx_0).Take(extract);

                    if (!Es.FertilizeE(idx_0).HaveEnvironment)
                    {
                        Es.BuildingE(idx_0).Destroy(Es);
                    }
                }
            }
        }
    }
}