using System;

namespace Game.Game
{
    public static class CellUnitDamage_Values
    {
        public const float UNIQUE_PERCENT_DAMAGE = 0.5f;
        public const float HP_FOR_DEATH_AFTER_ATTACK = 0.15f;

        public static float StandDamage(in UnitTypes unit, in LevelTypes lev)
        {
            switch (unit)
            {
                case UnitTypes.King:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 3;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 1.5f;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1.5f;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Elfemale:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 2;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Snowy:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 2;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Undead:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1.5f;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Hell:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 5f;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }

                case UnitTypes.Skeleton: return 0.5f;

                case UnitTypes.Camel: return 0;
                default: throw new Exception();
            }
        }
        public static float ToolWeaponExtraPercent(in ToolWeaponTypes tw)
        {
            switch (tw)
            {
                case ToolWeaponTypes.Pick: return 0;
                case ToolWeaponTypes.Sword: return 0.5f;
                case ToolWeaponTypes.Shield: return 0;
                default: throw new Exception();
            }
        }
        public static float ToolWeaponMainPercent(in ToolWeaponTypes tw, in LevelTypes level)
        {
            switch (level)
            {
                case LevelTypes.First:
                    switch (tw)
                    {
                        case ToolWeaponTypes.BowCrossbow: return 0;
                        case ToolWeaponTypes.Axe: return 0;
                        default: throw new Exception();
                    }

                case LevelTypes.Second:
                    switch (tw)
                    {
                        case ToolWeaponTypes.BowCrossbow: return 0.5f;
                        case ToolWeaponTypes.Axe: return 0.5f;
                        default: throw new Exception();
                    }

                default: throw new Exception();
            }
        }


        #region Protection

        public static float ProtRelaxPercent(in ConditionUnitTypes cond)
        {
            switch (cond)
            {
                case ConditionUnitTypes.None: return 0;
                case ConditionUnitTypes.Protected: return 0.2f;
                case ConditionUnitTypes.Relaxed: return -0.2f;
                default: throw new Exception();
            }
        }
        public static float ProtectionPercent(in EnvironmentTypes env)
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

        #endregion
    }
}