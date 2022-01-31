namespace Game.Game
{
    sealed class UpdExtractWoodcutterMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractWoodcutterMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < CellEs.Count; idx_0++)
            {
                if (BuildEs.BuildingE(idx_0).CanExtractAdultForest(BuildEs, EnvironmentEs))
                {
                    EnvironmentEs.AdultForest(idx_0).ExtractWoodcutter(CellEs, Es.BuildingUpgradeEs, Es.InventorResourcesEs);

                    if (!EnvironmentEs.AdultForest(idx_0).HaveEnvironment)
                    {
                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            EnvironmentEs.YoungForest(idx_0).SetNew(Es.WhereEnviromentEs);
                        }
                    }
                }
            }
        }
    }
}