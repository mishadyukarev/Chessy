using System;

namespace Chessy.Game
{
    public static class Start_Values
    {
        public const byte X_AMOUNT = 15;
        public const byte Y_AMOUNT = 11;
        public const byte ALL_CELLS_AMOUNT = X_AMOUNT * Y_AMOUNT;

        public const int PEOPLE_IN_CITY = 3;
        public const int MAX_AVAILABLE_PAWN = 3;

        public const float MIN_RESOURCES_ENVIRONMENT = 0.1f;

        public const byte START_WIND = 60;
        public const DirectTypes DIRECT_WIND = DirectTypes.Right;
        public const SunSideTypes SUN_SIDE = SunSideTypes.Dawn;
        public const PlayerTypes WHOSE_MOVE = PlayerTypes.First;
        public const CellClickTypes CELL_CLICK = CellClickTypes.SimpleClick;
        public const ToolWeaponTypes SELECTED_TOOL_WEAPON = ToolWeaponTypes.Axe;
        public const LevelTypes SELECTED_LEVEL_TOOL_WEAPON = LevelTypes.First;

        public static float SpawnPercent(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 0.3f;
                case EnvironmentTypes.AdultForest: return 0.5f;
                case EnvironmentTypes.Hill: return 0.25f;
                case EnvironmentTypes.Mountain: return 0.3f;
                default: throw new Exception();
            }
        }
        public static float Resources(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 1;
                case ResourceTypes.Wood: return 1;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 3;
                case ResourceTypes.Gold: return 3;
                default: throw new Exception();
            }
        }
        public static bool HaveUnit(in UnitTypes unit, in LevelTypes level)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return true;
                        case LevelTypes.Second: return false;
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn: return false;
                case UnitTypes.Scout:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return true;
                        case LevelTypes.Second: return false;
                        default: throw new Exception();
                    }
                case UnitTypes.Elfemale: return false;
                case UnitTypes.Snowy: return false;
                case UnitTypes.Undead: return false;
                case UnitTypes.Hell: return false;
                case UnitTypes.Skeleton: return false;
                case UnitTypes.Camel: return false;
                default: throw new Exception();
            }
        }


        #region Unit

        #region Steps



        public static float StandartForUnit(in UnitTypes unit)
        {
            var steps = 0f;

            switch (unit)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 1; break;
                case UnitTypes.Pawn: steps = 1; break;

                case UnitTypes.Scout: steps = 2.5f; break;

                case UnitTypes.Elfemale: steps = 2; break;
                case UnitTypes.Snowy: steps = 3; break;
                case UnitTypes.Undead: steps = 3; break;
                case UnitTypes.Hell: steps = 1; break;

                case UnitTypes.Skeleton: steps = 2; break;

                case UnitTypes.Camel: steps = 2; break;
                default: throw new Exception();
            }

            return steps;
        }

        #endregion

        #endregion

    }
}
