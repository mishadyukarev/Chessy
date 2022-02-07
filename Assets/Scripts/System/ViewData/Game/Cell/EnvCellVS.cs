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
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Fertilizer).SR.SetActive(Es.EnvironmentEs(idx_0).Fertilizer.HaveEnvironment);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.YoungForest).SR.SetActive(Es.EnvironmentEs(idx_0).YoungForest.HaveEnvironment);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.AdultForest).SR.SetActive(Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Hill).SR.SetActive(Es.EnvironmentEs(idx_0).Hill.HaveEnvironment);
                VEs.EnvironmentVE(idx_0, EnvironmentTypes.Mountain).SR.SetActive(Es.EnvironmentEs(idx_0).Mountain.HaveEnvironment);
            }
        }
    }
}