using System;

namespace Chessy.Model.Values
{
    public static class StartGameValues
    {
        public const byte CLOUD_CELL_INDEX = 60;
        public const byte SPEED_WIND = 1;
        public const int PEOPLE_IN_CITY = 15;
        public const float NEED_WOOD_FOR_BUILDING_HOUSE = 0.5f;
        public const byte CELL_IDX_FOR_CLEARING_FOREST_FOR_1_PLAYER = 59;
        public const byte CELL_IDX_FOR_CLEARING_FOREST_FOR_2_PLAYER = 61;
        public const float PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT = 0.5f;

        public const DirectTypes DIRECT_WIND = DirectTypes.Right;
        public const SunSideTypes SUN_SIDE = SunSideTypes.Dawn;
        //public const PlayerTypes WHOSE_MOVE = PlayerTypes.First;
        public const CellClickTypes CELL_CLICK = CellClickTypes.SimpleClick;
        public const ToolsWeaponsWarriorTypes SELECTED_TOOL_WEAPON = ToolsWeaponsWarriorTypes.Axe;
        public const LevelTypes SELECTED_LEVEL_TOOL_WEAPON = LevelTypes.Second;

        public static float SpawnPercentEnvironment(in EnvironmentTypes envT)
        {
            switch (envT)
            {
                case EnvironmentTypes.Fertilizer: return 0.3f;
                case EnvironmentTypes.AdultForest: return 0.5f;
                case EnvironmentTypes.Hill: return 0.25f;
                case EnvironmentTypes.Mountain: return 0.3f;

                default: throw new Exception();
            }
        }

        public static float AmountResourceEveryone(in ResourceTypes resT)
        {
            switch (resT)
            {
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