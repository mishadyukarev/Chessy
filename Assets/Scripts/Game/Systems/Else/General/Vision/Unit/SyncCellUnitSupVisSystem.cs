using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom> _cellUnitViewFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;
        private EcsFilter<CellBlocksViewComponent> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var curUnitDataCom = ref _cellUnitFilter.Get1(idx);
                ref var curUnitViewCom = ref _cellUnitViewFilter.Get1(idx);

                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idx);

                ref var barsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);


                if (curUnitDataCom.IsVisibleUnit(WhoseMoveCom.CurPlayer))
                {
                    if (curUnitDataCom.HaveUnit)
                    {
                        barsViewCom.EnableSR(CellBarTypes.Hp);
                        barsViewCom.SetColorHp(Color.red);

                        float xCordinate = (float)curUnitDataCom.AmountHealth / curUnitDataCom.MaxAmountHealth;
                        barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));


                        if (curUnitDataCom.HaveMaxAmountSteps)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (curUnitDataCom.Is(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (curUnitDataCom.Is(CondUnitTypes.Relaxed))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (curOwnUnitCom.IsPlayerType(PlayerTypes.First))
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
