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
                if (Es.BuildE(idx_0).HaveBuilding)
                {
                    if (Es.BuildE(idx_0).IsActiveSmelterC.IsActive)
                    {
                        Es.InventorResourcesEs.Melt_Master(Es.BuildE(idx_0).OwnerC.Player);
                    }
                }
            }
        }
    }
}