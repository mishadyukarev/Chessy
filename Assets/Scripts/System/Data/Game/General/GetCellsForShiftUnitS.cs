namespace Game.Game
{
    public struct GetCellsForShiftUnitS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.First, idx_0).Clear();
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.Second, idx_0).Clear();

                if (Entities.CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                {
                    if (!Entities.CellEs.UnitEs.Stun(idx_0).ForExitStun.Have && Entities.CellEs.UnitEs.Else(idx_0).UnitC.Have && !Entities.CellEs.UnitEs.Else(idx_0).UnitC.IsAnimal)
                    {
                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_1).Resources.Have && !Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have)
                            {
                                var one = Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= Entities.CellEs.UnitEs.Step(idx_1).StepsForShiftOrAttack(CellSpaceSupport.GetDirect(idx_0, idx_1), Entities.CellEs.EnvironmentEs.Environments(idx_1), Entities.CellEs.TrailEs.Trails(idx_1));
                                var two = Entities.CellEs.UnitEs.Step(idx_0).HaveMax(Entities.CellEs.UnitEs.Else(idx_0));

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(Entities.CellEs.UnitEs.Else(idx_0).OwnerC.Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}