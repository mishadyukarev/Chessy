using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetAttackPawnCellsS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC, StunC> _effUnitF = default;

        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var level_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var step_0 = ref _statUnitF.Get1(idx_0);

                ref var effUnit_0 = ref _effUnitF.Get1(idx_0); 
                ref var stunUnit_0 = ref _effUnitF.Get2(idx_0);

                if (!stunUnit_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Pawn))
                    {
                        DirectTypes dir_cur = default;

                        CellSpaceC.TryGetXyAround(EntityPool.Cell<XyC>(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = EntityPool.IdxCell(item_1.Value);

                            ref var env_1 = ref _envF.Get1(idx_1);
                            ref var unit_1 = ref _unitF.Get1(idx_1);
                            ref var own_1 = ref _unitF.Get3(idx_1);

                            ref var trail_1 = ref EntityPool.Trail<TrailC>(idx_1);


                            if (!env_1.Have(EnvTypes.Mountain))
                            {
                                if (step_0.HaveStepsForDoing(env_1, item_1.Key, trail_1)
                                    || step_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, level_0.Level, ownUnit_0.Owner)))
                                {
                                    if (unit_1.HaveUnit)
                                    {
                                        if (!own_1.Is(ownUnit_0.Owner))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                                || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_1);
                                            }
                                            else AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_1);
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
}
