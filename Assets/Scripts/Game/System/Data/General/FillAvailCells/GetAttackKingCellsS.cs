using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GetAttackKingCellsS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<TrailC> _trailF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC, StunC> _effUnitF = default;



        public void Run()
        {
            foreach (byte idx_0 in _xyF)
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

                        CellSpace.TryGetXyAround(_xyF.Get1(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            curDir_1 += 1;

                            var idx_1 = _xyF.GetIdxCell(item_1.Value);
   

                            ref var unit_1 = ref _unitF.Get1(idx_1);
                            ref var ownUnit_1 = ref _unitF.Get3(idx_1);

                            ref var env_1 = ref _envF.Get1(idx_1);
                            ref var trail_1 = ref _trailF.Get1(idx_1);


                            if (!env_1.Have(EnvTypes.Mountain))
                            {
                                if (stepUnit_0.HaveStepsForDoing(env_1, item_1.Key, trail_1)
                                    || stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, level_0.Level, ownUnit_0.Owner)))
                                {
                                    if (unit_1.HaveUnit)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_0.Owner))
                                        {
                                            AttackCellsC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_1);
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
