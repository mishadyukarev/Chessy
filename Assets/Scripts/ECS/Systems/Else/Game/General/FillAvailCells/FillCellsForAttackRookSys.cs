using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForAttackRookSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;

        private EcsFilter<CellsForAttackCom> _cellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte idxCell_0 in _xyCellFilter)
            {
                var xy_0 = _xyCellFilter.GetXyCell(idxCell_0);

                ref var unitDataCom_0 = ref _cellUnitFilter.Get1(idxCell_0);
                ref var onUnitCom_0 = ref _cellUnitFilter.Get2(idxCell_0);
                ref var offUnitCom_0 = ref _cellUnitFilter.Get3(idxCell_0);

                ref var cellsAttackCom = ref _cellsForAttackFilter.Get1(0);


                if (unitDataCom_0.HaveUnit && unitDataCom_0.IsUnit(UnitTypes.Rook))
                {
                    if (unitDataCom_0.HaveMinAmountSteps)

                        if (onUnitCom_0.HaveOwner || offUnitCom_0.HaveLocalPlayer)
                        {
                            for (DirectTypes dirType_1 = (DirectTypes)1; dirType_1 < (DirectTypes)Enum.GetNames(typeof(DirectTypes)).Length; dirType_1++)
                            {
                                var xy_1 = CellSpaceSupport.GetXyCellByDirect(xy_0, dirType_1);
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);


                                ref var envrDatCom_1 = ref _cellEnvDataFilter.Get1(idxCell_1);
                                ref var unitDatCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var onUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);
                                ref var offUnitCom_1 = ref _cellUnitFilter.Get3(idxCell_1);


                                if (_cellViewFilter.Get1(idxCell_1).IsActiveParent)
                                {
                                    if (!envrDatCom_1.HaveEnvir(EnvirTypes.Mountain))
                                    {
                                        if (unitDatCom_1.HaveUnit)
                                        {
                                            if (onUnitCom_1.HaveOwner)
                                            {
                                                if (!onUnitCom_1.IsHim(onUnitCom_0.Owner))
                                                {
                                                    if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                                    {
                                                        cellsAttackCom.Add(AttackTypes.Unique, onUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                                    }
                                                    else cellsAttackCom.Add(AttackTypes.Simple, onUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                                }
                                            }

                                            else if (offUnitCom_1.HaveLocalPlayer)
                                            {
                                                if (offUnitCom_1.IsMainMaster != offUnitCom_0.IsMainMaster)
                                                {
                                                    if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                                    {
                                                        cellsAttackCom.Add(AttackTypes.Unique, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_1);
                                                    }
                                                    else cellsAttackCom.Add(AttackTypes.Simple, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_1);
                                                }
                                            }

                                            else
                                            {
                                                if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                                {
                                                    cellsAttackCom.Add(AttackTypes.Unique, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_1);
                                                }
                                                else cellsAttackCom.Add(AttackTypes.Simple, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_1);
                                            }
                                        }


                                        var xy_2 = CellSpaceSupport.GetXyCellByDirect(xy_1, dirType_1);
                                        var idxCell_2 = _xyCellFilter.GetIdxCell(xy_2);


                                        ref var envrDataCom_2 = ref _cellEnvDataFilter.Get1(idxCell_2);
                                        ref var unitDataCom_2 = ref _cellUnitFilter.Get1(idxCell_2);
                                        ref var onUnitCom_2 = ref _cellUnitFilter.Get2(idxCell_2);
                                        ref var offUnitCom_2 = ref _cellUnitFilter.Get3(idxCell_2);



                                        if (unitDataCom_2.HaveUnit)
                                        {
                                            var isVisibleUnit_2 = false;

                                            if (PhotonNetwork.OfflineMode) isVisibleUnit_2 = unitDataCom_2.IsVisibleUnit(offUnitCom_0.IsMainMaster);
                                            else isVisibleUnit_2 = unitDataCom_2.IsVisibleUnit(onUnitCom_0.IsMasterClient);

                                            if (isVisibleUnit_2)
                                            {
                                                if (dirType_1 == DirectTypes.LeftDown || dirType_1 == DirectTypes.LeftUp || dirType_1 == DirectTypes.RightUp || dirType_1 == DirectTypes.RightDown)
                                                {
                                                    if (onUnitCom_2.HaveOwner)
                                                    {
                                                        if (!onUnitCom_2.IsHim(onUnitCom_0.Owner))
                                                        {
                                                            cellsAttackCom.Add(AttackTypes.Simple, onUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
                                                        }
                                                    }
                                                    else if (offUnitCom_2.HaveLocalPlayer)
                                                    {
                                                        if (offUnitCom_2.IsMainMaster != offUnitCom_0.IsMainMaster)
                                                        {
                                                            cellsAttackCom.Add(AttackTypes.Simple, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_2);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        cellsAttackCom.Add(AttackTypes.Simple, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_2);
                                                    }
                                                }

                                                else
                                                {
                                                    if (onUnitCom_2.HaveOwner)
                                                    {
                                                        if (!onUnitCom_2.IsHim(onUnitCom_0.Owner))
                                                        {
                                                            cellsAttackCom.Add(AttackTypes.Unique, onUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
                                                        }
                                                    }
                                                    else if (offUnitCom_2.HaveLocalPlayer)
                                                    {
                                                        if (offUnitCom_2.IsMainMaster != offUnitCom_0.IsMainMaster)
                                                        {
                                                            cellsAttackCom.Add(AttackTypes.Unique, offUnitCom_0.IsMainMaster, idxCell_0, idxCell_2);
                                                        }
                                                    }

                                                    else
                                                    {
                                                        cellsAttackCom.Add(AttackTypes.Unique, onUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
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
    }
}
