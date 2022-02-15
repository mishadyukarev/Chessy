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
                if (Es.AdultForestE(idx_0).CanExtractWoodcutter(Es.BuildEs(idx_0)))
                {
                    var extract = Es.AdultForestE(idx_0).AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));

                    Es.InventorResourcesEs.Resource(ResourceTypes.Wood, Es.BuildingE(idx_0).Owner).ResourceC.Add(extract);

                    Es.AdultForestE(idx_0).Take(extract);
                    if (!Es.AdultForestE(idx_0).HaveEnvironment) Es.TrailEs(idx_0).DestroyAll();

                    if (!Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        Es.BuildingE(idx_0).Destroy(Es);

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            Es.YoungForestE(idx_0).SetRandomResources();
                        }
                    }
                }
            }
        }
    }
}