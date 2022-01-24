using System;

namespace Game.Game
{
    public static class UnitDamageValues
    {
        public const float UNIQUE_PERCENT_DAMAGE = 0.5f;
        public const int HP_FOR_DEATH_AFTER_ATTACK = 15;

        internal static int StandDamage(in UnitTypes unit, in LevelTypes lev)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 300;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 100;
                        case LevelTypes.Second: return 150;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 100;
                        case LevelTypes.Second: return 150;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 50;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Elfemale:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 200;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Snowy:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 200;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Camel: return 0;
                default: throw new Exception();
            }
        }
        internal static float PercentTW(in ToolWeaponTypes tw)
        {
            switch (tw)
            {
                case ToolWeaponTypes.None: return 0;
                case ToolWeaponTypes.Pick: return 0;
                case ToolWeaponTypes.Sword: return 0.5f;
                case ToolWeaponTypes.Shield: return 0;
                default: throw new Exception();
            }
        }
        public static int Damage(in UniqueAbilityTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: return 25;
                default: throw new Exception();
            }
        }
        public static int FIRE_DAMAGE = 40;


        #region Protection

        internal static float ProtRelaxPercent(in ConditionUnitTypes cond)
        {
            switch (cond)
            {
                case ConditionUnitTypes.None: return 0;
                case ConditionUnitTypes.Protected: return 0.2f;
                case ConditionUnitTypes.Relaxed: return -0.2f;
                default: throw new Exception();
            }
        }
        internal static float ProtectionPercent(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.None: throw new Exception();
                case EnvironmentTypes.Fertilizer: return -0.2f;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 0.2f;
                case EnvironmentTypes.Hill: return 0.2f;
                case EnvironmentTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }
        internal static float ProtectionPercent(in BuildingTypes build)
        {
            switch (build)
            {
                case BuildingTypes.None: return 0;
                case BuildingTypes.City: return 0.2f;
                case BuildingTypes.Farm: return -0.1f;
                case BuildingTypes.Woodcutter: return -0.1f;
                case BuildingTypes.Mine: return -0.1f;
                case BuildingTypes.Camp: return 0;
                default: throw new Exception();
            }
        }

        #endregion
    }
}