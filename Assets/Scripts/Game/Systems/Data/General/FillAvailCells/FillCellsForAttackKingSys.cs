using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class FillCellsForAttackKingSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var unitC_0 = ref _cellUnitFilter.Get1(curIdxCell);
                ref var stepUnitC_0 = ref _cellUnitFilter.Get2(curIdxCell);

                ref var effUnitC_0 = ref _cellUnitOthFilt.Get2(curIdxCell);
                ref var ownUnitC_0 = ref _cellUnitOthFilt.Get3(curIdxCell);

                if (unitC_0.Is(UnitTypes.King))
                {
                    DirectTypes curDir_1 = default;

                    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                    {
                        curDir_1 += 1;

                        var idx_1 = _xyCellFilter.GetIdxCell(xy1);

                        ref var envrDatC_1 = ref _cellEnvDataFilter.Get1(idx_1);
                        ref var unitDatC_1 = ref _cellUnitFilter.Get1(idx_1);

                        ref var effUnitC_1 = ref _cellUnitOthFilt.Get2(idx_1);
                        ref var ownUnitC_1 = ref _cellUnitOthFilt.Get3(idx_1);

                        if (!envrDatC_1.Have(EnvTypes.Mountain))
                        {
                            if (stepUnitC_0.HaveStepsForDoing(envrDatC_1) || stepUnitC_0.HaveMaxSteps(effUnitC_0, unitC_0.Unit))
                            {
                                if (unitDatC_1.HaveUnit)
                                {
                                    if (!ownUnitC_1.Is(ownUnitC_0.Owner))
                                    {
                                        CellsAttackC.Add(ownUnitC_0.Owner, AttackTypes.Simple, curIdxCell, idx_1);
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
