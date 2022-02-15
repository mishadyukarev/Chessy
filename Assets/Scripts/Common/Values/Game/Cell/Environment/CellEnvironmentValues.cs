using System;

namespace Game.Game
{
    public static class CellEnvironmentValues
    {
        #region Common


        public const float STANDART_MAX_AMOUNT_RESOURCES = 1;

        public static float MinResources(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 0.00001f;
                case EnvironmentTypes.YoungForest: return 0.00001f;
                case EnvironmentTypes.AdultForest: return 0.00001f;
                case EnvironmentTypes.Hill: return 0.00001f;
                case EnvironmentTypes.Mountain: return 0.00001f;
                default: throw new Exception();
            }
        }

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
                case EnvironmentTypes.Fertilizer: return STANDART_MAX_AMOUNT_RESOURCES;
                default: throw new Exception();
            }
        }

        #endregion


        #region AdultForest

        public static float FireAdultForest => STANDART_MAX_AMOUNT_RESOURCES / 4;
        public static float AddingAfterBuildingFarm => STANDART_MAX_AMOUNT_RESOURCES / 2;

        #endregion


        #region YoungFores

        public const float PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE = 0.5f;

        #endregion


        #region Mountain

        public const float ADDING_FROM_MOUNTAIN = 0.1f;

        #endregion
    }
}
