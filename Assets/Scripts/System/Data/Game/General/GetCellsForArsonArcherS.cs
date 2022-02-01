namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEsWorker.Idxs)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                var unit_from = UnitEs(idx_0).MainE.UnitTC;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in CellEsWorker.GetIdxsAround(idx_0))
                        {
                            if (!EffectEs(idx_1).FireE.HaveFireC.Have)
                            {
                                if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
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