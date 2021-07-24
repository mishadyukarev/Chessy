using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Workers
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
                    return EGGM.KingInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.Pawn:
                    return EGGM.PawnInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.PawnSword:
                    return EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.Rook:
                    return EGGM.RookInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.RookCrossbow:
                    return EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.Bishop:
                    return EGGM.BishopInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

                case UnitTypes.BishopCrossbow:
                    return EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.GetAmountUnitsInGame(key);

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
                    EGGM.KingInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.SetAmountUnitsInGane(key, value);
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
                    EGGM.KingInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, adding);
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
                    EGGM.KingInfoEnt_AmountUnitsInGameCom.AddAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.Pawn:
                    EGGM.PawnInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.PawnSwordInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.Rook:
                    EGGM.RookInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.RookCrossbowInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.Bishop:
                    EGGM.BishopInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.BishopCrossbowInfoEnt_AmountUnitsInGameCom.TakeAmountUnitsInGame(key, taking);
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
