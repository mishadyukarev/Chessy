using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForAttackKingSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curStepUnitC = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                if (curUnitDatCom.Is(UnitTypes.King))
                {
                    DirectTypes curDurect1 = default;

                    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                    {
                        curDurect1 += 1;
                        if (xy1[0] <= 0 || xy1[1] <= 0 || xy1[0] >= 0 || xy1[1] >= 0)
                        {

                        }
                        var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                        ref var arouEnvrDatCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                        ref var arouUnitDatCom = ref _cellUnitFilter.Get1(idxCellAround);
                        ref var arouOwnUnitCom = ref _cellUnitFilter.Get3(idxCellAround);

                        if (!arouEnvrDatCom.Have(EnvirTypes.Mountain))
                        {
                            if (arouEnvrDatCom.NeedAmountSteps <= curStepUnitC.AmountSteps || curStepUnitC.HaveMaxSteps(arouUnitDatCom.UnitType))
                            {
                                if (arouUnitDatCom.HaveUnit)
                                {
                                    if (!arouOwnUnitCom.Is(curOwnUnitCom.PlayerType))
                                    {
                                        CellsAttackC.Add(curOwnUnitCom.PlayerType, AttackTypes.Simple, curIdxCell, idxCellAround);
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
