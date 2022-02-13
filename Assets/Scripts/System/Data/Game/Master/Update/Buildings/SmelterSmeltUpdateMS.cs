namespace Game.Game
{
    sealed class SmelterSmeltUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal SmelterSmeltUpdateMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildingE(idx_0).HaveBuilding)
                {
                    if (Es.BuildingE(idx_0).IsActiveSmelter)
                    {
                        Es.InventorResourcesEs.Melt_Master(Es.BuildingE(idx_0).Owner);
                    }
                }
            }
        }
    }
}