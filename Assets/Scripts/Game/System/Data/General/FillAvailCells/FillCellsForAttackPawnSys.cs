using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<TrailC> _trailF = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC, StunC> _effUnitF = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var ownUnit_0 = ref _unitF.Get2(idx_0);

                ref var step_0 = ref _statUnitF.Get1(idx_0);

                ref var effUnit_0 = ref _effUnitF.Get1(idx_0); 
                ref var stunUnit_0 = ref _effUnitF.Get2(idx_0);

                if (!stunUnit_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Pawn))
                    {
                        DirectTypes curDurect1 = default;

                        CellSpace.TryGetXyAround(_xyF.Get1(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            curDurect1 += 1;
                            var idx_1 = _xyF.GetIdxCell(item_1.Value);

                            ref var envC_1 = ref _envF.Get1(idx_1);
                            ref var unitC_1 = ref _unitF.Get1(idx_1);
                            ref var ownUnitC_1 = ref _unitF.Get2(idx_1);

                            ref var trail_1 = ref _trailF.Get1(idx_1);


                            if (!envC_1.Have(EnvTypes.Mountain))
                            {
                                if (step_0.HaveStepsForDoing(envC_1, item_1.Key, trail_1)
                                    || step_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                                {
                                    if (unitC_1.HaveUnit)
                                    {
                                        if (!ownUnitC_1.Is(ownUnit_0.Owner))
                                        {
                                            if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                                || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                            {
                                                CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_1);
                                            }
                                            else CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Unique, idx_0, idx_1);
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
