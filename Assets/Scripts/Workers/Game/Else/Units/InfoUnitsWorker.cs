using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Info
{
    internal abstract class InfoUnitsWorker : MainGeneralWorker
    {
        #region AmountUnits

        internal static int GetAmountAllUnitsInGame() => GetAmountAllUnitsInGame(true) + GetAmountAllUnitsInGame(false);
        internal static int GetAmountAllUnitsInGame(bool key)
        {
            return AmountUnitsInGame(UnitTypes.King, key)
                + AmountUnitsInGame(UnitTypes.Pawn, key)
                + AmountUnitsInGame(UnitTypes.PawnSword, key)
                + AmountUnitsInGame(UnitTypes.Rook, key)
                + AmountUnitsInGame(UnitTypes.RookCrossbow, key)
                + AmountUnitsInGame(UnitTypes.Bishop, key)
                + AmountUnitsInGame(UnitTypes.BishopCrossbow, key);
        }
        internal static int AmountUnitsInGame(UnitTypes unitType, bool key)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Count;

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountUnitInGame(UnitTypes unitType, bool key, List<int[]> list)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key] = list.Copy();
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void AddAmountUnitInGame(UnitTypes unitType, bool key, int[] xyAdding)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Add(xyAdding);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeAmountUnitInGame(UnitTypes unitType, bool key, int[] xyTaking)
        {
            List<int[]> list;
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    list = EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.Pawn:
                    list = EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.PawnSword:
                    list = EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.Rook:
                    list = EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.RookCrossbow:
                    list = EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.Bishop:
                    list = EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                case UnitTypes.BishopCrossbow:
                    list = EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];
                    break;

                default:
                    throw new Exception();
            }

            if (!TryFindCellInListAndRemove(xyTaking, list)) throw new Exception();
        }

        internal static List<int[]> GetLixtXyUnits(UnitTypes unitType, bool key)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key].Copy();

                default:
                    throw new Exception();
            }
        }

        internal static bool IsSettedKing(bool key) => GetLixtXyUnits(UnitTypes.King, key).Count > 0;

        #endregion


        internal static int GetAmountUnitsInStandartCondition(ConditionTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ConditionTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Count;

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key].Count;

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Count;

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key].Count;

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Count;

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key].Count;

                        default:
                            throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }
        internal static List<int[]> GetUnitsInStandardCondition(ConditionTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ConditionTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Copy();

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key].Copy();

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Copy();

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key].Copy();

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Copy();

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key].Copy();

                        default:
                            throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }
        internal static bool TryFindUnitInStandartCondition(ConditionTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].TryFindCell(xy);

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key].TryFindCell(xy);

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].TryFindCell(xy);

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key].TryFindCell(xy);

                        default:
                            throw new Exception();
                    }

                case ConditionTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].TryFindCell(xy);

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key].TryFindCell(xy);

                        default:
                            throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }
        internal static void AddUnitInStandartCondition(ConditionTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.Pawn:
                            EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.PawnSword:
                            EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.Rook:
                            EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.RookCrossbow:
                            EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.Bishop:
                            EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key].Add(xy);
                            break;

                        case UnitTypes.BishopCrossbow:
                            EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key].Add(xy);
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                case ConditionTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.Pawn:
                            EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.PawnSword:
                            EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.Rook:
                            EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.RookCrossbow:
                            EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.Bishop:
                            EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key].Add(xy);
                            break;

                        case UnitTypes.BishopCrossbow:
                            EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key].Add(xy);
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                case ConditionTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.Pawn:
                            EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.PawnSword:
                            EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.Rook:
                            EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.RookCrossbow:
                            EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.Bishop:
                            EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key].Add(xy);
                            break;

                        case UnitTypes.BishopCrossbow:
                            EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key].Add(xy);
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeUnitInStandartCondition(ConditionTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            List<int[]> curList;

            switch (protectRelaxType)
            {
                case ConditionTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            curList = EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.Pawn:
                            curList = EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.PawnSword:
                            curList = EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.Rook:
                            curList = EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.RookCrossbow:
                            curList = EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.Bishop:
                            curList = EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];
                            break;

                        case UnitTypes.BishopCrossbow:
                            curList = EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key];
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                case ConditionTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            curList = EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.Pawn:
                            curList = EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.PawnSword:
                            curList = EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.Rook:
                            curList = EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.RookCrossbow:
                            curList = EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.Bishop:
                            curList = EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];
                            break;

                        case UnitTypes.BishopCrossbow:
                            curList = EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key];
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                case ConditionTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            curList = EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.Pawn:
                            curList = EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.PawnSword:
                            curList = EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.Rook:
                            curList = EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.RookCrossbow:
                            curList = EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.Bishop:
                            curList = EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];
                            break;

                        case UnitTypes.BishopCrossbow:
                            curList = EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key];
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                default:
                    throw new Exception();
            }

            if (!TryFindCellInListAndRemove(xy, curList)) throw new Exception();
        }

        private static bool TryFindCellInListAndRemove(int[] xyTaking, List<int[]> list)
        {
            for (int xyNumber = 0; xyNumber < list.Count; xyNumber++)
            {
                if (list[xyNumber].Compare(xyTaking))
                {
                    list.RemoveAt(xyNumber);
                    return true;
                }
            }

            return false;
        }

        internal static int GetAllExtractionUnits(ResourceTypes resourceType, bool key)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return -AmountUnitsInGame(UnitTypes.Pawn, key)
                        - AmountUnitsInGame(UnitTypes.PawnSword, key)
                        - AmountUnitsInGame(UnitTypes.Rook, key)
                        - AmountUnitsInGame(UnitTypes.RookCrossbow, key)
                        - AmountUnitsInGame(UnitTypes.Bishop, key)
                        - AmountUnitsInGame(UnitTypes.BishopCrossbow, key);

                case ResourceTypes.Wood:
                    throw new Exception();

                case ResourceTypes.Ore:
                    throw new Exception();

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}
