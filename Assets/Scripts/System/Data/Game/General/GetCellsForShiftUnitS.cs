namespace Game.Game
{
    sealed class GetCellsForShiftUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {         
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.First, idx_0).Clear();
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.Second, idx_0).Clear();

                if (Es.CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (!Es.UnitStunC(idx_0).IsStunned && Es.UnitTC(idx_0).HaveUnit && !Es.UnitTC(idx_0).IsAnimal)
                    {
                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!Es.EnvironmentEs(idx_1).Mountain.HaveEnvironment && !Es.UnitTC(idx_1).HaveUnit)
                            {
                                CellWorker.TryGetDirect(idx_0, idx_1, out var dir);

                                var one = Es.UnitE(idx_0).CanShift(dir, Es.CellEs(idx_1));
                                var two = Es.UnitStepC(idx_0).Have(CellUnitStatStepValues.StandartStepsUnit(Es.UnitTC(idx_0).Unit));

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.UnitPlayerTC(idx_0).Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}