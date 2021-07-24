using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Info
{
    internal abstract class InfoUnitsWorker : MainGeneralWorker
    {
        internal static int GetAmountUnitsInStandartCondition(ProtectRelaxTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ProtectRelaxTypes.None:
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

                case ProtectRelaxTypes.Protected:
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

                case ProtectRelaxTypes.Relaxed:
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
        internal static List<int[]> GetUnitsInStandardCondition(ProtectRelaxTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ProtectRelaxTypes.None:
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

                case ProtectRelaxTypes.Protected:
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

                case ProtectRelaxTypes.Relaxed:
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
        internal static bool TryFindUnitInStandartCondition(ProtectRelaxTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ProtectRelaxTypes.None:
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

                case ProtectRelaxTypes.Protected:
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

                case ProtectRelaxTypes.Relaxed:
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
        internal static void AddUnitInStandartCondition(ProtectRelaxTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ProtectRelaxTypes.None:
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

                case ProtectRelaxTypes.Protected:
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

                case ProtectRelaxTypes.Relaxed:
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
        internal static void TakeUnitInStandartCondition(ProtectRelaxTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy)
        {
            List<int[]> curList;

            switch (protectRelaxType)
            {
                case ProtectRelaxTypes.None:
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

                case ProtectRelaxTypes.Protected:
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

                case ProtectRelaxTypes.Relaxed:
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

            for (int xyNumber = 0; xyNumber < curList.Count; xyNumber++)
            {
                if (curList[xyNumber].Compare(xy))
                {
                    curList.RemoveAt(xyNumber);
                    return;
                }
            }

            throw new Exception();
        }


        internal static bool IsSettedKing(bool key) => EGGM.KingInfoEnt_IsSettedUnitDictCom.IsSettedUnit(key);
        internal static void SetSettedKing(bool key, bool value) => EGGM.KingInfoEnt_IsSettedUnitDictCom.SetIsSettedUnit(key, value);

        internal static int AmountUnits(UnitTypes unitType, bool isMaster)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInvetor(isMaster);

                default:
                    throw new Exception();
            }
        }
        internal static bool HaveUnitInInventor(UnitTypes unitType, bool isMaster)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.HaveUnitInInventor(isMaster);

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountUnitsInInventor(UnitTypes unitType, bool key, int value)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.SetAmountUnitsInInventor(key, value);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void AddUnitsInInventor(UnitTypes unitType, bool isMaster, int adding = 1)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.AddAmountUnitsInInventor(isMaster, adding);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeUnitsInInventor(UnitTypes unitType, bool isMaster, int taking = 1)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.TakeAmountUnitsInInventor(isMaster, taking);
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
