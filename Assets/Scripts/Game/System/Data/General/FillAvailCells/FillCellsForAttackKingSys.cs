using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsForAttackKingSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;

        private EcsFilter<CellUnitDataC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, UnitEffectsC, OwnerC> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(curIdxCell);
                ref var stepUnit_0 = ref _cellUnitFilter.Get2(curIdxCell);

                ref var effUnit_0 = ref _cellUnitOthFilt.Get2(curIdxCell);
                ref var ownUnit_0 = ref _cellUnitOthFilt.Get3(curIdxCell);

                if (unit_0.Is(UnitTypes.King))
                {
                    DirectTypes curDir_1 = default;

                    CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(curIdxCell).XyCell, out var dirs);

                    foreach (var item_1 in dirs)
                    {
                        curDir_1 += 1;

                        var idx_1 = _xyCellFilter.GetIdxCell(item_1.Value);

                        ref var env_1 = ref _cellEnvDataFilter.Get1(idx_1);
                        ref var unit_1 = ref _cellUnitFilter.Get1(idx_1);

                        ref var effUnit_1 = ref _cellUnitOthFilt.Get2(idx_1);
                        ref var ownUnit_1 = ref _cellUnitOthFilt.Get3(idx_1);

                        ref var trail_1 = ref _cellTrailFilt.Get1(idx_1);

                        if (!env_1.Have(EnvTypes.Mountain))
                        {
                            if (stepUnit_0.HaveStepsForDoing(env_1, item_1.Key, trail_1) 
                                || stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                            {
                                if (unit_1.HaveUnit)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Owner))
                                    {
                                        CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, curIdxCell, idx_1);
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
