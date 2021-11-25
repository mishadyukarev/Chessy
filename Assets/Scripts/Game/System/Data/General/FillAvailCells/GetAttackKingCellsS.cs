using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetAttackKingCellsS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC, StunC> _effUnitF = default;

        
        public void Run()
        {
            ref EnvC Env(in byte idx) => ref EntityPool.EnvCellC<EnvC>(idx);


            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var level_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var stepUnit_0 = ref _statUnitF.Get1(idx_0);

                ref var effUnit_0 = ref _effUnitF.Get1(idx_0);
                ref var stunUnit_0 = ref _effUnitF.Get2(idx_0);


                if (!stunUnit_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.King))
                    {
                        DirectTypes curDir_1 = default;

                        CellSpaceC.TryGetXyAround(EntityPool.CellC<XyC>(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            curDir_1 += 1;

                            var idx_1 = EntityPool.IdxCell(item_1.Value);
   

                            ref var unit_1 = ref _unitF.Get1(idx_1);
                            ref var ownUnit_1 = ref _unitF.Get3(idx_1);

                            ref var env_1 = ref EntityPool.EnvCellC<EnvC>(idx_1);
                            ref var trail_1 = ref EntityPool.GetTrailCellC<TrailC>(idx_1);


                            if (!Env(idx_1).Have(EnvTypes.Mountain))
                            {
                                if (stepUnit_0.HaveStepsForDoing(Env(idx_1), item_1.Key, trail_1)
                                    || stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, level_0.Level, ownUnit_0.Owner)))
                                {
                                    if (unit_1.HaveUnit)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_0.Owner))
                                        {
                                            AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_1);
                                        }
                                    }
                                }
                            }
                        }
                    } }
            }
        }
    }
}
