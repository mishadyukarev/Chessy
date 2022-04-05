using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    public struct SyncEnvironmentVS
    {
        public void Run(in byte idx_0, in EntitiesViewGame vEs, in EntitiesModelGame e)
        {
            if (e.AdultForestC(idx_0).HaveAnyResources)
            {
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(true);

                vEs.EnvironmentVEs(idx_0).HillUnderC.SetActive(e.HillC(idx_0).HaveAnyResources);

                vEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SetActive(false);
            }
            else
            {
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(false);
                vEs.EnvironmentVEs(idx_0).HillUnderC.SetActive(false);
                vEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SetActive(e.HillC(idx_0).HaveAnyResources);
            }

            vEs.EnvironmentVE(idx_0, EnvironmentTypes.Fertilizer).SetActive(e.FertilizeC(idx_0).HaveAnyResources);
            vEs.EnvironmentVE(idx_0, EnvironmentTypes.YoungForest).SetActive(e.YoungForestC(idx_0).HaveAnyResources);
            vEs.EnvironmentVE(idx_0, EnvironmentTypes.Mountain).SetActive(e.MountainC(idx_0).HaveAnyResources);
        }
    }
}