using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte curIdx in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);
                ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdx);

                if (curUnitDatCom.Is(UnitTypes.Pawn))
                {
                    DirectTypes curDurect1 = default;

                    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdx)))
                    {
                        curDurect1 += 1;
                        var idxAround = _xyCellFilter.GetIdxCell(xy1);

                        ref var aroEnvrDatCom = ref _cellEnvDataFilter.Get1(idxAround);
                        ref var aroUnitDatCom = ref _cellUnitFilter.Get1(idxAround);
                        ref var aroOwnUnitCom = ref _cellUnitFilter.Get2(idxAround);

                        if (!aroEnvrDatCom.Have(EnvirTypes.Mountain))
                        {
                            if (aroEnvrDatCom.NeedAmountSteps <= curUnitDatCom.AmountSteps || curUnitDatCom.HaveMaxAmountSteps || curUnitDatCom.Have(StatTypes.Steps))
                            {
                                if (aroUnitDatCom.HaveUnit)
                                {
                                    if (!aroOwnUnitCom.Is(curOnUnitCom.PlayerType))
                                    {
                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                            || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                        {
                                            CellsAttackC.Add(curOnUnitCom.PlayerType, AttackTypes.Simple, curIdx, idxAround);
                                        }
                                        else CellsAttackC.Add(curOnUnitCom.PlayerType, AttackTypes.Unique, curIdx, idxAround);
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
