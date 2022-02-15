namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                if (!Es.UnitStunC(idx_0).IsStunned)
                {
                    if (Es.UnitTC(idx_0).HaveUnit && Es.ExtraTWE(idx_0).ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
                    {
                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!Es.EffectEs(idx_1).FireE.HaveFireC.Have)
                            {
                                if (Es.EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                {
                                    CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}