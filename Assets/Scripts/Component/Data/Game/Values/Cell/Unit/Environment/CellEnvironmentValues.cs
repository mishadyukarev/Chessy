using System;

namespace Game.Game
{
    public static class CellEnvironmentValues
    {
        #region Common

        public static byte StartPercentForSpawn(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 30;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 50;
                case EnvironmentTypes.Hill: return 25;
                case EnvironmentTypes.Mountain: return 15;
                default: throw new Exception();
            }
        }

        public static int MaxResources(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 100;
                case EnvironmentTypes.YoungForest: return 1;
                case EnvironmentTypes.AdultForest: return 100;
                case EnvironmentTypes.Hill: return 100;
                case EnvironmentTypes.Mountain: return 1;
                default: throw new Exception();
            }
        }
        public static int MinResources(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 1;
                case EnvironmentTypes.YoungForest: return 1;
                case EnvironmentTypes.AdultForest: return 1;
                case EnvironmentTypes.Hill: return 1;
                case EnvironmentTypes.Mountain: return 1;
                default: throw new Exception();
            }
        }

        public static int StandartExtract(in BuildingTypes buildT, in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 10;
                case EnvironmentTypes.AdultForest: return 10;
                case EnvironmentTypes.Hill: return 10;
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
                case BuildingTypes.Mine:
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

        public static int AddingFromIceWall(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return MaxResources(env);
                default: throw new Exception();
            }
        }

        #endregion


        #region AdultForest

        public const int ADULT_FORESTS_FOR_TRUCE = 8;
        public static int FireAdultForest => MaxResources(EnvironmentTypes.AdultForest) / 4;
        public static int AddingAfterBuildingFarm => MaxResources(EnvironmentTypes.Fertilizer) / 2;

        #endregion


        #region YoungFores

        public const float PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE = 0.5f;

        #endregion

    }
}
