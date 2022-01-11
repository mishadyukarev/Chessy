using System;

namespace Game.Game
{
    internal readonly struct DamageUnitValues
    {
        internal const float UNIQUE_PERCENT_DAMAGE = 0.5f;
        internal const int HP_FOR_DEATH_AFTER_ATTACK = 15;

        internal int StandDamage(UnitTypes unit, LevelTypes lev)
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
                default: throw new Exception();
            }
        }
        internal float PercentTW(TWTypes tw)
        {
            switch (tw)
            {
                case TWTypes.None: return 0;
                case TWTypes.Pick: return 0;
                case TWTypes.Sword: return 0.5f;
                case TWTypes.Shield: return 0;
                default: throw new Exception();
            }
        }


        #region Protection

        internal float ProtRelaxPercent(ConditionUnitTypes cond)
        {
            switch (cond)
            {
                case ConditionUnitTypes.None: return 0;
                case ConditionUnitTypes.Protected: return 0.2f;
                case ConditionUnitTypes.Relaxed: return -0.2f;
                default: throw new Exception();
            }
        }
        internal float ProtectionPercent(EnvTypes env)
        {
            switch (env)
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
        internal float ProtectionPercent(BuildTypes build)
        {
            switch (build)
            {
                case BuildTypes.None: return 0;
                case BuildTypes.City: return 0.2f;
                case BuildTypes.Farm: return -0.1f;
                case BuildTypes.Woodcutter: return -0.1f;
                case BuildTypes.Mine: return -0.1f;
                case BuildTypes.Camp: return 0;
                default: throw new Exception();
            }
        }

        #endregion
    }
}