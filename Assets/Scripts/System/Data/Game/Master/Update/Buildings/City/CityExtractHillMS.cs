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
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

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