namespace Game.Game
{
    sealed class FarmExtractFertilizeUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal FarmExtractFertilizeUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.FarmExtractFertilizeE(idx_0).ResourcesC.HaveAnyResources)
                {
                    var extract = E.FarmExtractFertilizeE(idx_0).ResourcesC.Resources;

                    E.ResourcesC(E.BuildPlayerTC(idx_0).Player, ResourceTypes.Food).Resources += extract;
                    E.FertilizeC(idx_0).Resources -= extract;

                    if (!E.FertilizeC(idx_0).HaveAnyResources)
                    {
                        E.BuildTC(idx_0).Building = BuildingTypes.None;
                    }
                }
            }
        }
    }
}