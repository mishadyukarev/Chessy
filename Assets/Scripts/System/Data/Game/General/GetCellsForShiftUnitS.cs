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
                    if (!Es.UnitE(idx_0).IsStunned && Es.UnitEs(idx_0).UnitE.HaveUnit && !Es.UnitE(idx_0).IsAnimal)
                    {
                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!Es.EnvironmentEs(idx_1).Mountain.HaveEnvironment && !UnitEs(idx_1).UnitE.HaveUnit)
                            {
                                CellWorker.TryGetDirect(idx_0, idx_1, out var dir);

                                var one = Es.UnitE(idx_0).CanShift(Es.UnitE(idx_0).Unit, dir, CellEs(idx_1));
                                var two = Es.UnitE(idx_0).HaveMaxSteps;

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.UnitE(idx_0).Owner, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}