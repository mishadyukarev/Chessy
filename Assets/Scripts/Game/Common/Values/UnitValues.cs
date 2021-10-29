using System;

namespace Scripts.Game
{
    public static class UnitValues
    {
        #region Health

        public static int StandAmountHealth(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 300;
                case UnitTypes.Pawn: return 100;
                case UnitTypes.Rook: return 30;
                case UnitTypes.Bishop: return 30;
                case UnitTypes.Scout: return 1;
                default: throw new Exception();
            }
        }

        public static float ForAddingHealth(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 0.3f;
                case UnitTypes.Pawn: return 1;
                case UnitTypes.Rook: return 1;
                case UnitTypes.Bishop: return 1;
                case UnitTypes.Scout: return 1;
                default: throw new Exception();
            }
        }

        #endregion


        #region Damage

        public static int StandPowerDamage(UnitTypes unitType, LevelUnitTypes upgUnitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 300;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 150;
                        case LevelUnitTypes.Iron: return 200;
                        default: throw new Exception();
                    }
                case UnitTypes.Rook:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 150;
                        default: throw new Exception();
                    }
                case UnitTypes.Bishop:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 150;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 1;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        public static float UniqueRatioPowerDamage = 0.5f;

        #endregion


        #region Protection


        public static float Percent(CondUnitTypes condUnitType)
        {
            switch (condUnitType)
            {
                case CondUnitTypes.None: throw new Exception();
                case CondUnitTypes.Protected: return 0.2f;
                case CondUnitTypes.Relaxed: return -0.2f;
                default: throw new Exception();
            }
        }
        public static float ProtectionPercent(EnvirTypes envirType)
        {
            switch (envirType)
            {
                case EnvirTypes.None: throw new Exception();
                case EnvirTypes.Fertilizer: return -0.2f;
                case EnvirTypes.YoungForest: throw new Exception();
                case EnvirTypes.AdultForest: return 0.2f;
                case EnvirTypes.Hill: return 0.2f;
                case EnvirTypes.Mountain: throw new Exception();
                default: throw new Exception();
            }
        }
        public static float ProtectionPercent(BuildingTypes buildType)
        {
            switch (buildType)
            {
                case BuildingTypes.None: return 0;
                case BuildingTypes.City: return 0.25f;
                case BuildingTypes.Farm: return -0.1f;
                case BuildingTypes.Woodcutter: return -0.1f;
                case BuildingTypes.Mine: return -0.1f;
                case BuildingTypes.Camp: return 0.1f;
                default: throw new Exception();
            }
        }


        #endregion


        #region Steps

        public static int NeedAmountSteps(EnvirTypes envirType)
        {
            switch (envirType)
            {
                case EnvirTypes.None: throw new Exception();
                case EnvirTypes.Fertilizer:  throw new Exception();
                case EnvirTypes.YoungForest:  throw new Exception();
                case EnvirTypes.AdultForest: return 1;
                case EnvirTypes.Hill: return 1;
                case EnvirTypes.Mountain: throw new Exception();
                default: throw new Exception();
            }
        }

        public static int StandartAmountSteps(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: return 0;
                case UnitTypes.King: return 1;
                case UnitTypes.Pawn: return 2;
                case UnitTypes.Rook:  return 3;
                case UnitTypes.Bishop: return 3;
                case UnitTypes.Scout: return 4;
                default: throw new Exception();
            }
        }

        #endregion
    }
}
