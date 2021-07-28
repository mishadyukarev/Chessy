using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Game.Else.Info.Units
{
    internal sealed class InfoAmountUnitsWorker : MainGeneralWorker
    {
        private static List<int[]> GetListAmountUnits(UnitTypes unitType, bool key)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AmountUnitsInGame[key];

                default:
                    throw new Exception();
            }
        }

        internal static int GetAmountAllUnitsInGame() => GetAmountAllUnitsInGame(true) + GetAmountAllUnitsInGame(false);
        internal static int GetAmountAllUnitsInGame(bool key)
        {
            return GetAmountUnitsInGame(UnitTypes.King, key)
                + GetAmountUnitsInGame(UnitTypes.Pawn, key)
                + GetAmountUnitsInGame(UnitTypes.PawnSword, key)
                + GetAmountUnitsInGame(UnitTypes.Rook, key)
                + GetAmountUnitsInGame(UnitTypes.RookCrossbow, key)
                + GetAmountUnitsInGame(UnitTypes.Bishop, key)
                + GetAmountUnitsInGame(UnitTypes.BishopCrossbow, key);
        }
        internal static int GetAmountUnitsInGame(UnitTypes unitType, bool key)
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
        internal static int GetAmountUnitsInGame(bool key, params UnitTypes[] unitTypes)
        {
            int amountUnits = default;
            foreach (var unitType in unitTypes) amountUnits += GetAmountUnitsInGame(unitType, key);
            return amountUnits;
        }
        internal static int[] GetXyUnitInGame(UnitTypes unitType, bool key, int index) => GetListAmountUnits(unitType, key)[index];
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
        internal static void AddAmountUnitInGame(UnitTypes unitType, bool key, int[] xyAdding) => GetListAmountUnits(unitType, key).Add(xyAdding);
        internal static void RemoveAmountUnitsInGame(UnitTypes unitType, bool key, int[] xyTaking)
        {
            if (!GetListAmountUnits(unitType, key).TryFindCellInListAndRemove(xyTaking)) throw new Exception();
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
    }
}
