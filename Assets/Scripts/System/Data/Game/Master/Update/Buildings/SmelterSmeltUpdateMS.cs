namespace Game.Game
{
    sealed class SmelterSmeltUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal SmelterSmeltUpdateMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildTC(idx_0).HaveBuilding)
                {
                    if (E.BuildSmelterTC(idx_0))
                    {
                        //Es.InventorResourcesEs.Melt_Master(Es.BuildPlayerTC(idx_0).Player);
                    }
                }
            }
        }
    }
}