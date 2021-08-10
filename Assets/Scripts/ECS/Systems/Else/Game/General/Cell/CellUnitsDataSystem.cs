//using Assets.Scripts.Workers.Cell;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
//{
//    internal static class CellUnitsDataSystem
//    {
//        #region Else

//        //internal static void ShiftPlayerUnitToBaseCell(int[] fromXy, int[] toXy)
//        //{
//        //    var unitType = UnitType(fromXy);
//        //    var amountHealth = AmountHealth(fromXy);
//        //    var amountSteps = AmountSteps(fromXy);
//        //    var protectRelaxType = ConditionType(fromXy);
//        //    var player = Owner(fromXy);

//        //    SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, toXy);
//        //    SetOwner(player, toXy);

//        //    SetStandartValuesUnit(default, default, default, default, fromXy);
//        //    ResetOwner(fromXy);
//        //}

//        //internal static void SetPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, Player player, int[] xy)
//        //{
//        //    SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
//        //    SetOwner(player, xy);
//        //}

//        //internal static void SetNewPlayerUnit(UnitTypes unitType, Player player, int[] xy)
//        //{
//        //    int amountHealth;
//        //    int amountSteps;

//        //    switch (unitType)
//        //    {
//        //        case UnitTypes.None:
//        //            throw new Exception();

//        //        case UnitTypes.King:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_KING;
//        //            amountSteps = STANDART_AMOUNT_STEPS_KING;
//        //            break;

//        //        case UnitTypes.Pawn:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_PAWN;
//        //            amountSteps = STANDART_AMOUNT_STEPS_PAWN;
//        //            break;

//        //        case UnitTypes.PawnSword:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_PAWN_SWORD;
//        //            amountSteps = STANDART_AMOUNT_STEPS_PAWN_SWORD;
//        //            break;

//        //        case UnitTypes.Rook:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_ROOK;
//        //            amountSteps = STANDART_AMOUNT_STEPS_ROOK;
//        //            break;

//        //        case UnitTypes.RookCrossbow:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;
//        //            amountSteps = STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;
//        //            break;

//        //        case UnitTypes.Bishop:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_BISHOP;
//        //            amountSteps = STANDART_AMOUNT_STEPS_BISHOP;
//        //            break;

//        //        case UnitTypes.BishopCrossbow:
//        //            amountHealth = STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;
//        //            amountSteps = STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
//        //            break;

//        //        default:
//        //            throw new Exception();
//        //    }

//        //    SetPlayerUnit(unitType, amountHealth, amountSteps, ConditionUnitTypes.None, player, xy);
//        //}

//        //internal static void ResetUnit(int[] xy)
//        //{
//        //    ResetStandartValuesUnit(xy);

//        //    ResetOwner(xy);
//        //    ResetIsBot(xy);
//        //}

//        //internal static void SyncAll(UnitTypes unitType, Player owner, bool haveBot, int amountHealth, int amountSteps, ConditionUnitTypes conditionType, int[] xy)
//        //{
//        //    SetStandartValuesUnit(unitType, amountHealth, amountSteps, conditionType, xy);
//        //    SetOwner(owner, xy);
//        //    SetIsBot(haveBot, xy);
//        //}


//        #endregion


//        #region ForMoving

//        //internal static List<byte> GetCellsForShift(byte[] xy)
//        //{
//        //    var list = new List<byte>();

//        //    var listAvailable = CellSpaceSupport.TryGetXyAround(xy);

//        //    foreach (var xy1 in listAvailable)
//        //    {
//        //        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1) && !HaveAnyUnit(xy1))
//        //        {
//        //            if (AmountSteps(xy) >= CellEnvrDataSystem.NeedAmountSteps(xy1) || HaveMaxAmountSteps(xy))
//        //            {
//        //                list.Add(xy1);
//        //            }
//        //        }
//        //    }

//        //    return list;
//        //}
//        //internal static void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack, int[] xy)
//        //{
//        //    availableCellsSimpleAttack = new List<int[]>();
//        //    availableCellsUniqueAttack = new List<int[]>();

//        //    if (IsMelee(xy))
//        //    {
//        //        for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
//        //        {
//        //            var xy1 = CellSpaceSupport.GetXYCell(xy, directType1);


//        //            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
//        //            {
//        //                if (CellEnvrDataSystem.NeedAmountSteps(xy1) <= AmountSteps(xy) || HaveMaxAmountSteps(xy))
//        //                {
//        //                    if (HaveAnyUnit(xy1))
//        //                    {
//        //                        if (HaveOwner(xy1))
//        //                        {
//        //                            if (!IsHim(playerFrom, xy1))
//        //                            {
//        //                                if (UnitType(xy) == UnitTypes.Pawn || UnitType(xy) == UnitTypes.PawnSword)
//        //                                {
//        //                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                    {
//        //                                        availableCellsSimpleAttack.Add(xy1);
//        //                                    }
//        //                                    else availableCellsUniqueAttack.Add(xy1);
//        //                                }

//        //                                else if (UnitType(xy) == UnitTypes.King)
//        //                                {
//        //                                    availableCellsSimpleAttack.Add(xy1);
//        //                                }
//        //                            }
//        //                        }
//        //                        else if (IsBot(xy1))
//        //                        {
//        //                            if (UnitType(xy) == UnitTypes.Pawn || UnitType(xy) == UnitTypes.PawnSword)
//        //                            {
//        //                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                {
//        //                                    availableCellsSimpleAttack.Add(xy1);
//        //                                }
//        //                                else availableCellsUniqueAttack.Add(xy1);
//        //                            }

