using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                CellsForShiftCom.Clear(PlayerTypes.First, curIdxCell);
                CellsForShiftCom.Clear(PlayerTypes.Second, curIdxCell);

                ref var unitC_0 = ref _cellUnitFilter.Get1(curIdxCell);
                ref var stepUnitC_0 = ref _cellUnitFilter.Get2(curIdxCell);

                ref var effUnitC_0 = ref _cellUnitOthFilt.Get2(curIdxCell);
                ref var ownUnitC_0 = ref _cellUnitOthFilt.Get3(curIdxCell);


                if (unitC_0.HaveUnit)
                {
                    var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                    foreach (var xy1 in xyCellsAround)
                    {
                        var idxCell_1= _xyCellFilter.GetIdxCell(xy1);

                        ref var unitC_1 = ref _cellUnitFilter.Get1(idxCell_1);
                        ref var envC_1 = ref _cellEnvDataFilter.Get1(idxCell_1);


                        if (!envC_1.Have(EnvTypes.Mountain))
                        {
                            if (!unitC_1.HaveUnit)
                            {
                                if (stepUnitC_0.HaveStepsForDoing(envC_1) || stepUnitC_0.HaveMaxSteps(effUnitC_0, unitC_0.Unit))
                                {
                                    CellsForShiftCom.AddIdxCell(ownUnitC_0.Owner, curIdxCell, idxCell_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
