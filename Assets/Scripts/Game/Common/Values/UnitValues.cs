using System;

namespace Scripts.Game
{
    public static class UnitValues
    {
        #region Health

        public static int StandMaxHpUnit(UnitTypes unitType)
        {
            float amountHp = 0;
            switch (unitType)
            {
                case UnitTypes.None: amountHp = 0; break;
                case UnitTypes.King: amountHp = 300; break;
                case UnitTypes.Pawn: amountHp = 100; break;
                case UnitTypes.Rook: amountHp = 30; break;
                case UnitTypes.Bishop: amountHp = 30; break;
                case UnitTypes.Scout: amountHp = 1; break;
                default: throw new Exception();
            }

            return (int)amountHp;
        }

        public static float PercentForAddingHp(UnitTypes unitType)
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

        public static int StandDamage(UnitTypes unitType, LevelUnitTypes upgUnitType)
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
        public static float PercentTW(ToolWeaponTypes tlWType)
        {
            switch (tlWType)
            {
                case ToolWeaponTypes.None: return 0;
                case ToolWeaponTypes.Hoe: throw new Exception();
                case ToolWeaponTypes.Pick: return 0;
                case ToolWeaponTypes.Sword: return 0.5f;
                case ToolWeaponTypes.Shield: return 0;
                default: throw new Exception();
            }
        }

        public const float UNIQUE_PERCENT_DAMAGE = 0.5f;
        public const int HP_FOR_DEATH_AFTER_ATTACK = 15;

        #endregion


        #region Protection


        public static float Percent(CondUnitTypes condUnitType)
        {
            switch (condUnitType)
            {
                case CondUnitTypes.None: return 0;
                case CondUnitTypes.Protected: return 0.2f;
                case CondUnitTypes.Relaxed: return -0.2f;
                default: throw new Exception();
            }
        }
        public static float ProtectionPercent(EnvTypes envirType)
        {
            switch (envirType)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer: return -0.2f;
                case EnvTypes.YoungForest: return 0;
                case EnvTypes.AdultForest: return 0.2f;
                case EnvTypes.Hill: return 0.2f;
                case EnvTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }
        public static float ProtectionPercent(BuildTypes buildType)
        {
            switch (buildType)
            {
                case BuildTypes.None: return 0;
                case BuildTypes.City: return 0.25f;
                case BuildTypes.Farm: return -0.1f;
                case BuildTypes.Woodcutter: return -0.1f;
                case BuildTypes.Mine: return -0.1f;
                case BuildTypes.Camp: return 0.1f;
                default: throw new Exception();
            }
        }


        #endregion


        #region Steps

        public static int NeedAmountSteps(EnvTypes envirType)
        {
            switch (envirType)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer:  throw new Exception();
                case EnvTypes.YoungForest:  throw new Exception();
                case EnvTypes.AdultForest: return 1;
                case EnvTypes.Hill: return 1;
                case EnvTypes.Mountain: throw new Exception();
                default: throw new Exception();
            }
        }
        public static int StandartAmountSteps(bool haveEffect, UnitTypes unitType, float upg)
        {
            var steps = 0;

            switch (unitType)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 2; break;
                case UnitTypes.Pawn: steps = 2; break;
                case UnitTypes.Rook: steps = 3; break;
                case UnitTypes.Bishop: steps = 3; break;
                case UnitTypes.Scout: steps = 4; break;
                default: throw new Exception();
            }

            if (haveEffect) steps += 1;

            steps += (int)upg;

            return steps;
        }

        #endregion


        #region

        public static int MaxStandAmountWater => 100;

        #endregion
    }
}
