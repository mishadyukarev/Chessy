namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                var unit_from = UnitEs.Main(idx_0).UnitTC;

                if (!UnitEs.Stun(idx_0).IsStunned)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                        {
                            ref var fire_1 = ref CellEs.FireEs.Fire(idx_1).Fire;

                            if (!fire_1.Have)
                            {
                                if (EnvironmentEs.AdultForest(idx_1).HaveEnvironment)
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