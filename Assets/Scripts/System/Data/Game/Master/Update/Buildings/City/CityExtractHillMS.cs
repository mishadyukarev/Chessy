namespace Game.Game
{
    sealed class CityExtractHillMS : SystemAbstract, IEcsRunSystem
    {
        internal CityExtractHillMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildE(idx_0).Is(BuildingTypes.City))
                {
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.EnvHillE(idx_1).HaveEnvironment)
                        {
                            Es.EnvHillE(idx_1).ExtractCity(Es.CellEs(idx_0), Es.InventorResourcesEs);
                        }
                    }
                }
            }
        }
    }
}