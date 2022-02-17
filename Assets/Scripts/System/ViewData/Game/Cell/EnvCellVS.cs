namespace Game.Game
{
    sealed class EnvCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal EnvCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Fertilizer).SR.SetActive(Es.EnvironmentEs(idx_0).FertilizeC.HaveAny);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.YoungForest).SR.SetActive(Es.EnvironmentEs(idx_0).YoungForestC.HaveAny);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SR.SetActive(Es.EnvironmentEs(idx_0).AdultForestC.HaveAny);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SR.SetActive(Es.EnvironmentEs(idx_0).HillC.HaveAny);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Mountain).SR.SetActive(Es.EnvironmentEs(idx_0).MountainC.HaveAny);
            }
        }
    }
}