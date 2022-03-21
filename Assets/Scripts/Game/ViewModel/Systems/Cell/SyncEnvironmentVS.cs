namespace Chessy.Game
{
    sealed class SyncEnvironmentVS
    {
        public static void Run(in byte idx_0, in EntitiesView vEs, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            if (e.EnvironmentEs(idx_0).AdultForestC.HaveAnyResources)
            {
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(true);

                if (e.EnvironmentEs(idx_0).HillC.HaveAnyResources)
                {
                    vEs.EnvironmentVEs(idx_0).HillUnderC.SetActive(true);
                }
            }
            else
            {
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(false);
                vEs.EnvironmentVEs(idx_0).HillUnderC.SetActive(false);
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SetActive(e.EnvironmentEs(idx_0).HillC.HaveAnyResources);
            }

            vEs.EnvironmentVE(idx_0, EnvironmentTypes.Fertilizer).SetActive(e.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources);
            vEs.EnvironmentVE(idx_0, EnvironmentTypes.YoungForest).SetActive(e.EnvironmentEs(idx_0).YoungForestC.HaveAnyResources);
            vEs.EnvironmentVE(idx_0, EnvironmentTypes.Mountain).SetActive(e.EnvironmentEs(idx_0).MountainC.HaveAnyResources);
        }
    }
}