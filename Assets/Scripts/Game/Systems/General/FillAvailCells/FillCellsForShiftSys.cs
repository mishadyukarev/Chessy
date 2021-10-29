using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                CellsForShiftCom.Clear(PlayerTypes.First, curIdxCell);
                CellsForShiftCom.Clear(PlayerTypes.Second, curIdxCell);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curStepUnitC = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get3(curIdxCell);


                if (curUnitDatCom.HaveUnit)
                {
                    var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                    foreach (var xy1 in xyCellsAround)
                    {
                        var idxCell_1= _xyCellFilter.GetIdxCell(xy1);

                        if (!_cellEnvDataFilter.Get1(idxCell_1).Have(EnvirTypes.Mountain))
                        {
                            if (!_cellUnitFilter.Get1(idxCell_1).HaveUnit)
                            {
                                if (curStepUnitC.AmountSteps >= _cellEnvDataFilter.Get1(idxCell_1).NeedAmountSteps
                                    || curStepUnitC.HaveMaxSteps(curUnitDatCom.UnitType))
                                {
                                    CellsForShiftCom.AddIdxCell(curOwnUnitCom.PlayerType, curIdxCell, idxCell_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
