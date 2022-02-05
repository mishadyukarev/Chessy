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
                if (Es.BuildE(idx_0).CanExtractFertilizer(EnvironmentEs(idx_0)))
                {
                    Es.EnvFertilizeE(idx_0).ExtractFarm(CellEs(idx_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

                    //if (!EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                    //{
                    //    BuildEs(idx_0).BuildingE.Destroy();
                    //}
                }
            }
        }
    }
}