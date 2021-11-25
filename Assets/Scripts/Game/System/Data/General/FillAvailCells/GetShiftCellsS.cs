﻿using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetShiftCellsS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC, StunC> _effUnitF = default;

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
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
                    if (unit_0.HaveUnit)
                    {
                        CellSpace.TryGetXyAround(EntityDataPool.GetCellC<XyC>(idx_0).Xy, out var directs);

                        foreach (var item_1 in directs)
                        {
                            var idx_1 = EntityDataPool.GetIdxCell(item_1.Value);

                            ref var trail_1 = ref EntityDataPool.GetTrailCellC<TrailC>(idx_1);

                            ref var unitC_1 = ref _unitF.Get1(idx_1);
                            ref var envC_1 = ref _envF.Get1(idx_1);


                            if (!envC_1.Have(EnvTypes.Mountain))
                            {
                                if (!unitC_1.HaveUnit)
                                {
                                    if (step_0.HaveStepsForDoing(envC_1, item_1.Key, trail_1)
                                        || step_0.HaveMaxSteps(unit_0.Unit, eff_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, level_0.Level, own_0.Owner)))
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
