using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class GetAttackKingCellsS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<EffectsC, StunC> _effUnitF = default;

        
        public void Run()
        {
            ref EnvC Env(in byte idx) => ref EntityPool.Environment<EnvC>(idx);


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

                        CellSpaceC.TryGetXyAround(EntityPool.Cell<XyC>(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            curDir_1 += 1;

                            var idx_1 = EntityPool.IdxCell(item_1.Value);
   

                            ref var unit_1 = ref _unitF.Get1(idx_1);
                            ref var ownUnit_1 = ref _unitF.Get3(idx_1);

                            ref var env_1 = ref EntityPool.Environment<EnvC>(idx_1);
                            ref var trail_1 = ref EntityPool.Trail<TrailC>(idx_1);


                            if (!Env(idx_1).Have(EnvTypes.Mountain))
                            {
                                if (Unit<StepUnitC>(idx_0).HaveStepsForDoing(item_1.Key)
                                    || Unit<StepUnitC>(idx_0).HaveMaxSteps)
                                {
                                    if (unit_1.Have)
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
