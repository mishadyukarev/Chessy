namespace Game.Game
{
    public struct UpdateRelaxSnowyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                if (CellUnitEntities.Else(idx_0).UnitC.Is(UnitTypes.Snowy))
                {
                    if (CellUnitEntities.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                    {
                        var maxWater_0 = CellUnitEntities.MaxWater(idx_0);

                        CellUnitEntities.Water(idx_0).AmountC.Amount = CellUnitEntities.MaxWater(idx_0);

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (CellUnitEntities.Else(idx_1).UnitC.Have && CellUnitEntities.Else(idx_0).OwnerC.Is(CellUnitEntities.Else(idx_1).OwnerC.Player))
                            {
                                CellUnitEntities.Water(idx_1).AmountC.Amount = CellUnitEntities.MaxWater(idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}