namespace Game.Game
{
    public struct UpdateRelaxSnowyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                if (Entities.CellEs.UnitEs.Else(idx_0).UnitC.Is(UnitTypes.Snowy))
                {
                    if (Entities.CellEs.UnitEs.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                    {
                        var maxWater_0 = Entities.CellEs.UnitEs.MaxWater(idx_0);

                        Entities.CellEs.UnitEs.Water(idx_0).AmountC.Amount = Entities.CellEs.UnitEs.MaxWater(idx_0);

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have && Entities.CellEs.UnitEs.Else(idx_0).OwnerC.Is(Entities.CellEs.UnitEs.Else(idx_1).OwnerC.Player))
                            {
                                Entities.CellEs.UnitEs.Water(idx_1).AmountC.Amount = Entities.CellEs.UnitEs.MaxWater(idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}