using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte curIdx in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilt.Get1(curIdx);
                ref var curStepUnitC = ref _cellUnitFilt.Get2(curIdx);

                ref var effUnitC_0 = ref _cellUnitOthFilt.Get2(curIdx);
                ref var curOnUnitCom = ref _cellUnitOthFilt.Get3(curIdx);

                if (curUnitDatCom.Is(UnitTypes.Pawn))
                {
                    DirectTypes curDurect1 = default;

                    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdx)))
                    {
                        curDurect1 += 1;
                        var idxAround = _xyCellFilter.GetIdxCell(xy1);

                        ref var aroEnvrDatCom = ref _cellEnvDataFilter.Get1(idxAround);
                        ref var aroUnitDatCom = ref _cellUnitFilt.Get1(idxAround);
                        ref var aroOwnUnitCom = ref _cellUnitFilt.Get3(idxAround);

                        if (!aroEnvrDatCom.Have(EnvirTypes.Mountain))
                        {
                            if (aroEnvrDatCom.NeedAmountSteps <= curStepUnitC.AmountSteps 
                                || curStepUnitC.HaveMaxSteps(effUnitC_0, curUnitDatCom.UnitType))
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
