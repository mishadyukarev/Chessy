using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetArsonCellsS : IEcsRunSystem
    {
        public void Run()
        {
            foreach(byte idx_0 in EntityCellPool.Idxs)
            {
                var curXy = EntityCellPool.Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref EntityCellPool.Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref EntityCellPool.Unit<OwnerC>(idx_0);
                ref var stun_0 = ref EntityCellPool.Unit<StunC>(idx_0);

                if (!stun_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Archer))
                    {
                        foreach (var xy_1 in CellSpaceC.XyAround(curXy))
                        {
                            var idx_1 = EntityCellPool.IdxCell(xy_1);

                            ref var env_1 = ref EntityCellPool.Environment<EnvC>(idx_1);
                            ref var fire_1 = ref EntityCellPool.Fire<HaveEffectC>(idx_1);

                            if (!fire_1.Have)
                            {
                                if (env_1.Have(EnvTypes.AdultForest))
                                {
                                    ArsonCellsC.Add(ownUnit_0.Owner, idx_0, idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
