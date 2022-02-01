namespace Game.Game
{
    sealed class GetCellsForShiftUnitS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForShiftUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.First, idx_0).Clear();
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.Second, idx_0).Clear();

                if (CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                {
                    if (!UnitEs.Stun(idx_0).IsStunned && UnitEs.Main(idx_0).HaveUnit(UnitEs.StatEs) && !UnitEs.Main(idx_0).UnitTC.IsAnimal)
                    {
                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                        {
                            if (!EnvironmentEs.Mountain(idx_1).HaveEnvironment && !UnitEs.Main(idx_1).HaveUnit(UnitEs.StatEs))
                            {
                                var one = UnitEs.StatEs.Step(idx_0).Steps.Amount >= UnitEs.Main(idx_1).StepsForShiftOrAttack(CellEs.GetDirect(idx_0, idx_1), EnvironmentEs, TrailEs);
                                var two = UnitEs.StatEs.Step(idx_0).HaveMax(UnitEs.Main(idx_0));

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(UnitEs.Main(idx_0).OwnerC.Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}