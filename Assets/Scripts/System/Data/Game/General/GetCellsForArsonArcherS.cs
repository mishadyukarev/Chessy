using static Game.Game.CellFireE;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    public struct GetCellsForArsonArcherS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                ref var unit_from = ref Else(idx_0).UnitC;
                ref var ownUnit_from = ref CellUnitEs.Else(idx_0).OwnerC;
                ref var stun_from = ref CellUnitEs.Stun(idx_0).ForExitStun;

                if (!stun_from.Have)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            ref var fire_1 = ref CellFireEs.Fire(idx_1).Fire;

                            if (!fire_1.Have)
                            {
                                if (Environment(EnvironmentTypes.AdultForest, idx_1).Resources.Have)
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