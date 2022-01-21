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
                    if (CellUnitElseEs.Condition(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        var maxWater_0 = CellUnitWaterEs.MaxWater(idx_0);

                        CellUnitWaterEs.SetMaxWater(idx_0);

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (CellUnitEs.Unit(idx_1).Have && CellUnitElseEs.Owner(idx_0).Is(CellUnitElseEs.Owner(idx_1).Player))
                            {
                                CellUnitWaterEs.SetMaxWater(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}