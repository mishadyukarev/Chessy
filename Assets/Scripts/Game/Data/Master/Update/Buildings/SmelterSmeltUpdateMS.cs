namespace Game.Game
{
    sealed class SmelterSmeltUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal SmelterSmeltUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    if (E.IsActiveSmelter(idx_0))
                    {
                        //Es.InventorResourcesEs.Melt_Master(Es.BuildPlayerTC(idx_0).Player);
                    }
                }
            }
        }
    }
}