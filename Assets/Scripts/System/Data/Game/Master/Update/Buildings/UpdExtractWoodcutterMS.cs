namespace Game.Game
{
    sealed class UpdExtractWoodcutterMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractWoodcutterMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.EnvAdultForestE(idx_0).CanExtractWoodcutter(BuildEs(idx_0)))
                {
                    EnvironmentEs(idx_0).AdultForest.ExtractWoodcutter(CellEs(idx_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

                    if (!EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        BuildEs(idx_0).BuildingE.Destroy();

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            EnvironmentEs(idx_0).YoungForest.SetRandomResources();
                        }
                    }
                }
            }
        }
    }
}