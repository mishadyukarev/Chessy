namespace Game.Game
{
    sealed class EnvCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public EnvCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellWorker.Idxs)
            {
                CellVEs(idx_0).EnvironmentVEs.SR(EnvironmentTypes.Fertilizer).SetActive(EnvironmentEs(idx_0).Fertilizer.HaveEnvironment);
                CellVEs(idx_0).EnvironmentVEs.SR(EnvironmentTypes.YoungForest).SetActive(EnvironmentEs(idx_0).YoungForest.HaveEnvironment);
                CellVEs(idx_0).EnvironmentVEs.SR(EnvironmentTypes.AdultForest).SetActive(EnvironmentEs(idx_0).AdultForest.HaveEnvironment);
                CellVEs(idx_0).EnvironmentVEs.SR(EnvironmentTypes.Hill).SetActive(EnvironmentEs(idx_0).Hill.HaveEnvironment);
                CellVEs(idx_0).EnvironmentVEs.SR(EnvironmentTypes.Mountain).SetActive(EnvironmentEs(idx_0).Mountain.HaveEnvironment);
            }
        }
    }
}
