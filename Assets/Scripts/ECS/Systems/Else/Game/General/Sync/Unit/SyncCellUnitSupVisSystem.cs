using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewComp> _cellUnitViewFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;
        private EcsFilter<CellBlocksViewComponent> _cellBlocksFilter = default;

        private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var curUnitDataCom = ref _cellUnitFilter.Get1(idx);
                ref var curUnitViewCom = ref _cellUnitViewFilter.Get1(idx);

                ref var curOnlineUnitCom = ref _cellUnitFilter.Get2(idx);
                ref var curOffUnitCom = ref _cellUnitFilter.Get3(idx);
                ref var curBotUnitCom = ref _cellUnitFilter.Get4(idx);


                ref var barsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);


                if (curUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
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

                        if (curUnitDataCom.IsConditionType(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (curUnitDataCom.IsConditionType(CondUnitTypes.Relaxed))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (curOnlineUnitCom.HaveOwner)
                        {
                            if (curOnlineUnitCom.IsMasterClient)
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

                        else if (curOffUnitCom.LocalPlayerType != default)
                        {
                            if (WhoseMoveCom.IsMainMove)
                            {
                                if (curOffUnitCom.Is(PlayerTypes.First))
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
                            else
                            {
                                if (curOffUnitCom.Is(PlayerTypes.First))
                                {
                                    barsViewCom.SetColorHp(Color.red);
                                    blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.red);
                                }
                                else
                                {
                                    barsViewCom.SetColorHp(Color.blue);
                                    blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.blue);
                                }
                            }
                        }

                        else if (curBotUnitCom.IsBot)
                        {
                            barsViewCom.SetColorHp(Color.red);
                        }
                    }
                }
            }
        }
    }
}
