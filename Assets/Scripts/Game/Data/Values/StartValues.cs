using System;

namespace Chessy.Game.Values
{
    public static class StartValues
    {
        public const byte X_AMOUNT = 15;
        public const byte Y_AMOUNT = 11;
        public const byte CELLS = X_AMOUNT * Y_AMOUNT;

        public const int PEOPLE_IN_CITY = 15;
        public const int MAX_AVAILABLE_PAWN = 1;

        public const float MIN_RESOURCES_ENVIRONMENT = 0.1f;

        public const byte START_WIND = 60;
        public const float STRENGTH_WIND = 1;
        public const DirectTypes DIRECT_WIND = DirectTypes.Right;
        public const SunSideTypes SUN_SIDE = SunSideTypes.Dawn;
        public const PlayerTypes WHOSE_MOVE = PlayerTypes.First;
        public const CellClickTypes CELL_CLICK = CellClickTypes.SimpleClick;
        public const ToolWeaponTypes SELECTED_TOOL_WEAPON = ToolWeaponTypes.Axe;
        public const LevelTypes SELECTED_LEVEL_TOOL_WEAPON = LevelTypes.Second;

        public const float NEED_WOOD_FOR_BUILDING_HOUSE = 0.15f;

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
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
    }
}
