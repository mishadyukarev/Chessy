using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetShiftCellsS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<EffectsC, StunC> _effUnitF = default;

        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ShiftCellsC.Clear(PlayerTypes.First, idx_0);
                ShiftCellsC.Clear(PlayerTypes.Second, idx_0);

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var level_0 = ref _unitF.Get2(idx_0);
                ref var own_0 = ref _unitF.Get3(idx_0);

                ref var step_0 = ref _statUnitF.Get1(idx_0);

                ref var eff_0 = ref _effUnitF.Get1(idx_0);
                ref var stun_0 = ref _effUnitF.Get2(idx_0);

                

                if (!stun_0.IsStunned)
                {
                    if (unit_0.Have)
                    {
                        CellSpaceC.TryGetXyAround(EntityPool.Cell<XyC>(idx_0).Xy, out var directs);

                        foreach (var item_1 in directs)
                        {
                            var idx_1 = EntityPool.IdxCell(item_1.Value);

                            ref var trail_1 = ref EntityPool.Trail<TrailC>(idx_1);

                            ref var unitC_1 = ref _unitF.Get1(idx_1);
                            ref var envC_1 = ref _envF.Get1(idx_1);


                            if (!envC_1.Have(EnvTypes.Mountain))
                            {
                                if (!unitC_1.Have)
                                {
                                    if (EntityPool.UnitStat<UnitStatCellC>(idx_0).HaveStepsForDoing(item_1.Key)
                                        || EntityPool.UnitStat<UnitStatCellC>(idx_0).HaveMaxSteps)
                                    {
                                        ShiftCellsC.AddIdxCell(own_0.Owner, idx_0, idx_1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
