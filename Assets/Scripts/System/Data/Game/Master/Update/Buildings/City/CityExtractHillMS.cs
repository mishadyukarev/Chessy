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
                if (Es.BuildTC(idx_0).Is(BuildingTypes.City))
                {
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.HillC(idx_1).HaveAny)
                        {
                            //var extract = AmountExtractCity();

                            //invResEs.Resource(Resource, cellEs_from.BuildEs.BuildingE.Owner).ResourceC.Add(extract);
                            //Take(extract);
                        }
                    }
                }
            }
        }
    }
}