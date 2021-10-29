using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, OwnerCom, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom> _cellUnitViewFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;
        private EcsFilter<CellBlocksViewComponent> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var curUnitDatC = ref _cellUnitFilter.Get1(idx);
                ref var curHpUnitC = ref _cellUnitFilter.Get2(idx);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(idx);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get4(idx);
                ref var curVisUnitCom = ref _cellUnitFilter.Get5(idx);

                ref var curUnitViewCom = ref _cellUnitViewFilter.Get1(idx);

                ref var barsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);


                if (curVisUnitCom.IsVisibled(WhoseMoveC.CurPlayer))
                {

                    if (curUnitDatC.HaveUnit)
                    {
                        if (!curUnitDatC.Is(UnitTypes.Scout))
                        {
                            barsViewCom.EnableSR(CellBarTypes.Hp);
                            barsViewCom.SetColorHp(Color.red);

                            float xCordinate = (float)curHpUnitC.AmountHealth / curHpUnitC.MaxAmountHealth(curUnitDatC.UnitType);
                            barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));
                        }

                        if (curStepUnitC.HaveMaxAmountSteps(curUnitDatC.UnitType))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (curUnitDatC.Is(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (curUnitDatC.Is(CondUnitTypes.Relaxed))
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
