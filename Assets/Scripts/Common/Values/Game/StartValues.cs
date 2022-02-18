using System;

namespace Game.Game
{
    public static class StartValues
    {
        public const byte X_AMOUNT = 15;
        public const byte Y_AMOUNT = 11;
        public const byte ALL_CELLS_AMOUNT = X_AMOUNT * Y_AMOUNT;

        public const int PEOPLE_IN_CITY = 1;
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
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 3f;
                case ResourceTypes.Gold: return 3f;
                default: throw new Exception();
            }
        }
        public static int Units(in UnitTypes unit, in LevelTypes level)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Scout:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Elfemale: return 0;
                case UnitTypes.Snowy: return 0;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                default: throw new Exception();
            }
        }
    }
}
