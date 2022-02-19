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
                if (Es.RiverEs(idx_0).RiverTC.HaveRiverNear)
                {
                    if (!Es.MountainC(idx_0).HaveAny)
                    {
                        Es.FertilizeC(idx_0).Resources++;
                    }
                }
            }
        }
    }
}