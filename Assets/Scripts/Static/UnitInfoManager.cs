using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Static
{
    internal class UnitInfoManager : CellWorker
    {

        internal static int AmountUnitsInGame(UnitTypes unitType, bool key)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.KingInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.Rook:
                    return EGGM.RookInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoInGameEnt_AmountDictCom.Amount(key);

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoInGameEnt_AmountDictCom.Amount(key);

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountUnitInGame(UnitTypes unitType, bool key, int value)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoInGameEnt_AmountDictCom.SetAmount(key, value);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddAmountUnitInGame(UnitTypes unitType, bool key, int adding = 1)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoInGameEnt_AmountDictCom.AddAmount(key, adding);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeAmountUnitInGame(UnitTypes unitType, bool key, int taking = 1)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.KingInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoInGameEnt_AmountDictCom.TakeAmount(key, taking);
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
