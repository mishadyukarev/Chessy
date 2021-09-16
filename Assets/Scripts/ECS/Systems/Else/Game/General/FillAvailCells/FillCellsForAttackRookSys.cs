using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForAttackRookSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;

        private EcsFilter<AvailCellsForAttackComp> _cellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte idxCell_0 in _xyCellFilter)
            {
                var xy_0 = _xyCellFilter.GetXyCell(idxCell_0);

                ref var unitDataCom_0 = ref _cellUnitFilter.Get1(idxCell_0);
                ref var ownerUnitCom_0 = ref _cellUnitFilter.Get2(idxCell_0);

                ref var cellsForAttackComp = ref _cellsForAttackFilter.Get1(0);


                if (unitDataCom_0.HaveUnit && unitDataCom_0.IsUnitType(UnitTypes.Rook))
                {
                    if (unitDataCom_0.HaveMinAmountSteps)

                        if (ownerUnitCom_0.HaveOwner)
                        {
                            for (DirectTypes dirType_1 = (DirectTypes)1; dirType_1 < (DirectTypes)Enum.GetNames(typeof(DirectTypes)).Length; dirType_1++)
                            {
                                var xy_1 = CellSpaceSupport.GetXyCellByDirect(xy_0, dirType_1);
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);


                                ref var envrDataCom_1 = ref _cellEnvDataFilter.Get1(idxCell_1);
                                ref var unitDataCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var ownerUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);


                                if (_cellViewFilter.Get1(idxCell_1).IsActiveParent)
                                {
                                    if (!envrDataCom_1.HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (unitDataCom_1.HaveUnit)
                                        {
                                            if (ownerUnitCom_1.HaveOwner)
                                            {
                                                if (!ownerUnitCom_1.IsHim(ownerUnitCom_0.Owner))
                                                {
                                                    if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                                    {
                                                        cellsForAttackComp.Add(AttackTypes.Unique, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                                    }
                                                    else cellsForAttackComp.Add(AttackTypes.Simple, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                                }
                                            }

                                            else
                                            {
                                                if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                                {
                                                    cellsForAttackComp.Add(AttackTypes.Unique, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                                }
                                                else cellsForAttackComp.Add(AttackTypes.Simple, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_1);
                                            }
                                        }


                                        var xy_2 = CellSpaceSupport.GetXyCellByDirect(xy_1, dirType_1);
                                        var idxCell_2 = _xyCellFilter.GetIdxCell(xy_2);


                                        ref var envrDataCom_2 = ref _cellEnvDataFilter.Get1(idxCell_2);
                                        ref var unitDataCom_2 = ref _cellUnitFilter.Get1(idxCell_2);
                                        ref var ownerUnitCom_2 = ref _cellUnitFilter.Get2(idxCell_2);


                                        if (unitDataCom_2.IsVisibleUnit(ownerUnitCom_0.IsMasterClient))

                                            if (dirType_1 == DirectTypes.LeftDown || dirType_1 == DirectTypes.LeftUp || dirType_1 == DirectTypes.RightUp || dirType_1 == DirectTypes.RightDown)
                                            {
                                                if (!envrDataCom_2.HaveEnvironment(EnvironmentTypes.Mountain))
                                                {
                                                    if (unitDataCom_2.HaveUnit)
                                                    {
                                                        if (ownerUnitCom_2.HaveOwner)
                                                        {
                                                            if (!ownerUnitCom_2.IsHim(ownerUnitCom_0.Owner))
                                                            {
                                                                cellsForAttackComp.Add(AttackTypes.Simple, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
                                                            }
                                                        }

                                                        else
                                                        {
                                                            cellsForAttackComp.Add(AttackTypes.Simple, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
                                                        }
                                                    }
                                                }
                                            }

                                            else
                                            {
                                                if (!envrDataCom_2.HaveEnvironment(EnvironmentTypes.Mountain))
                                                {
                                                    if (unitDataCom_2.HaveUnit)
                                                    {
                                                        if (ownerUnitCom_2.HaveOwner)
                                                        {
                                                            if (!ownerUnitCom_2.IsHim(ownerUnitCom_0.Owner))
                                                            {
                                                                cellsForAttackComp.Add(AttackTypes.Unique, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
                                                            }
                                                        }

                                                        else
                                                        {
                                                            cellsForAttackComp.Add(AttackTypes.Unique, ownerUnitCom_0.IsMasterClient, idxCell_0, idxCell_2);
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
