namespace Game.Game
{
    public sealed class UpdExtractFarmMS : SystemAbstract, IEcsRunSystem
    {
        public UpdExtractFarmMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (BuildEs(idx_0).BuildingE.CanExtractFertilizer(EnvironmentEs(idx_0)))
                {
                    EnvironmentEs(idx_0).Fertilizer.ExtractFarm(CellEs(idx_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

                    if (!EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                    {
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
                    }
                }
            }
        }
    }
}