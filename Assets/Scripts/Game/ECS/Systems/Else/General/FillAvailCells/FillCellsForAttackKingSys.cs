using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForAttackKingSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        private EcsFilter<CellsForAttackCom> _cellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var cellsAttackCom = ref _cellsForAttackFilter.Get1(0);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curUnitDatCom.Is(UnitTypes.King))
                    {
                        DirectTypes curDurect1 = default;

                        foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                        {
                            curDurect1 += 1;
                            var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                            ref var arouEnvrDatCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                            ref var arouUnitDatCom = ref _cellUnitFilter.Get1(idxCellAround);
                            ref var arouOwnUnitCom = ref _cellUnitFilter.Get2(idxCellAround);

                            if (!arouEnvrDatCom.HaveEnvir(EnvirTypes.Mountain))
                            {
                                if (arouEnvrDatCom.NeedAmountSteps <= curUnitDatCom.AmountSteps || curUnitDatCom.HaveMaxAmountSteps)
                                {
                                    if (arouUnitDatCom.HaveUnit)
                                    {
                                        if (!arouOwnUnitCom.IsPlayerType(curOwnUnitCom.PlayerType))
                                        {
                                            cellsAttackCom.Add(curOwnUnitCom.PlayerType, AttackTypes.Simple, curIdxCell, idxCellAround);
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
