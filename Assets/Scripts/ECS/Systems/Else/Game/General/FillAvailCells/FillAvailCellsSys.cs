using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Cell
{
    internal sealed class FillAvailCellsSys : IEcsRunSystem
    {
        private EcsFilter<SelectorComponent> _selectFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;

        private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftFilter = default;
        private EcsFilter<AvailCellsForArcherArsonComp> _availCellsForArcherArsonFilter = default;
        private EcsFilter<AvailCellsForAttackComp> _availCellsForAttackFilter = default;

        public void Run()
        {
            ref var availCellsForShiftComp = ref _availCellsForShiftFilter.Get1(0);
            ref var selectorComp = ref _selectFilter.Get1(0);




            foreach (byte curIdxCell in _xyCellFilter)
            {
                availCellsForShiftComp.Clear(true, curIdxCell);
                availCellsForShiftComp.Clear(false, curIdxCell);

                ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var availCellsForAttackComp = ref _availCellsForAttackFilter.Get1(0);



                availCellsForAttackComp.Clear(AttackTypes.Simple, true, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Simple, false, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Unique, true, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Unique, false, curIdxCell);


                if (curCellUnitDataCom.HaveUnit)
                {
                    if (curOwnerCellUnitCom.HaveOwner)
                    {
                        var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                        foreach (var xy1 in xyCellsAround)
                        {
                            var idxCellAround = _xyCellFilter.GetIndexCell(xy1);

                            if (!_cellEnvDataFilter.Get1(idxCellAround).HaveEnvironment(EnvironmentTypes.Mountain)
                                && !_cellUnitFilter.Get1(idxCellAround).HaveUnit)
                            {
                                if (curCellUnitDataCom.AmountSteps >= _cellEnvDataFilter.Get1(idxCellAround).NeedAmountSteps || _cellUnitFilter.Get1(curIdxCell).HaveMaxAmountSteps)
                                {
                                    availCellsForShiftComp.AddIdxCell(curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                }
                            }
                        }





                        if (curCellUnitDataCom.IsMelee)
                        {
                            DirectTypes curDurect1 = default;

                            foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                            {
                                curDurect1 += 1;
                                var idxCellAround = _xyCellFilter.GetIndexCell(xy1);

                                ref var arouCellEnvrDataCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                                ref var arouCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellAround);
                                ref var arouOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellAround);
                                ref var arouBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxCellAround);

                                if (!arouCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                                {
                                    if (arouCellEnvrDataCom.NeedAmountSteps <= curCellUnitDataCom.AmountSteps || curCellUnitDataCom.HaveMaxAmountSteps)
                                    {
                                        if (arouCellUnitDataCom.HaveUnit)
                                        {
                                            if (arouOwnerCellUnitCom.HaveOwner)
                                            {
                                                if (!arouOwnerCellUnitCom.IsHim(curOwnerCellUnitCom.Owner))
                                                {
                                                    if (curCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))
                                                    {
                                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                                            || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                        {
                                                            availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                        }
                                                        else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                    }

                                                    else if (curCellUnitDataCom.IsUnitType(UnitTypes.King))
                                                    {
                                                        availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                    }
                                                }
                                            }

                                            else
                                            {
                                                if (curCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))
                                                {
                                                    if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                                        || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                    {
                                                        availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                    }
                                                    else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                }

                                                else if (curCellUnitDataCom.IsUnitType(UnitTypes.King))
                                                {
                                                    availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        else
                        {
                            DirectTypes curDurect1 = default;

                            foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                            {
                                curDurect1 += 1;
                                var idxCellAround1 = _xyCellFilter.GetIndexCell(xy1);

                                ref var arou1CellEnvrDataCom = ref _cellEnvDataFilter.Get1(idxCellAround1);
                                ref var arou1CellUnitDataCom = ref _cellUnitFilter.Get1(idxCellAround1);
                                ref var arou1OwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellAround1);
                                ref var arou1BotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxCellAround1);


                                if (_cellViewFilter.Get1(idxCellAround1).IsActiveParent)
                                {
                                    if (!arou1CellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (curCellUnitDataCom.HaveMinAmountSteps)
                                        {
                                            if (arou1CellUnitDataCom.HaveUnit)
                                            {
                                                if (arou1OwnerCellUnitCom.HaveOwner)
                                                {
                                                    if (!arou1OwnerCellUnitCom.IsHim(curOwnerCellUnitCom.Owner))
                                                    {
                                                        if (curCellUnitDataCom.IsUnitType(UnitTypes.Rook))
                                                        {
                                                            if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                            {
                                                                availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                            }
                                                            else availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                        }

                                                        else if (curCellUnitDataCom.IsUnitType(UnitTypes.Bishop))
                                                        {
                                                            if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                            {
                                                                availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                            }
                                                            else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                        }
                                                    }
                                                }

                                                else
                                                {
                                                    if (curCellUnitDataCom.IsUnitType(UnitTypes.Rook))
                                                    {
                                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                        {
                                                            availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                        }
                                                        else availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                    }

                                                    else if (curCellUnitDataCom.IsUnitType(UnitTypes.Bishop))
                                                    {
                                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                        {
                                                            availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                        }
                                                        else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround1);
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    DirectTypes curDurect2 = default;
                                    foreach (var xy2 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCellAround1)))
                                    {
                                        curDurect2 += 1;
                                        var idxCellAround2 = _xyCellFilter.GetIndexCell(xy2);


                                        ref var arou2CellEnvrDataCom = ref _cellEnvDataFilter.Get1(idxCellAround2);
                                        ref var arou2CellUnitDataCom = ref _cellUnitFilter.Get1(idxCellAround2);
                                        ref var arou2OwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellAround2);
                                        ref var arou2BotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxCellAround2);



                                        if (!arou2CellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                                        {
                                            if (curCellUnitDataCom.HaveMinAmountSteps)
                                            {
                                                if (arou2CellUnitDataCom.HaveUnit)
                                                {
                                                    if (arou2OwnerCellUnitCom.HaveOwner)
                                                    {
                                                        if (!arou2OwnerCellUnitCom.IsHim(curOwnerCellUnitCom.Owner))
                                                        {
                                                            if (curCellUnitDataCom.IsUnitType(UnitTypes.Rook))
                                                            {
                                                                if (curDurect2 == DirectTypes.Left || curDurect2 == DirectTypes.Right || curDurect2 == DirectTypes.Up || curDurect2 == DirectTypes.Down)
                                                                {
                                                                    availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                                }
                                                                else availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                            }

                                                            else if (curCellUnitDataCom.IsUnitType(UnitTypes.Bishop))
                                                            {
                                                                if (curDurect2 == DirectTypes.Left || curDurect2 == DirectTypes.Right || curDurect2 == DirectTypes.Up || curDurect2 == DirectTypes.Down)
                                                                {
                                                                    availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                                }
                                                                else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                            }
                                                        }
                                                    }

                                                    else
                                                    {
                                                        if (curCellUnitDataCom.IsUnitType(UnitTypes.Rook))
                                                        {
                                                            if (curDurect2 == DirectTypes.Left || curDurect2 == DirectTypes.Right || curDurect2 == DirectTypes.Up || curDurect2 == DirectTypes.Down)
                                                            {
                                                                availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                            }
                                                            else availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                        }

                                                        else if (curCellUnitDataCom.IsUnitType(UnitTypes.Bishop))
                                                        {
                                                            if (curDurect2 == DirectTypes.Left || curDurect2 == DirectTypes.Right || curDurect2 == DirectTypes.Up || curDurect2 == DirectTypes.Down)
                                                            {
                                                                availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                            }
                                                            else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround2);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }



                            //    for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                            //{
                            //    var xy1 = CellSpaceSupport.GetXYCell(xy, directType1);

                            //    if (CellViewSystem.IsActiveSelfParentCell(xy1))
                            //    {
                            //        if (HaveMinAmountSteps(xy))
                            //        {
                            //            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                            //            {
                            //                if (HaveAnyUnit(xy1))
                            //                {
                            //                    if (HaveOwner(xy1))
                            //                    {
                            //                        if (!IsHim(playerFrom, xy1))
                            //                        {
                            //                            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            //                            {
                            //                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                            //                                {
                            //                                    availableCellsUniqueAttack.Add(xy1);
                            //                                }
                            //                                else availableCellsSimpleAttack.Add(xy1);
                            //                            }

                            //                            else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                            //                            {
                            //                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                            //                                {
                            //                                    availableCellsSimpleAttack.Add(xy1);
                            //                                }
                            //                                else availableCellsUniqueAttack.Add(xy1);
                            //                            }
                            //                        }
                            //                    }

                            //                    else if (IsBot(xy1))
                            //                    {
                            //                        if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            //                        {
                            //                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                            //                            {
                            //                                availableCellsUniqueAttack.Add(xy1);
                            //                            }
                            //                            else availableCellsSimpleAttack.Add(xy1);
                            //                        }

                            //                        else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                            //                        {
                            //                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                            //                            {
                            //                                availableCellsSimpleAttack.Add(xy1);
                            //                            }
                            //                            else availableCellsUniqueAttack.Add(xy1);
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }


                            //        var xy2 = CellSpaceSupport.GetXYCell(xy1, directType1);

                            //        if (IsVisibleUnit(PhotonNetwork.IsMasterClient, xy2))
                            //        {
                            //            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            //            {
                            //                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            //                {
                            //                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                            //                    {
                            //                        if (HaveAnyUnit(xy2))
                            //                        {
                            //                            if (HaveOwner(xy2))
                            //                            {
                            //                                if (!IsHim(playerFrom, xy2))
                            //                                {
                            //                                    availableCellsUniqueAttack.Add(xy2);
                            //                                }
                            //                            }

                            //                            else if (IsBot(xy2))
                            //                            {
                            //                                availableCellsUniqueAttack.Add(xy2);
                            //                            }
                            //                        }
                            //                    }
                            //                }

                            //                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            //                {
                            //                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                            //                    {
                            //                        if (HaveAnyUnit(xy2))
                            //                        {
                            //                            if (HaveOwner(xy2))
                            //                            {
                            //                                if (!IsHim(playerFrom, xy2))
                            //                                {
                            //                                    availableCellsSimpleAttack.Add(xy2);
                            //                                }
                            //                            }

                            //                            else if (IsBot(xy2))
                            //                            {
                            //                                availableCellsSimpleAttack.Add(xy2);
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }


                            //            else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                            //            {
                            //                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            //                {
                            //                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                            //                    {
                            //                        if (HaveAnyUnit(xy2))
                            //                        {
                            //                            if (HaveOwner(xy2))
                            //                            {
                            //                                if (!IsHim(playerFrom, xy2))
                            //                                {
                            //                                    availableCellsUniqueAttack.Add(xy2);
                            //                                }
                            //                            }

                            //                            else if (IsBot(xy2))
                            //                            {
                            //                                availableCellsUniqueAttack.Add(xy2);
                            //                            }
                            //                        }
                            //                    }
                            //                }

                            //                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            //                {
                            //                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                            //                    {
                            //                        if (HaveAnyUnit(xy2))
                            //                        {
                            //                            if (HaveOwner(xy2))
                            //                            {
                            //                                if (!IsHim(playerFrom, xy2))
                            //                                {
                            //                                    availableCellsSimpleAttack.Add(xy2);
                            //                                }
                            //                            }

                            //                            else if (IsBot(xy2))
                            //                            {
                            //                                availableCellsSimpleAttack.Add(xy2);
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //} 

                        }



                        //    if (selectorComp.IsSelectedCell)
                        //{
                        //    if (selectorComp.IdxSelectedCell == curIdxCell)
                        //    {


                        //        if (curCellUnitDataCom.HaveUnit)
                        //        {
                        //            

                        //            if (selectorComp.IsCellClickType(CellClickTypes.PickFire))
                        //            {
                        //                ref var availCellsForArcherArsonComp = ref _availCellsForArcherArsonFilter.Get1(0);
                        //                availCellsForArcherArsonComp.Clear(startOwnerCellUnitDataCom.IsMasterClient);

                        //                var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                        //                foreach (var xy1 in xyCellsAround)
                        //                {
                        //                    var idxCellAround = _xyCellFilter.GetIndexCell(xy1);

                        //                    if (_cellEnvDataFilter.Get1(idxCellAround).HaveEnvironment(EnvironmentTypes.AdultForest) && !_cellFireDataFilter.Get1(idxCellAround).HaveFire)
                        //                    {
                        //                        availCellsForArcherArsonComp.Add(startOwnerCellUnitDataCom.IsMasterClient, idxCellAround);
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {


                        //        else
                        //        {
                        //            
                        //            
                        //        }
                        //    }
                    }
                }
            }
        }
    }
}