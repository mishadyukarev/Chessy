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
                //if (Es.BuildTC(idx_0).CanExtractFertilizer(Es.EnvironmentEs(idx_0)))
                //{
                //    //var extract = Es.FertilizeC(idx_0).AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));

                //    //Es.InventorResourcesEs.Resource(ResourceTypes.Food, Es.BuildingE(idx_0).Owner).ResourceC.Add(extract);

                //    //Es.FertilizeC(idx_0).Take(extract);

                //    //if (!Es.FertilizeC(idx_0).HaveAny)
                //    //{
                //    //    Es.BuildingE(idx_0).Destroy(Es);
                //    //}
                //}
            }
        }
    }
}