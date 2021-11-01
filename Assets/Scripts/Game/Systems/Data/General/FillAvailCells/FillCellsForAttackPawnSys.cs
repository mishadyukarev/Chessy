﻿using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;

        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilt.Get1(idx_0);
                ref var curStepUnitC = ref _cellUnitFilt.Get2(idx_0);

                ref var effUnitC_0 = ref _cellUnitOthFilt.Get2(idx_0);
                ref var curOnUnitCom = ref _cellUnitOthFilt.Get3(idx_0);


                if (curUnitDatCom.Is(UnitTypes.Pawn))
                {
                    DirectTypes curDurect1 = default;

                    CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idx_0).XyCell, out var dirs);

                    foreach (var item_1 in dirs)
                    {
                        curDurect1 += 1;
                        var idx_1 = _xyCellFilter.GetIdxCell(item_1.Value);

                        ref var envC_1 = ref _cellEnvDataFilter.Get1(idx_1);
                        ref var unitC_1 = ref _cellUnitFilt.Get1(idx_1);
                        ref var ownUnitC_1 = ref _cellUnitFilt.Get3(idx_1);

                        ref var trail_1 = ref _cellTrailFilt.Get1(idx_1);


                        if (!envC_1.Have(EnvTypes.Mountain))
                        {
                            if (curStepUnitC.HaveStepsForDoing(envC_1, item_1.Key, trail_1) || curStepUnitC.HaveMaxSteps(effUnitC_0, curUnitDatCom.Unit))
                            {
                                if (unitC_1.HaveUnit)
                                {
                                    if (!ownUnitC_1.Is(curOnUnitCom.Owner))
                                    {
                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                            || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                        {
                                            CellsAttackC.Add(curOnUnitCom.Owner, AttackTypes.Simple, idx_0, idx_1);
                                        }
                                        else CellsAttackC.Add(curOnUnitCom.Owner, AttackTypes.Unique, idx_0, idx_1);
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