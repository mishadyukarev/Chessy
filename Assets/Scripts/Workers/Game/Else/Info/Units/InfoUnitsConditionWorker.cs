using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Info
{
    internal sealed class InfoUnitsConditionWorker : MainGeneralWorker
    {
        private static List<int[]> GetUnitsInStandardCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInNone[key];

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInNone[key];

                        default:
                            throw new Exception();
                    }

                case ConditionUnitTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInProtect[key];

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInProtect[key];

                        default:
                            throw new Exception();
                    }

                case ConditionUnitTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return EGGM.KingInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.Pawn:
                            return EGGM.PawnInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.PawnSword:
                            return EGGM.PawnSwordInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.Rook:
                            return EGGM.RookInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.RookCrossbow:
                            return EGGM.RookCrossbowInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.Bishop:
                            return EGGM.BishopInfoEnt_AmountUnitsInRelaxCom.UnitsInRelax[key];

                        case UnitTypes.BishopCrossbow:
                            return EGGM.BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.UnitsInRelax[key];

                        default:
                            throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }

        internal static int[] GetXyInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => (int[])GetUnitsInStandardCondition(protectRelaxType, unitType, key)[index].Clone();
        internal static int GetAmountUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).Count;
        internal static bool TryFindUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).TryFindCell(xy);
        internal static void AddUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyAdding) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).Add(xyAdding);
        internal static void RemoveUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyTaking)
        {
            var v = GetUnitsInStandardCondition(protectRelaxType, unitType, key);

            if (!GetUnitsInStandardCondition(protectRelaxType, unitType, key).TryFindCellInListAndRemove(xyTaking))
                throw new Exception();
        }
        internal static void RemoveUnitInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index)
            => GetUnitsInStandardCondition(protectRelaxType, unitType, key).RemoveAt(index);
    }
}