using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent, ConditionUnitC, OwnerCom, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, UnitEffectsC, ThirstyUnitC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataCom, VisibleC> _cellUnitViewFilt = default;
        private EcsFilter<CellUnitMainViewCom> _cellUnitViewFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;
        private EcsFilter<CellBlocksViewComponent> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var unitC_0 = ref _cellUnitFilter.Get1(idx);
                ref var curHpUnitC = ref _cellUnitFilter.Get2(idx);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(idx);
                ref var condUnitC = ref _cellUnitOthFilt.Get2(idx);
                ref var effUnitC = ref _cellUnitOthFilt.Get3(idx);
                ref var thirUnitC_0 = ref _cellUnitOthFilt.Get4(idx);
                ref var curOwnUnitCom = ref _cellUnitOthFilt.Get5(idx);
                ref var curVisUnitCom = ref _cellUnitViewFilt.Get2(idx);

                ref var curUnitViewCom = ref _cellUnitViewFilter.Get1(idx);

                ref var barsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);


                if (curVisUnitCom.IsVisibled(WhoseMoveC.CurPlayer))
                {
                    if (unitC_0.HaveUnit)
                    {
                        if (!unitC_0.Is(UnitTypes.Scout))
                        {
                            barsViewCom.EnableSR(CellBarTypes.Hp);
                            barsViewCom.SetColorHp(Color.red);

                            float xCordinate = (float)curHpUnitC.AmountHp / curHpUnitC.MaxHpUnit(effUnitC, unitC_0.Unit);
                            barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));
                        }

                        if (thirUnitC_0.NeedWater(unitC_0.Unit))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.NeedWater);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);
                        }

                        if (curStepUnitC.HaveMaxSteps(effUnitC, unitC_0.Unit))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (condUnitC.Is(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (condUnitC.Is(CondUnitTypes.Relaxed))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (curOwnUnitCom.Is(PlayerTypes.First))
                        {
                            barsViewCom.SetColorHp(Color.blue);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.blue);
                        }
                        else
                        {
                            barsViewCom.SetColorHp(Color.red);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.red);
                        }
                    }
                }
            }
        }
    }
}
