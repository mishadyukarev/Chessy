using System;

namespace Scripts.Game
{
    public static class UnitValues
    {
        #region Health

        internal static int StandartAmountHealth(UnitTypes unitType, LevelUnitTypes levelUnitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (levelUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 500;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (levelUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 150;
                        default: throw new Exception();
                    }  
                case UnitTypes.Rook:
                    switch (levelUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 100;
                        default: throw new Exception();
                    }
                case UnitTypes.Bishop:
                    switch (levelUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 100;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (levelUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 1;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static float ForAddingHealth(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 0.2f;
                case UnitTypes.Pawn: return 1f;
                case UnitTypes.Rook: return 1f;
                case UnitTypes.Bishop: return 1f;
                case UnitTypes.Scout: throw new Exception();
                default: throw new Exception();
            }
        }

        #endregion


        #region Damage

        internal static int SimplePowerDamage(UnitTypes unitType, LevelUnitTypes upgUnitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 180;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }         
                case UnitTypes.Pawn:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 100;
                        case LevelUnitTypes.Iron: return 120;
                        default: throw new Exception();
                    }
                case UnitTypes.Rook:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 70;
                        case LevelUnitTypes.Iron: return 90;
                        default: throw new Exception();
                    }
                case UnitTypes.Bishop:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 70;
                        case LevelUnitTypes.Iron: return 90;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (upgUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: return 0;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
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
                case UnitTypes.Scout: throw new Exception();
                default: throw new Exception();
            }
        }

        #endregion


        #region Protection

        internal static float ProtectionPercentEnvir(UnitTypes unitType, EnvirTypes envirType)
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
                case UnitTypes.Scout: return 0;
                default: throw new Exception();
            }


        }
        internal static float ProtectionPercentBuild(UnitTypes unitType, BuildingTypes buildingType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: return 0;
                        case BuildingTypes.City: return 0.5f;
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: return 0;
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: return 0;
                        case BuildingTypes.City: return 0.5f;
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: return 0;
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Rook:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: return 0;
                        case BuildingTypes.City: return 0.5f;
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: return 0;
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Bishop:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: return 0;
                        case BuildingTypes.City: return 0.5f;
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: return 0;
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout: return 0;
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
                case UnitTypes.Scout: return 0;
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
                case UnitTypes.Scout: return 0;
                default: throw new Exception();
            }
        }

        #endregion


        #region Steps

        internal static int NeedAmountSteps(EnvirTypes envirType)
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

        internal static int StandartAmountSteps(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 1;
                case UnitTypes.Pawn: return 2;
                case UnitTypes.Rook:  return 2;
                case UnitTypes.Bishop: return 2;
                case UnitTypes.Scout: return 4;
                default: throw new Exception();
            }
        }

        #endregion
    }
}
