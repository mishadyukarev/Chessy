using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent, CellUnitViewComponent> _cellUnitFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;
        private EcsFilter<CellBlocksViewComponent> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var cellUnitDataCom = ref _cellUnitFilter.Get1(idx);
                ref var ownerCellUnitCom = ref _cellUnitFilter.Get2(idx);
                ref var botOwnerCellUnitCom = ref _cellUnitFilter.Get3(idx);
                ref var cellUnitViewCom = ref _cellUnitFilter.Get3(idx);

                ref var cellBarsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var cellBlocksViewCom = ref _cellBlocksFilter.Get1(idx);


                if (cellUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                {
                    if (cellUnitDataCom.HaveUnit)
                    {
                        cellBarsViewCom.EnableSR(CellBarTypes.Hp);
                        cellBarsViewCom.SetColor(CellBarTypes.Hp, Color.red);

                        float xCordinate = (float)cellUnitDataCom.AmountHealth / cellUnitDataCom.MaxAmountHealth;
                        cellBarsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));


                        if (cellUnitDataCom.HaveMaxAmountSteps)
                        {
                            cellBlocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            cellBlocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (cellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                        {
                            cellBlocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            cellBlocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (cellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                        {
                            cellBlocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            cellBlocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            cellBlocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (ownerCellUnitCom.HaveOwner)
                        {
                            if (ownerCellUnitCom.IsMasterClient)
                            {
                                cellBarsViewCom.SetColor(CellBarTypes.Hp, Color.blue);
                                cellBlocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.blue);
                            }
                            else
                            {
                                cellBarsViewCom.SetColor(CellBarTypes.Hp, Color.red);
                                cellBlocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.red);
                            }
                        }

                        else if (botOwnerCellUnitCom.IsBot)
                        {
                            cellBarsViewCom.SetColor(CellBarTypes.Hp, Color.red);
                        }
                    }

                    else
                    {
                        cellBarsViewCom.DisableSR(CellBarTypes.Hp);

                        cellBlocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        cellBlocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        cellBarsViewCom.DisableSR(CellBarTypes.Hp);
                    }
                }

                else
                {
                    cellBarsViewCom.DisableSR(CellBarTypes.Hp);

                    cellBlocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                    cellBlocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                    cellBarsViewCom.DisableSR(CellBarTypes.Hp);
                }
            }
        }
    }
}