//        //                            else if (UnitType(xy) == UnitTypes.King)
//        //                            {
//        //                                availableCellsSimpleAttack.Add(xy1);
//        //                            }
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }



//        //    else
//        //    {
//        //        for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
//        //        {
//        //            var xy1 = CellSpaceSupport.GetXYCell(xy, directType1);

//        //            if (CellViewSystem.IsActiveSelfParentCell(xy1))
//        //            {
//        //                if (HaveMinAmountSteps(xy))
//        //                {
//        //                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
//        //                    {
//        //                        if (HaveAnyUnit(xy1))
//        //                        {
//        //                            if (HaveOwner(xy1))
//        //                            {
//        //                                if (!IsHim(playerFrom, xy1))
//        //                                {
//        //                                    if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
//        //                                    {
//        //                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                        {
//        //                                            availableCellsUniqueAttack.Add(xy1);
//        //                                        }
//        //                                        else availableCellsSimpleAttack.Add(xy1);
//        //                                    }

//        //                                    else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
//        //                                    {
//        //                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                        {
//        //                                            availableCellsSimpleAttack.Add(xy1);
//        //                                        }
//        //                                        else availableCellsUniqueAttack.Add(xy1);
//        //                                    }
//        //                                }
//        //                            }

//        //                            else if (IsBot(xy1))
//        //                            {
//        //                                if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
//        //                                {
//        //                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                    {
//        //                                        availableCellsUniqueAttack.Add(xy1);
//        //                                    }
//        //                                    else availableCellsSimpleAttack.Add(xy1);
//        //                                }

//        //                                else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
//        //                                {
//        //                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
//        //                                    {
//        //                                        availableCellsSimpleAttack.Add(xy1);
//        //                                    }
//        //                                    else availableCellsUniqueAttack.Add(xy1);
//        //                                }
//        //                            }
//        //                        }
//        //                    }
//        //                }


//        //                var xy2 = CellSpaceSupport.GetXYCell(xy1, directType1);

//        //                if (IsVisibleUnit(PhotonNetwork.IsMasterClient, xy2))
//        //                {
//        //                    if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
//        //                    {
//        //                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
//        //                        {
//        //                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
//        //                            {
//        //                                if (HaveAnyUnit(xy2))
//        //                                {
//        //                                    if (HaveOwner(xy2))
//        //                                    {
//        //                                        if (!IsHim(playerFrom, xy2))
//        //                                        {
//        //                                            availableCellsUniqueAttack.Add(xy2);
//        //                                        }
//        //                                    }

//        //                                    else if (IsBot(xy2))
//        //                                    {
//        //                                        availableCellsUniqueAttack.Add(xy2);
//        //                                    }
//        //                                }
//        //                            }
//        //                        }

//        //                        if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
//        //                        {
//        //                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
//        //                            {
//        //                                if (HaveAnyUnit(xy2))
//        //                                {
//        //                                    if (HaveOwner(xy2))
//        //                                    {
//        //                                        if (!IsHim(playerFrom, xy2))
//        //                                        {
//        //                                            availableCellsSimpleAttack.Add(xy2);
//        //                                        }
//        //                                    }

//        //                                    else if (IsBot(xy2))
//        //                                    {
//        //                                        availableCellsSimpleAttack.Add(xy2);
//        //                                    }
//        //                                }
//        //                            }
//        //                        }
//        //                    }


//        //                    else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
//        //                    {
//        //                        if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
//        //                        {
//        //                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
//        //                            {
//        //                                if (HaveAnyUnit(xy2))
//        //                                {
//        //                                    if (HaveOwner(xy2))
//        //                                    {
//        //                                        if (!IsHim(playerFrom, xy2))
//        //                                        {
//        //                                            availableCellsUniqueAttack.Add(xy2);
//        //                                        }
//        //                                    }

//        //                                    else if (IsBot(xy2))
//        //                                    {
//        //                                        availableCellsUniqueAttack.Add(xy2);
//        //                                    }
//        //                                }
//        //                            }
//        //                        }

//        //                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
//        //                        {
//        //                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
//        //                            {
//        //                                if (HaveAnyUnit(xy2))
//        //                                {
//        //                                    if (HaveOwner(xy2))
//        //                                    {
//        //                                        if (!IsHim(playerFrom, xy2))
//        //                                        {
//        //                                            availableCellsSimpleAttack.Add(xy2);
//        //                                        }
//        //                                    }

//        //                                    else if (IsBot(xy2))
//        //                                    {
//        //                                        availableCellsSimpleAttack.Add(xy2);
//        //                                    }
//        //                                }
//        //                            }
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }
//        //}
//        //internal static List<int[]> GetStartCellsForSettingUnit(Player player)
//        //{
//        //    var list = new List<int[]>();

//        //    for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
//        //        for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
//        //        {
//        //            var xy = new int[] { x, y };

//        //            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy))
//        //            {
//        //                if (!HaveAnyUnit(xy))
//        //                {
//        //                    if (MainGameSystem.XyStartCellsCom.IsStartedCell(player.IsMasterClient, xy))
//        //                    {
//        //                        list.Add(new int[] { x, y });
//        //                    }
//        //                }
//        //            }
//        //        }

//        //    return list;
//        //}

//        #endregion
//    }
//}
