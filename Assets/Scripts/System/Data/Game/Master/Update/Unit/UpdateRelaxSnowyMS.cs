namespace Game.Game
{
    public struct UpdateRelaxSnowyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                if (CellUnitEs.Unit(idx_0).Is(UnitTypes.Snowy))
                {
                    if (EntitiesPool.UnitElse.Condition(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        var maxWater_0 = EntitiesPool.UnitWaters[idx_0].MaxWater;

                        EntitiesPool.UnitWaters[idx_0].SetMaxWater();

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (CellUnitEs.Unit(idx_1).Have && EntitiesPool.UnitElse.Owner(idx_0).Is(EntitiesPool.UnitElse.Owner(idx_1).Player))
                            {
                                EntitiesPool.UnitWaters[idx_1].SetMaxWater();
                            }
                        }
                    }
                }
            }
        }
    }
}