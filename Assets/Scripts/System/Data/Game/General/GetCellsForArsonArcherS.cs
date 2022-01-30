namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                ref var unit_from = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var ownUnit_from = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var stun_from = ref Es.CellEs.UnitEs.Stun(idx_0).ForExitStun;

                if (!stun_from.Have)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                        {
                            ref var fire_1 = ref Es.CellEs.FireEs.Fire(idx_1).Fire;

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