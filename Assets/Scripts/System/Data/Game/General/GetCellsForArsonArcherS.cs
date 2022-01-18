using static Game.Game.CellFireEs;
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

                ref var unit_from = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_from = ref Unit<PlayerTC>(idx_0);
                ref var stun_from = ref CellUnitStunEs.StepsForExitStun<AmountC>(idx_0);

                if (!stun_from.Have)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in CellSpaceC.IdxAround(idx_0))
                        {
                            ref var fire_1 = ref Fire<HaveEffectC>(idx_1);

                            if (!fire_1.Have)
                            {
                                if (Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx_1).Have)
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