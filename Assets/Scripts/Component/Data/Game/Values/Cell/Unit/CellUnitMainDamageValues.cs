using System;

namespace Game.Game
{
    public static class CellUnitMainDamageValues
    {
        public const float UNIQUE_PERCENT_DAMAGE = 0.5f;
        public const float HP_FOR_DEATH_AFTER_ATTACK = 0.15f;

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
                case UnitTypes.Undead:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 150;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                case UnitTypes.Hell:
                    switch (lev)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 600;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }

                case UnitTypes.Skeleton: return 50;

                case UnitTypes.Camel: return 0;
                default: throw new Exception();
            }
        }
        internal static float PercentExtraDamageTW(in CellUnitExtraToolWeaponE extraTWE)
        {
            switch (extraTWE.ToolWeapon)
            {
                case ToolWeaponTypes.Pick: return 0;
                case ToolWeaponTypes.Sword: return 0.5f;
                case ToolWeaponTypes.Shield: return 0;
                default: throw new Exception();
            }
        }
        internal static float PercentDamageTW(in CellUnitMainToolWeaponE mainTWE)
        {
            switch (mainTWE.Level)
            {
                case LevelTypes.First:
                    switch (mainTWE.ToolWeapon)
                    {
                        case ToolWeaponTypes.BowCrossbow: return 0;
                        case ToolWeaponTypes.Axe: return 0;
                        default: throw new Exception();
                    }

                case LevelTypes.Second:
                    switch (mainTWE.ToolWeapon)
                    {
                        case ToolWeaponTypes.BowCrossbow: return 0.3f;
                        case ToolWeaponTypes.Axe: return 0;
                        default: throw new Exception();
                    }

                default: throw new Exception();
            }
        }


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

        #endregion
    }
}