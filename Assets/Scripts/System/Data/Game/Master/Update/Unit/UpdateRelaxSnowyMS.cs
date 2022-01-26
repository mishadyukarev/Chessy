namespace Game.Game
{
    public struct UpdateRelaxSnowyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                if (CellUnitEs.Else(idx_0).UnitC.Is(UnitTypes.Snowy))
                {
                    if (CellUnitEs.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                    {
                        var maxWater_0 = CellUnitEs.MaxWater(idx_0);

                        CellUnitEs.Water(idx_0).AmountC.Amount = CellUnitEs.MaxWater(idx_0);

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (CellUnitEs.Else(idx_1).UnitC.Have && CellUnitEs.Else(idx_0).OwnerC.Is(CellUnitEs.Else(idx_1).OwnerC.Player))
                            {
                                CellUnitEs.Water(idx_1).AmountC.Amount = CellUnitEs.MaxWater(idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}