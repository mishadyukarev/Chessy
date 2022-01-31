namespace Game.Game
{
    sealed class EnvCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public EnvCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx in CellEs.Idxs)
            {
                CellEnvVEs.EnvCellVC<SpriteRendererVC>(EnvironmentTypes.Fertilizer, idx).SetActive(CellEs.EnvironmentEs.Fertilizer(idx).HaveEnvironment);
                CellEnvVEs.EnvCellVC<SpriteRendererVC>(EnvironmentTypes.YoungForest, idx).SetActive(CellEs.EnvironmentEs.YoungForest(idx).HaveEnvironment);
                CellEnvVEs.EnvCellVC<SpriteRendererVC>(EnvironmentTypes.AdultForest, idx).SetActive(CellEs.EnvironmentEs.AdultForest(idx).HaveEnvironment);
                CellEnvVEs.EnvCellVC<SpriteRendererVC>(EnvironmentTypes.Hill, idx).SetActive(CellEs.EnvironmentEs.Hill(idx).HaveEnvironment);
                CellEnvVEs.EnvCellVC<SpriteRendererVC>(EnvironmentTypes.Mountain, idx).SetActive(CellEs.EnvironmentEs.Mountain(idx).HaveEnvironment);
            }
        }
    }
}
