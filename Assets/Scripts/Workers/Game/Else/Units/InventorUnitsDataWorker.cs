using Assets.Scripts.Workers.Cell;
using System;

namespace Assets.Scripts.Workers.Game.Else
{
    internal sealed class InventorUnitsDataWorker : MainGeneralWorker
    {
        internal static int AmountUnitsInInventor(UnitTypes unitType, bool key)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key];

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
                    EGGM.KingInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.AmountUnitsInInventorDict[key] = value;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddUnitsInInventor(UnitTypes unitType, bool key, int adding = 1)
            => SetAmountUnitsInInventor(unitType, key, AmountUnitsInInventor(unitType, key) + adding);
        internal static void TakeUnitsInInventor(UnitTypes unitType, bool key, int taking = 1)
            => SetAmountUnitsInInventor(unitType, key, AmountUnitsInInventor(unitType, key) - taking);

        internal static bool HaveUnitInInventor(UnitTypes unitType, bool key) => AmountUnitsInInventor(unitType, key) > 0;
    }
}
