namespace Chessy.Game
{
    sealed class EnvCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal EnvCellVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.EnvironmentEs(idx_0).AdultForestC.HaveAnyResources)
                {
                    VEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(true);

                    if (E.EnvironmentEs(idx_0).HillC.HaveAnyResources)
                    {
                        VEs.EnvironmentVEs(idx_0).HillUnderC.SetActive(true);
                    }
                }
                else
                {
                    VEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SetActive(false);
                    VEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SetActive(E.EnvironmentEs(idx_0).HillC.HaveAnyResources);
                }

                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Fertilizer).SetActive(E.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.YoungForest).SetActive(E.EnvironmentEs(idx_0).YoungForestC.HaveAnyResources);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Mountain).SetActive(E.EnvironmentEs(idx_0).MountainC.HaveAnyResources);
            }
        }
    }
}