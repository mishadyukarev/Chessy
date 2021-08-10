using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Cell
{
    internal sealed class FillAvailCellsSystem : IEcsRunSystem
    {
        private EcsFilter<ForFillAvailCellsCom> _forFillAvailCellsFilter = default;
        private EcsFilter<IdxAvailableCellsComponent> _idxAvailCellsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

        public void Run()
        {
            var idxStartUnitCell = _forFillAvailCellsFilter.Get1(0).IdxUnitCell;

            ref var startCellUnitDataCom = ref _cellUnitFilter.Get1(idxStartUnitCell);
            ref var startOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxStartUnitCell);


            var availableCells = new List<byte>();
            var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxStartUnitCell));

            foreach (var xy1 in xyCellsAround)
            {
                var curIdxCell = _xyCellFilter.GetIndexCell(xy1);

                if (!_cellEnvDataFilter.Get1(curIdxCell).HaveEnvironment(EnvironmentTypes.Mountain)
                    && !_cellUnitFilter.Get1(curIdxCell).HaveUnit)
                {
                    if (startCellUnitDataCom.AmountSteps >= _cellEnvDataFilter.Get1(curIdxCell).NeedAmountSteps || _cellUnitFilter.Get1(idxStartUnitCell).HaveMaxAmountSteps)
                    {
                        availableCells.Add(curIdxCell);
                    }
                }
            }

            _idxAvailCellsFilter.Get1(0).SetAllCellsCopy(AvailableCellTypes.Shift, availableCells);



            var availableCellsSimpleAttack = new List<byte>();
            var availableCellsUniqueAttack = new List<byte>();

            if (startCellUnitDataCom.IsMelee)
            {
                var curDurect1 = (DirectTypes)0;

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxStartUnitCell)))
                {
                    curDurect1 += 1;
                    var curIdxCell = _xyCellFilter.GetIndexCell(xy1);

                    ref var curCellEnvrDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                    ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                    ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                    ref var curBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                    if (!curCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        if (curCellEnvrDataCom.NeedAmountSteps <= startCellUnitDataCom.AmountSteps || startCellUnitDataCom.HaveMaxAmountSteps)
                        {
                            if (curCellUnitDataCom.HaveUnit)
                            {
                                if (curOwnerCellUnitCom.HaveOwner || curBotOwnerCellUnitCom.IsBot)
                                {
                                    if (startCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn, UnitTypes.PawnSword }))
                                    {
                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                            || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                        {
                                            availableCellsSimpleAttack.Add(curIdxCell);
                                        }
                                        else availableCellsUniqueAttack.Add(curIdxCell);
                                    }

                                    else if (startCellUnitDataCom.IsUnitType(UnitTypes.King))
                                    {
                                        availableCellsSimpleAttack.Add(curIdxCell);
                                    }
                                }
                            }
                        }
                    }
                }
            }



            //else
            //{
            //    for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
            //    {
            //        var xy1 = CellSpaceSupport.GetXYCell(xy, directType1);

            //        if (CellViewSystem.IsActiveSelfParentCell(xy1))
            //        {
            //            if (HaveMinAmountSteps(xy))
            //            {
            //                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
            //                {
            //                    if (HaveAnyUnit(xy1))
            //                    {
            //                        if (HaveOwner(xy1))
            //                        {
            //                            if (!IsHim(playerFrom, xy1))
            //                            {
            //                                if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
            //                                {
            //                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
            //                                    {
            //                                        availableCellsUniqueAttack.Add(xy1);
            //                                    }
            //                                    else availableCellsSimpleAttack.Add(xy1);
            //                                }

            //                                else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
            //                                {
            //                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
            //                                    {
            //                                        availableCellsSimpleAttack.Add(xy1);
            //                                    }
            //                                    else availableCellsUniqueAttack.Add(xy1);
            //                                }
            //                            }
            //                        }

            //                        else if (IsBot(xy1))
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
            //                }
            //            }


            //            var xy2 = CellSpaceSupport.GetXYCell(xy1, directType1);

            //            if (IsVisibleUnit(PhotonNetwork.IsMasterClient, xy2))
            //            {
            //                if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
            //                {
            //                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
            //                    {
            //                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
            //                        {
            //                            if (HaveAnyUnit(xy2))
            //                            {
            //                                if (HaveOwner(xy2))
            //                                {
            //                                    if (!IsHim(playerFrom, xy2))
            //                                    {
            //                                        availableCellsUniqueAttack.Add(xy2);
            //                                    }
            //                                }

            //                                else if (IsBot(xy2))
            //                                {
            //                                    availableCellsUniqueAttack.Add(xy2);
            //                                }
            //                            }
            //                        }
            //                    }

            //                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
            //                    {
            //                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
            //                        {
            //                            if (HaveAnyUnit(xy2))
            //                            {
            //                                if (HaveOwner(xy2))
            //                                {
            //                                    if (!IsHim(playerFrom, xy2))
            //                                    {
            //                                        availableCellsSimpleAttack.Add(xy2);
            //                                    }
            //                                }

            //                                else if (IsBot(xy2))
            //                                {
            //                                    availableCellsSimpleAttack.Add(xy2);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }


            //                else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
            //                {
            //                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
            //                    {
            //                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
            //                        {
            //                            if (HaveAnyUnit(xy2))
            //                            {
            //                                if (HaveOwner(xy2))
            //                                {
            //                                    if (!IsHim(playerFrom, xy2))
            //                                    {
            //                                        availableCellsUniqueAttack.Add(xy2);
            //                                    }
            //                                }

            //                                else if (IsBot(xy2))
            //                                {
            //                                    availableCellsUniqueAttack.Add(xy2);
            //                                }
            //                            }
            //                        }
            //                    }

            //                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
            //                    {
            //                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
            //                        {
            //                            if (HaveAnyUnit(xy2))
            //                            {
            //                                if (HaveOwner(xy2))
            //                                {
            //                                    if (!IsHim(playerFrom, xy2))
            //                                    {
            //                                        availableCellsSimpleAttack.Add(xy2);
            //                                    }
            //                                }

            //                                else if (IsBot(xy2))
            //                                {
            //                                    availableCellsSimpleAttack.Add(xy2);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //}
        }
    }
}
