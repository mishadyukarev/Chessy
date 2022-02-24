using System;

namespace Game.Game
{
    public static class Environment_Values
    {
        #region Common


        public const float ENVIRONMENT_MAX = 1;

        public static float StandartExtract(in BuildingTypes buildT, in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 0.1f;
                case EnvironmentTypes.AdultForest: return 0.1f;
                case EnvironmentTypes.Hill: return 0.1f;
                default: throw new Exception();
            }
        }
        public static float Upgrade(in BuildingTypes buildT, in UpgradeTypes upgT)
        {
            switch (buildT)
            {
                case BuildingTypes.Farm:
                    switch (upgT)
                    {
                        case UpgradeTypes.PickCenter: return 0.5f;
                        default: throw new Exception();
                    }
                case BuildingTypes.Woodcutter:
                    switch (upgT)
                    {
                        case UpgradeTypes.PickCenter: return 0.5f;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        public static float RatioExtractPawnFromMaxResource(in LevelTypes levelUnit, in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.AdultForest:
                    switch (levelUnit)
                    {
                        case LevelTypes.First: return 0.1f;
                        case LevelTypes.Second: return 0.2f;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        public static float AddingFromIceWall(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return ENVIRONMENT_MAX;
                default: throw new Exception();
            }
        }

        #endregion


        #region Fertilize

        public const float DRY_FERTILIZE = ENVIRONMENT_MAX * 0.1f;
        public const float RIVER_FERTILIZE_AROUND = ENVIRONMENT_MAX * 0.1f;

        public const float FARM_EXTRACT = ENVIRONMENT_MAX * 0.1f;
        public const float FARM_CENTER_UPGRADE = FARM_EXTRACT * 0.5f;

        #endregion


        #region Hill

        public const float PAWN_PICK_EXTRACT_HILL = ENVIRONMENT_MAX * 0.1f;
        public const float CITY_EXTRACT_HILL = ENVIRONMENT_MAX * 0.1f;

        #endregion


        #region AdultForest

        public const float FireAdultForest = ENVIRONMENT_MAX / 4;
        public const float AddingAfterBuildingFarm = ENVIRONMENT_MAX / 2;
        public const float EXTRACT_PAWM_ADULT_FOREST = ENVIRONMENT_MAX / 10;
        public const float PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT = 1.5f;

        public const float WOODCUTTER_EXTRACT = ENVIRONMENT_MAX / 10;
        public const float WOODCUTTER_CENTER_UPGRADE = WOODCUTTER_EXTRACT * 0.5f;

        #endregion


        #region YoungFores

        public const float PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE = 0.5f;

        #endregion


        #region Mountain

        public const float ADDING_FROM_MOUNTAIN = 0.1f;

        #endregion
    }
}
