namespace Game.Game
{
    sealed class UpdFertilizeAroundRiverMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdFertilizeAroundRiverMS(in Entities ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.RiverEs(idx_0).RiverE.HaveRiverNear)
                {
                    if (!Es.MountainE(idx_0).HaveEnvironment)
                    {
                        Es.FertilizeE(idx_0).AddFromNearRiver();
                    }
                }
            }
        }
    }
}