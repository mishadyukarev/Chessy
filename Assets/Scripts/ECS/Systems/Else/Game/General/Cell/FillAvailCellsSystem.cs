using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Cell
{
    internal sealed class FillAvailCellsSystem : IEcsRunSystem
    {
        private EcsFilter<SelectorComponent> _selectFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;

        private EcsFilter<AvailCellsForSetUnitComp> _availCellsForSetUnitFilter = default;
        private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftFilter = default;
        private EcsFilter<AvailCellsForArcherArsonComp> _availCellsForArcherArsonFilter = default;
        private EcsFilter<AvailCellsForUniqueAttackComp> _availCellsForUniqueAttackFilter = default;
        private EcsFilter<AvailCellsForSimpleAttackComp> _availCellsForSimpleAttackFilter = default;

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

                if (curCellUnitDataCom.HaveUnit)
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


                    //ref var availCellsForSimpleAttackComp = ref _availCellsForSimpleAttackFilter.Get1(0);
                    //ref var availCellsForUniqueAttackComp = ref _availCellsForUniqueAttackFilter.Get1(0);

                    //availCellsForSimpleAttackComp.Clear(true);
                    //availCellsForSimpleAttackComp.Clear(false);

                    //availCellsForUniqueAttackComp.Clear(true);
                    //availCellsForUniqueAttackComp.Clear(false);

                    //if (curCellUnitDataCom.IsMelee)
                    //{
                    //    DirectTypes curDurect1 = default;

                    //    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                    //    {
                    //        curDurect1 += 1;
                    //        var idxCellAround = _xyCellFilter.GetIndexCell(xy1);

                    //        ref var arouCellEnvrDataCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                    //        ref var arouCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellAround);
                    //        ref var arouOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellAround);
                    //        ref var arouBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxCellAround);

                    //        if (!arouCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                    //        {
                    //            if (arouCellEnvrDataCom.NeedAmountSteps <= curCellUnitDataCom.AmountSteps || curCellUnitDataCom.HaveMaxAmountSteps)
                    //            {
                    //                if (arouCellUnitDataCom.HaveUnit)
                    //                {
                    //                    if (arouOwnerCellUnitCom.HaveOwner || arouBotOwnerCellUnitCom.IsBot)
                    //                    {
                    //                        if (!arouOwnerCellUnitCom.IsMine)
                    //                        {
                    //                            if (curCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))
                    //                            {
                    //                                if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                    //                                    || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                    //                                {
                    //                                    availCellsForSimpleAttackComp.Add(startOwnerCellUnitDataCom.IsMasterClient, idxCellAround);
                    //                                }
                    //                                else availCellsForUniqueAttackComp.Add(startOwnerCellUnitDataCom.IsMasterClient, idxCellAround);
                    //                            }

                    //                            else if (curCellUnitDataCom.IsUnitType(UnitTypes.King))
                    //                            {
                    //                                availCellsForSimpleAttackComp.Add(startOwnerCellUnitDataCom.IsMasterClient, idxCellAround);
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }

                else
                {

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
                //            for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                //            {
                //                var xy1 = CellSpaceSupport.GetXYCell(xy, directType1);

                //                if (CellViewSystem.IsActiveSelfParentCell(xy1))
                //                {
                //                    if (HaveMinAmountSteps(xy))
                //                    {
                //                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                //                        {
                //                            if (HaveAnyUnit(xy1))
                //                            {
                //                                if (HaveOwner(xy1))
                //                                {
                //                                    if (!IsHim(playerFrom, xy1))
                //                                    {
                //                                        if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                //                                        {
                //                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                //                                            {
                //                                                availableCellsUniqueAttack.Add(xy1);
                //                                            }
                //                                            else availableCellsSimpleAttack.Add(xy1);
                //                                        }

                //                                        else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                //                                        {
                //                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                //                                            {
                //                                                availableCellsSimpleAttack.Add(xy1);
                //                                            }
                //                                            else availableCellsUniqueAttack.Add(xy1);
                //                                        }
                //                                    }
                //                                }

                //                                else if (IsBot(xy1))
                //                                {
                //                                    if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                //                                    {
                //                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                //                                        {
                //                                            availableCellsUniqueAttack.Add(xy1);
                //                                        }
                //                                        else availableCellsSimpleAttack.Add(xy1);
                //                                    }

                //                                    else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                //                                    {
                //                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                //                                        {
                //                                            availableCellsSimpleAttack.Add(xy1);
                //                                        }
                //                                        else availableCellsUniqueAttack.Add(xy1);
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }


                //                    var xy2 = CellSpaceSupport.GetXYCell(xy1, directType1);

                //                    if (IsVisibleUnit(PhotonNetwork.IsMasterClient, xy2))
                //                    {
                //                        if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                //                        {
                //                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                //                            {
                //                                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                //                                {
                //                                    if (HaveAnyUnit(xy2))
                //                                    {
                //                                        if (HaveOwner(xy2))
                //                                        {
                //                                            if (!IsHim(playerFrom, xy2))
                //                                            {
                //                                                availableCellsUniqueAttack.Add(xy2);
                //                                            }
                //                                        }

                //                                        else if (IsBot(xy2))
                //                                        {
                //                                            availableCellsUniqueAttack.Add(xy2);
                //                                        }
                //                                    }
                //                                }
                //                            }

                //                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                //                            {
                //                                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                //                                {
                //                                    if (HaveAnyUnit(xy2))
                //                                    {
                //                                        if (HaveOwner(xy2))
                //                                        {
                //                                            if (!IsHim(playerFrom, xy2))
                //                                            {
                //                                                availableCellsSimpleAttack.Add(xy2);
                //                                            }
                //                                        }

                //                                        else if (IsBot(xy2))
                //                                        {
                //                                            availableCellsSimpleAttack.Add(xy2);
                //                                        }
                //                                    }
                //                                }
                //                            }
                //                        }


                //                        else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                //                        {
                //                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                //                            {
                //                                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                //                                {
                //                                    if (HaveAnyUnit(xy2))
                //                                    {
                //                                        if (HaveOwner(xy2))
                //                                        {
                //                                            if (!IsHim(playerFrom, xy2))
                //                                            {
                //                                                availableCellsUniqueAttack.Add(xy2);
                //                                            }
                //                                        }

                //                                        else if (IsBot(xy2))
                //                                        {
                //                                            availableCellsUniqueAttack.Add(xy2);
                //                                        }
                //                                    }
                //                                }
                //                            }

                //                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                //                            {
                //                                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                //                                {
                //                                    if (HaveAnyUnit(xy2))
                //                                    {
                //                                        if (HaveOwner(xy2))
                //                                        {
                //                                            if (!IsHim(playerFrom, xy2))
                //                                            {
                //                                                availableCellsSimpleAttack.Add(xy2);
                //                                            }
                //                                        }

                //                                        else if (IsBot(xy2))
                //                                        {
                //                                            availableCellsSimpleAttack.Add(xy2);
                //                                        }
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
            }
        }
    }
}
//            }
//        }
//    }
//}