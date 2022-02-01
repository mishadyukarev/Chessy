namespace Game.Game
{
    sealed class GetCellsForShiftUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEsWorker.Idxs)
            {
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.First, idx_0).Clear();
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.Second, idx_0).Clear();

                if (CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (!UnitEffectEs(idx_0).StunE.IsStunned && UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && !UnitEs(idx_0).MainE.UnitTC.IsAnimal)
                    {
                        foreach (var idx_1 in CellEsWorker.GetIdxsAround(idx_0))
                        {
                            if (!EnvironmentEs(idx_1).Mountain.HaveEnvironment && !UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                            {
                                var one = UnitStatEs(idx_0).StepE.Steps.Amount >= UnitEs(idx_1).MainE.StepsForShiftOrAttack(CellEsWorker.GetDirect(idx_0, idx_1), EnvironmentEs(idx_1), TrailEs(idx_1));
                                var two = UnitStatEs(idx_0).StepE.HaveMax(UnitEs(idx_0).MainE);

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(UnitEs(idx_0).MainE.OwnerC.Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}