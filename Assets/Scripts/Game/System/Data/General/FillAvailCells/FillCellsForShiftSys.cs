using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;

        private EcsFilter<CellUnitDataC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyCellFilter)
            {
                CellsForShiftCom.Clear(PlayerTypes.First, idx_0);
                CellsForShiftCom.Clear(PlayerTypes.Second, idx_0);

                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var stepUnitC_0 = ref _cellUnitFilter.Get2(idx_0);

                ref var effUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitOthFilt.Get3(idx_0);


                if (unit_0.HaveUnit)
                {
                    CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idx_0).XyCell, out var directs);

                    foreach (var item_1 in directs)
                    {
                        var idx_1= _xyCellFilter.GetIdxCell(item_1.Value);

                        ref var trail_1 = ref _cellTrailFilt.Get1(idx_1);

                        ref var unitC_1 = ref _cellUnitFilter.Get1(idx_1);
                        ref var envC_1 = ref _cellEnvDataFilter.Get1(idx_1);


                        if (!envC_1.Have(EnvTypes.Mountain))
                        {
                            if (!unitC_1.HaveUnit)
                            {
                                if (stepUnitC_0.HaveStepsForDoing(envC_1, item_1.Key, trail_1) 
                                    || stepUnitC_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                                {
                                    CellsForShiftCom.AddIdxCell(ownUnit_0.Owner, idx_0, idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
