using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetArsonCellsS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        private EcsFilter<StunC> _effUnitF = default;

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
            {
                var curXy = EntityDataPool.GetCellC<XyC>(idx_0).Xy;

                ref var unit_0 = ref EntityDataPool.GetUnitCellC<UnitC>(idx_0);
                ref var ownUnit_0 = ref EntityDataPool.GetUnitCellC<OwnerC>(idx_0);
                ref var stunUnit_0 = ref _effUnitF.Get1(idx_0);

                if (!stunUnit_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Archer))
                    {
                        foreach (var arouXy in CellSpace.GetXyAround(curXy))
                        {
                            var idx_1 = EntityDataPool.GetIdxCell(arouXy);

                            ref var env_1 = ref _envF.Get1(idx_1);

                            if (!_fireF.Get1(idx_1).Have)
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
