using System;

namespace Scripts.Game
{
    public static class UnitValues
    {
        #region Health

        internal static int StandartAmountHealth(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return 500;

                case UnitTypes.Pawn:
                    return 100;

                case UnitTypes.Rook:
                    return 100;

                case UnitTypes.Bishop:
                    return 100;

                default:
                    throw new Exception();
            }
        }
        internal static float ForAdding(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return 0.2f;

                case UnitTypes.Pawn:
                    return 1f;

                case UnitTypes.Rook:
                    return 1f;

                case UnitTypes.Bishop:
                    return 1f;

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region Damage

        internal static int SimplePowerDamage(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 150;
                case UnitTypes.Pawn: return 80;
                case UnitTypes.Rook: return 80;
                case UnitTypes.Bishop: return 80;
                default: throw new Exception();
            }
        }

        internal static float UniqueRatioPowerDamage(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn: return 0.5f;
                case UnitTypes.Rook: return 0.5f;
                case UnitTypes.Bishop: return 0.5f;
                default: throw new Exception();
            }
        }

        #endregion


        #region Protection

        internal static float ProtectionRatioEnvir(UnitTypes unitType, EnvirTypes envirType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (envirType)
                    {
                        case EnvirTypes.None: throw new Exception();
                        case EnvirTypes.Fertilizer: return -0.5f;
                        case EnvirTypes.YoungForest: throw new Exception();
                        case EnvirTypes.AdultForest: return 0.4f;
                        case EnvirTypes.Hill: return 0.2f;
                        case EnvirTypes.Mountain: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (envirType)
                    {
                        case EnvirTypes.None: throw new Exception();
                        case EnvirTypes.Fertilizer: return -0.5f;
                        case EnvirTypes.YoungForest: throw new Exception();
                        case EnvirTypes.AdultForest: return 0.5f;
                        case EnvirTypes.Hill: return 0.2f;
                        case EnvirTypes.Mountain: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Rook:
                    switch (envirType)
                    {
                        case EnvirTypes.None: throw new Exception();
                        case EnvirTypes.Fertilizer: return -0.5f;
                        case EnvirTypes.YoungForest: throw new Exception();
                        case EnvirTypes.AdultForest: return 0.5f;
                        case EnvirTypes.Hill: return 0.2f;
                        case EnvirTypes.Mountain: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Bishop:
                    switch (envirType)
                    {
                        case EnvirTypes.None: throw new Exception();
                        case EnvirTypes.Fertilizer: return -0.5f;
                        case EnvirTypes.YoungForest: throw new Exception();
                        case EnvirTypes.AdultForest: return 0.5f;
                        case EnvirTypes.Hill: return 0.2f;
                        case EnvirTypes.Mountain: throw new Exception();
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }


        }

        internal static float PercentForProtection(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 0.5f;
                case UnitTypes.Pawn: return 0.5f;
                case UnitTypes.Rook: return 0.5f;
                case UnitTypes.Bishop: return 0.5f;
                default: throw new Exception();
            }
        }
        internal static float PercentForRelax(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return -0.5f;
                case UnitTypes.Pawn: return -0.5f;
                case UnitTypes.Rook: return -0.5f;
                case UnitTypes.Bishop: return -0.5f;
                default: throw new Exception();
            }
        }

        #endregion


        #region Steps

        internal static int NeedAmountSteps(EnvirTypes envirType)
        {
            switch (envirType)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    throw new Exception();

                case EnvirTypes.YoungForest:
                    throw new Exception();

                case EnvirTypes.AdultForest:
                    return 2;

                case EnvirTypes.Hill:
                    return 2;

                case EnvirTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static int StandartAmountSteps(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return 1;

                case UnitTypes.Pawn:
                    return 2;

                case UnitTypes.Rook:
                    return 2;

                case UnitTypes.Bishop:
                    return 2;
                default:
                    throw new Exception();
            }
        }

        #endregion
    }
}
