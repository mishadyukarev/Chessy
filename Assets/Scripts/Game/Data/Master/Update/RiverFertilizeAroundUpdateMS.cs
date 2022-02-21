namespace Game.Game
{
    sealed class RiverFertilizeAroundUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal RiverFertilizeAroundUpdateMS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.RiverEs(idx_0).RiverTC.HaveRiverNear)
                {
                    if (!E.MountainC(idx_0).HaveAny)
                    {
                        E.FertilizeC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                    }
                }
            }
        }
    }
}